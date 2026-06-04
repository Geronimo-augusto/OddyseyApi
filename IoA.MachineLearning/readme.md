# 🧠 Oddysey.MachineLearning - Motor Preditivo do Internet of Animals

![Python](https://img.shields.io/badge/Python-3670A0?style=for-the-badge\&logo=python\&logoColor=ffdd54)
![FastAPI](https://img.shields.io/badge/FastAPI-005571?style=for-the-badge\&logo=fastapi)
![XGBoost](https://img.shields.io/badge/XGBoost-A61C3C?style=for-the-badge)
![Scikit Learn](https://img.shields.io/badge/Scikit--Learn-F7931E?style=for-the-badge\&logo=scikitlearn\&logoColor=white)

---

# 📖 Sobre o Projeto

O **Oddysey.MachineLearning** é o microsserviço responsável pela inteligência artificial do ecossistema **Internet of Animals (IoA)**.

Este componente recebe informações provenientes do módulo de monitoramento ambiental e do sistema de telemetria animal, processando os dados através de algoritmos de Machine Learning para calcular o risco de ocorrência de eventos ambientais extremos.

O serviço integra-se diretamente com:

* Oddysey.EdgeGateway (Visão Computacional + Sensoriamento)
* Oddysey.Api (Orquestrador .NET)
* Dashboard da Defesa Civil

Seu objetivo é transformar grandes volumes de dados ambientais e comportamentais em previsões compreensíveis e acionáveis.

---

# 🌍 Papel Dentro do Ecossistema IoA

```text
Oddysey.EdgeGateway
(Câmeras + YOLO + Clima)
          │
          ▼
Oddysey.Api (.NET 10gi)
(Orquestrador)
          │
          ▼
Oddysey.MachineLearning
(FastAPI + XGBoost)
          │
          ▼
Predição de Risco
          │
          ▼
Alertas SignalR
(Defesa Civil / Dashboard)
```

O microsserviço atua exclusivamente como motor de inferência e treinamento dos modelos preditivos.

---

# 📁 Estrutura do Projeto

```text
📁 Oddysey.MachineLearning/
│
├── main.py
│   ├── API FastAPI
│   ├── Endpoint de Inferência
│   └── Integração com o modelo treinado
│
├── train_model.py
│   ├── Geração de Dataset Sintético
│   ├── Clusterização K-Means
│   ├── Seleção de Features
│   ├── Treinamento MLP
│   ├── Treinamento XGBoost
│   └── Geração dos arquivos .pkl
│
├── xgboost_Oddysey.pkl
│   └── Modelo treinado utilizado em produção
│
├── features.pkl
│   └── Lista das features utilizadas na inferência
│
└── requirements.txt
```

---

# ⚙️ Arquitetura do Serviço

O projeto foi dividido em duas responsabilidades principais.

## 1️⃣ Treinamento dos Modelos

Arquivo:

```text
train_model.py
```

Responsável por:

* Gerar dados sintéticos de treinamento.
* Identificar padrões comportamentais.
* Selecionar atributos relevantes.
* Treinar modelos preditivos.
* Salvar os modelos para uso em produção.

---

## 2️⃣ Inferência em Tempo Real

Arquivo:

```text
main.py
```

Responsável por:

* Receber dados enviados pela API .NET.
* Aplicar o modelo treinado.
* Calcular o risco de desastre.
* Classificar o nível de alerta.
* Retornar o resultado para o ecossistema.

---

# 🤖 Pipeline de Machine Learning

## Etapa 1 - Geração do Dataset

O sistema cria um conjunto de dados sintético contendo:

* Frequência cardíaca
* Aceleração corporal
* Quantidade de aves detectadas
* Índice de movimentação
* Pressão atmosférica
* Temperatura ambiente

Esses dados simulam o comportamento de animais sentinelas em cenários normais e anômalos.

---

## Etapa 2 - Clusterização Comportamental

Algoritmo:

```text
K-Means
```

Objetivo:

* Separar comportamentos normais e anormais.
* Identificar possíveis padrões de pânico coletivo.
* Agrupar eventos semelhantes.

Variáveis utilizadas:

```text
heart_rate
acceleration
yolo_movement_index
```

---

## Etapa 3 - Seleção de Features

Técnica:

```text
SelectKBest
```

Objetivo:

* Remover atributos pouco relevantes.
* Reduzir ruído.
* Melhorar desempenho do modelo.

---

## Etapa 4 - Rede Neural

Algoritmo:

```text
MLPRegressor
```

Arquitetura:

```text
64 neurônios
32 neurônios
```

Objetivo:

* Aprender padrões não lineares.
* Servir como comparação para o modelo principal.

---

## Etapa 5 - Modelo Principal

Algoritmo:

```text
XGBoost Regressor
```

Responsável por:

* Calcular o risco final.
* Produzir uma probabilidade entre 0 e 1.
* Fornecer a classificação utilizada pelo sistema.

---

# 📊 Variáveis Utilizadas

| Variável            | Descrição                     |
| ------------------- | ----------------------------- |
| heart_rate          | Frequência cardíaca           |
| acceleration        | Aceleração corporal           |
| yolo_bird_count     | Quantidade de aves detectadas |
| yolo_movement_index | Índice de movimentação        |
| pressure_hpa        | Pressão atmosférica           |
| temperature_c       | Temperatura ambiente          |

---

# 🚨 Classificação de Alertas

Após a inferência, o sistema converte o score em níveis de alerta.

## NORMAL

```text
0.00 até 0.50
```

Nenhuma anomalia detectada.

---

## WARNING

```text
0.51 até 0.85
```

Possível alteração ambiental.

Exemplo:

```text
Agitação Coletiva / Pressão Anormal
```

---

## CRITICAL

```text
Acima de 0.85
```

Possível desastre iminente.

Exemplo:

```text
Evacuação: Anomalia Biológica e Climática Severa
```

---

# 🔌 Endpoint Disponível

## Predição de Desastre

```http
POST /predict-disaster
```

### Exemplo de Requisição

```json
{
  "speciesId": "Serinus_canaria_01",
  "latitude": -23.5505,
  "longitude": -46.6333,
  "acceleration": 14.2,
  "heartRate": 320,
  "yoloBirdCount": 5,
  "yoloMovementIndex": 12500.5,
  "pressureHpa": 1013,
  "temperatureC": 25.5
}
```

---

### Exemplo de Resposta

```json
{
  "AnomalyType": "Agitação Coletiva / Pressão Anormal",
  "Probability": 0.73,
  "AlertLevel": "WARNING"
}
```

---

# 🚀 Como Executar

## Instalar Dependências

```bash
pip install -r requirements.txt
```

---

## Treinar os Modelos

```bash
python train_model.py
```

Arquivos gerados:

```text
xgboost_Oddysey.pkl
features.pkl
avaliacao_modelo.png
```

---

## Executar a API

```bash
uvicorn main:app --reload --port 8000
```

Swagger:

```text
http://localhost:8000/docs
```

---

# 📈 Métricas

Durante o treinamento são calculadas:

* Mean Squared Error (MSE)
* R² Score

Além disso, é gerado automaticamente:

```text
avaliacao_modelo.png
```

para comparação visual entre valores previstos e valores reais.

---

# 🎯 Objetivo Científico

Este módulo busca identificar padrões de comportamento animal associados a possíveis eventos ambientais extremos através da combinação de:

* Inteligência Artificial
* Visão Computacional
* Sensoriamento Ambiental
* Telemetria Biológica

Os resultados são utilizados pelo sistema IoA para auxiliar a emissão de alertas antecipados para órgãos responsáveis e comunidades monitoradas.

---

# 👨‍💻 Projeto Integrado

Este repositório faz parte do projeto maior:

```text
Internet of Animals (IoA)
```

composto pelos módulos:

* Oddysey.EdgeGateway
* Oddysey.Api
* Oddysey.MachineLearning

que juntos formam uma plataforma distribuída de monitoramento ambiental e predição de riscos baseada em comportamento animal e inteligência artificial.
