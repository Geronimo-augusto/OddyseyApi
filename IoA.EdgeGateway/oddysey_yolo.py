from ultralytics import YOLO
import cv2
import numpy as np
import requests
import threading
import time
import random

# ==========================================
# ODDYSEY - EDGE GATEWAY (YOLO + SENSOR FUSION)
# ==========================================

VIDEO = r"canarios.mp4"
URL_API_CSHARP = "https://localhost:7054/api/telemetry/predict" # Mude para a porta do seu C#
OPENWEATHER_API_KEY = "SUA_CHAVE_AQUI" # Insira sua chave do OpenWeather

# Coordenadas da Câmera (Simulado: São Paulo)
LATITUDE = -23.5505
LONGITUDE = -46.6333

# Controle de tempo para envios assíncronos
TEMPO_ENVIO_CSHARP = 5 # Envia para o C# a cada 5 segundos
TEMPO_ATUALIZACAO_CLIMA = 300 # Busca o clima a cada 5 minutos (evita block na API)

# Variáveis Globais de Estado
ultimo_envio = time.time()
ultima_checagem_clima = 0
dados_clima = {"pressao": 1013.0, "temperatura": 25.0} # Valores padrão iniciais

# ==========================
# FUNÇÕES ASSÍNCRONAS
# ==========================

def buscar_clima():
    """Consome a API do OpenWeather no background"""
    global dados_clima, ultima_checagem_clima
    url = f"https://api.openweathermap.org/data/2.5/weather?lat={LATITUDE}&lon={LONGITUDE}&appid={OPENWEATHER_API_KEY}&units=metric"
    try:
        response = requests.get(url)
        if response.status_code == 200:
            data = response.json()
            dados_clima["pressao"] = data["main"]["pressure"]
            dados_clima["temperatura"] = data["main"]["temp"]
            print(f"[CLIMA] Atualizado: {dados_clima['temperatura']}°C | {dados_clima['pressao']}hPa")
        ultima_checagem_clima = time.time()
    except Exception as e:
        print(f"[ERRO CLIMA]: {e}")

def enviar_telemetria_csharp(bird_count, movement_index):
    """Envia o pacote completo (Câmera + Clima + Sensores) para o C#"""
    # Simulando os dados das Tags anexadas aos animais que a câmera está filmando
    simulacao_accel = random.uniform(9.0, 15.0)
    simulacao_hr = random.uniform(280, 350)
    
    payload = {
        "SpeciesId": "Serinus_canaria_01",
        "Latitude": LATITUDE,
        "Longitude": LONGITUDE,
        "Acceleration": round(simulacao_accel, 2),
        "HeartRate": round(simulacao_hr, 2),
        "YoloBirdCount": bird_count,
        "YoloMovementIndex": movement_index,
        "PressureHpa": dados_clima["pressao"],
        "TemperatureC": dados_clima["temperatura"]
    }
    
    try:
        # verify=False ignora erro de certificado SSL em localhost no desenvolvimento
        response = requests.post(URL_API_CSHARP, json=payload, verify=False)
        print(f"[BFF C#] Pacote enviado! Resposta: {response.status_code}")
    except Exception as e:
        print(f"[ERRO BFF C#]: Não foi possível conectar. {e}")


# ==========================
# SETUP YOLO E VÍDEO
# ==========================
model = YOLO("yolov8s.pt")
AVES_ESPERADAS = 5
CONFIANCA_MINIMA = 0.25
LIMITE_MOVIMENTO_SUSPEITO = 25000
LIMITE_MOVIMENTO_ALERTA = 50000

cap = cv2.VideoCapture(VIDEO)
ret, frame_anterior = cap.read()

if not ret:
    print("Erro ao abrir vídeo")
    exit()

frame_anterior = cv2.resize(frame_anterior, (960, 540))
gray_anterior = cv2.cvtColor(frame_anterior, cv2.COLOR_BGR2GRAY)
gray_anterior = cv2.GaussianBlur(gray_anterior, (21, 21), 0)

# Inicia a primeira busca de clima
threading.Thread(target=buscar_clima).start()

# ==========================
# LOOP PRINCIPAL (VISÃO)
# ==========================
while True:
    ret, frame = cap.read()
    if not ret:
        break

    frame = cv2.resize(frame, (960, 540))
    results = model(frame, verbose=False)
    aves_detectadas = 0

    for result in results:
        for box in result.boxes:
            classe = int(box.cls[0])
            nome = model.names[classe]
            confianca = float(box.conf[0])

            if nome != "bird" or confianca < CONFIANCA_MINIMA:
                continue

            aves_detectadas += 1
            x1, y1, x2, y2 = map(int, box.xyxy[0])
            cv2.rectangle(frame, (x1, y1), (x2, y2), (0, 255, 0), 2)

    # Análise de Movimento (Motion Tracking)
    gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    gray = cv2.GaussianBlur(gray, (21, 21), 0)
    diff = cv2.absdiff(gray_anterior, gray)
    thresh = cv2.threshold(diff, 25, 255, cv2.THRESH_BINARY)[1]
    thresh = cv2.dilate(thresh, None, iterations=2)
    movimento_total = cv2.countNonZero(thresh)
    gray_anterior = gray.copy()

    # ==========================
    # INTEGRAÇÃO (IOT & APIs)
    # ==========================
    tempo_atual = time.time()
    
    # Atualiza o clima se passaram 5 minutos
    if tempo_atual - ultima_checagem_clima > TEMPO_ATUALIZACAO_CLIMA:
        threading.Thread(target=buscar_clima).start()
        
    # Envia os dados para o C# a cada 5 segundos
    if tempo_atual - ultimo_envio > TEMPO_ENVIO_CSHARP:
        threading.Thread(target=enviar_telemetria_csharp, args=(aves_detectadas, movimento_total)).start()
        ultimo_envio = tempo_atual

    # ==========================
    # RENDERIZAÇÃO DO PAINEL
    # ==========================
    status = "NORMAL"
    cor_banner = (0, 120, 0)

    if aves_detectadas < AVES_ESPERADAS:
        status = "ALERTA - AVE AUSENTE"
        cor_banner = (0, 0, 255)
    elif movimento_total > LIMITE_MOVIMENTO_ALERTA:
        status = "ALERTA - AGITACAO COLETIVA"
        cor_banner = (0, 0, 255)

    cv2.rectangle(frame, (0, 0), (960, 90), cor_banner, -1)
    cv2.putText(frame, f"ODDYSEY | Status: {status}", (20, 30), cv2.FONT_HERSHEY_SIMPLEX, 0.8, (255, 255, 255), 2)
    cv2.putText(frame, f"Aves: {aves_detectadas} | Movimento: {movimento_total}", (20, 65), cv2.FONT_HERSHEY_SIMPLEX, 0.7, (0, 255, 255), 2)

    cv2.imshow("ODDYSEY - Edge Computing", frame)

    if cv2.waitKey(30) == 27: # ESC para sair
        break

cap.release()
cv2.destroyAllWindows()