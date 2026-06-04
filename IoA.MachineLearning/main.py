from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import pandas as pd
import joblib
import numpy as np
import traceback

app = FastAPI(title="IoA ML Engine", description="Motor de Predição com XGBoost e YOLO")

# Carrega o modelo e as features na inicialização
try:
    model = joblib.load('xgboost_ioa.pkl')
    selected_features = joblib.load('features.pkl')
except FileNotFoundError:
    print("Execute train_model.py primeiro para gerar o arquivo .pkl!")

# DTO que deve refletir exatamente o que o C# envia
class TelemetryPayload(BaseModel):
    speciesId: str
    latitude: float
    longitude: float
    acceleration: float
    heartRate: float
    # Adições da Visão Computacional (YOLO) e Clima
    yoloBirdCount: int
    yoloMovementIndex: float
    pressureHpa: float
    temperatureC: float

@app.post("/predict-disaster")
async def predict_disaster(data: TelemetryPayload):
    try:
        # 1. Montar o DataFrame com os dados recebidos
# 1. Montar o DataFrame com os dados recebidos
        input_data = pd.DataFrame([{
            'heart_rate': data.heartRate,
            'acceleration': data.acceleration,
            'yolo_bird_count': data.yoloBirdCount,
            'yolo_movement_index': data.yoloMovementIndex,
            'pressure_hpa': data.pressureHpa,
            'temperature_c': data.temperatureC  
        }])

        # 2. Filtrar apenas as features que o modelo usou no treinamento
        X_infer = input_data[selected_features]

        # 3. Fazer a predição (XGBoost)
        risk_score = float(model.predict(X_infer)[0])
        risk_score = np.clip(risk_score, 0.0, 1.0) # Garante que fique entre 0 e 1

        # 4. Classificação Baseada no Score
        if risk_score > 0.85:
            alert_level = "CRITICAL"
            anomaly_type = "Evacuação: Anomalia Biológica e Climática Severa"
        elif risk_score > 0.50:
            alert_level = "WARNING"
            anomaly_type = "Agitação Coletiva / Pressão Anormal"
        else:
            alert_level = "NORMAL"
            anomaly_type = "Nenhuma Anomalia"

        return {
            "AnomalyType": anomaly_type,
            "Probability": risk_score,
            "AlertLevel": alert_level
        }

    except Exception as e:
        # Imprime o erro real no console para não ficarmos cegos
        print("=== ERRO NO MOTOR ML ===")
        traceback.print_exc()
        raise HTTPException(status_code=500, detail=str(e))