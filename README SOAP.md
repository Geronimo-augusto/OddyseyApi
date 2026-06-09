<img src="https://raw.githubusercontent.com/AnaTorresLoureiro/legendary-doodle/refs/heads/main/Design%20sem%20nome%20(1).png"/>

<p align="center">

<img src="https://img.shields.io/badge/Global%20Solution-2026-4ea8de?style=for-the-badge&labelColor=0d0f14"/>

<img src="https://img.shields.io/badge/.NET%2010-ASP.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"/>

<img src="https://img.shields.io/badge/Python-FastAPI-009688?style=for-the-badge&logo=python&logoColor=white"/>

<img src="https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white"/>

<img src="https://img.shields.io/badge/Status-Concluído-success?style=for-the-badge"/>

</p>

<h1 align="center">🛰️ Space Telemetry</h1>

<p align="center">
<b>Monitoramento Inteligente de Fauna por Telemetria Espacial e Inteligência Artificial</b>
</p>

<p align="center">
Sistema distribuído desenvolvido para monitoramento ambiental através de biotelemetria, satélites de baixa órbita (LEO), Machine Learning e análise preditiva em tempo real.
</p>

---

# 👥 Integrantes

| Nome | RM |
|--------|--------|
| Ana Laura Torres Loureiro | RM554375 |
| Murilo Cordeiro Ferreira | RM556727 |
| Geronimo Augusto Nascimento Santos | RM557170 |
| Ianny Raquel Ferreira De Souza | RM559096 |

---

# 🌎 Sobre o Projeto

O **Space Telemetry** é uma solução criada para auxiliar pesquisadores, órgãos ambientais e instituições científicas na preservação da biodiversidade.

A proposta utiliza dispositivos de biotelemetria instalados em animais monitorados que transmitem informações fisiológicas e geográficas para satélites de baixa órbita (LEO).

Os dados são processados por uma API ASP.NET Core integrada a um serviço de Machine Learning desenvolvido em Python, capaz de detectar anomalias comportamentais e prever possíveis eventos ambientais de risco.

Todas as análises são armazenadas em SQL Server, permitindo rastreabilidade, auditoria e histórico completo das ocorrências.

---

# ⭐ Diferenciais

- Arquitetura baseada em SOA (Service Oriented Architecture)
- Comunicação entre microsserviços via REST
- Machine Learning para detecção de anomalias
- Persistência de histórico para auditoria
- Repository Pattern
- Injeção de Dependência
- DTOs para desacoplamento
- Tratamento centralizado de exceções
- Aplicação dos pilares da POO
- Integração ASP.NET Core + Python

# 🏛️ Aplicação dos Conceitos de SOA

O projeto foi desenvolvido seguindo os princípios da Arquitetura Orientada a Serviços (SOA), permitindo a separação de responsabilidades entre os componentes da solução.

## Serviços Identificados

| Serviço | Responsabilidade |
|----------|----------------|
| Telemetry API | Receber dados de telemetria |
| Telemetry Service | Processar regras de negócio |
| Machine Learning API | Realizar análises preditivas |
| Persistence Layer | Armazenar históricos e resultados |

## Benefícios Obtidos

- Baixo acoplamento
- Facilidade de manutenção
- Escalabilidade
- Reutilização de serviços
- Independência tecnológica

---

# 🎯 Objetivos

| Objetivo | Descrição |
|-----------|-----------|
| 🛰️ Monitoramento | Receber dados de telemetria em tempo real |
| 🤖 Inteligência Artificial | Detectar comportamentos anômalos |
| 🚨 Alertas Preventivos | Antecipar eventos ambientais críticos |
| 📊 Histórico | Armazenar análises para pesquisas futuras |
| 🌎 Sustentabilidade | Apoiar ações de preservação da biodiversidade |

---

# 🏗️ Arquitetura da Solução

```mermaid
flowchart LR

A[Biotelemetry Tag]
--> B[LEO Satellite]

B --> C[ASP.NET Core API]

C --> D[Telemetry Prediction Service]

D --> E[Python ML API]

D --> F[SQL Server]

E --> D

F --> G[Consumer Applications]

G --> H[Swagger]
```

---

# 🔄 Fluxo Operacional

```mermaid
flowchart TD

A[Biotelemetry Tag]
--> B[LEO Satellite]

B --> C[Telemetry Controller]

C --> D[Telemetry Prediction Service]

D --> E[Python ML API]

E --> F{Anomalia Detectada?}

F -->|Sim| G[Gerar Alerta]

F -->|Não| H[Registrar Normalidade]

G --> I[Salvar Histórico]

H --> I

I --> J[Disponibilizar via REST API]
```

---

# 🔁 Sequência de Comunicação

```mermaid
sequenceDiagram

participant Tag
participant Satellite
participant API
participant Service
participant Python
participant Database

Tag->>Satellite: Dados de Telemetria

Satellite->>API: Dados coletados

API->>Service: Solicitação de análise

Service->>Python: POST /predict

Python-->>Service: Resultado

Service->>Database: Salvar histórico

Database-->>Service: Confirmação

Service-->>API: Retorno

API-->>Satellite: Resposta
```

---

# 🧩 Diagrama de Classes

```mermaid
classDiagram

class SpaceEquipment {
    <<abstract>>
    +Id
    +Name
    +TransmitDiagnostic()
}

class Satellite {
    +OrbitType
    +AltitudeKm
}

class BiotelemetryTag {
    +SpeciesId
    +HeartRate
    +Acceleration
}

class PredictionHistory {
    +SpeciesId
    +Probability
    +AlertLevel
    +AnalysisDate
}

SpaceEquipment <|-- Satellite
SpaceEquipment <|-- BiotelemetryTag

class ITelemetryPredictionService

class TelemetryPredictionService

class IGenericRepository~T~

class GenericRepository~T~

ITelemetryPredictionService <|.. TelemetryPredictionService
IGenericRepository <|.. GenericRepository
```

---

# 🛠 Stack Tecnológica

| Camada | Tecnologia |
|----------|------------|
| Backend | ASP.NET Core 8 |
| Linguagem Principal | C# |
| Machine Learning | Python |
| Framework IA | FastAPI |
| Banco de Dados | SQL Server |
| ORM | Entity Framework Core |
| Documentação | Swagger |
| Arquitetura | SOA |
| Padrões | Repository Pattern, DTO, Dependency Injection |

---

# 🚀 Tecnologias Utilizadas

### Backend

- ASP.NET Core 10
- C#
- Swagger/OpenAPI

### Banco de Dados

- SQL Server
- Entity Framework Core

### Inteligência Artificial

- Python
- FastAPI
- Modelo de Machine Learning para análise preditiva

---

# 🧠 Conceitos de POO Aplicados

## Encapsulamento

Os atributos das entidades são protegidos e acessados através de propriedades e DTOs.

## Herança

A classe abstrata `SpaceEquipment` é utilizada como base para `Satellite` e `BiotelemetryTag`.

## Polimorfismo

O método `TransmitDiagnostic()` possui comportamentos distintos para cada equipamento.

## Abstração

A classe base define comportamentos comuns enquanto as subclasses implementam suas particularidades.

---

# 📐 Princípios SOLID Aplicados

### Single Responsibility Principle (SRP)

Cada classe possui uma única responsabilidade dentro da aplicação.

### Open Closed Principle (OCP)

Os serviços podem ser estendidos sem necessidade de modificar implementações existentes.

### Liskov Substitution Principle (LSP)

As implementações respeitam os contratos definidos pelas interfaces.

### Interface Segregation Principle (ISP)

Interfaces específicas evitam dependências desnecessárias.

### Dependency Inversion Principle (DIP)

A aplicação depende de abstrações através de interfaces e injeção de dependência.

---

# 🔌 Interfaces

### IGenericRepository<T>

Responsável pelas operações genéricas de persistência.

### ITelemetryPredictionService

Responsável pela comunicação com o serviço de Machine Learning e gerenciamento das análises.

---

# 💉 Injeção de Dependência

```csharp
builder.Services.AddScoped(
    typeof(IGenericRepository<>),
    typeof(GenericRepository<>)
);

builder.Services.AddScoped<
    ITelemetryPredictionService,
    TelemetryPredictionService
>();

builder.Services.AddHttpClient();
```

---

# 📦 DTOs


| DTO | Tipo |
|---------|---------|
| TelemetryDTO | Request |
| SatelliteDTO | Request |
| BiotelemetryTagDTO | Request |
| SpaceEquipmentDTO | Request/Response |
| PredictionResponseDTO | Response |

---

# 🔄 AutoMapper

A aplicação utiliza AutoMapper para realizar o mapeamento entre DTOs e entidades de domínio.

### Perfil Configurado

- TelemetryProfile

### Benefícios

- Redução de código repetitivo
- Conversão automática entre objetos
- Maior organização das camadas da aplicação
- Melhor manutenção do código

---

# 🗄️ Estrutura do Banco de Dados

## PredictionHistory

Tabela responsável por armazenar todas as análises realizadas pelo sistema.

| Campo | Descrição |
|---------|---------|
| Id | Identificador |
| SpeciesId | Espécie monitorada |
| Latitude | Coordenada geográfica |
| Longitude | Coordenada geográfica |
| Probability | Probabilidade da anomalia |
| AlertLevel | Nível do alerta |
| AnalysisDate | Data da análise |

## SpaceEquipment

Tabela base utilizada para representar os equipamentos monitorados.

### Tipos derivados

- Satellite
- BiotelemetryTag

Estratégia utilizada:

- TPH (Table Per Hierarchy)

## PredictionHistory

Armazena:

- Espécie monitorada
- Coordenadas geográficas
- Frequência cardíaca
- Aceleração
- Probabilidade da anomalia
- Tipo de alerta
- Data da análise

## SpaceEquipment

Tabela base contendo:

- Satellite
- BiotelemetryTag

Utilizando estratégia TPH (Table Per Hierarchy).

---

# ✔️ Validação de Dados

Antes do processamento das análises, a aplicação realiza validações dos dados recebidos.

### Validações Aplicadas

- Campos obrigatórios
- Coordenadas geográficas válidas
- Identificação da espécie
- Dados numéricos consistentes
- Estrutura correta dos DTOs

Essas validações garantem maior confiabilidade e integridade das informações processadas.

# 🛡️ Tratamento de Exceções

A exceção customizada:

```csharp
SpaceTelemetryException
```

centraliza erros relacionados a:

- Comunicação com API Python
- Falhas de processamento
- Erros de integração
- Exceções não tratadas

---

# 🔗 Endpoints

## POST /api/telemetry/predict

Recebe dados de telemetria e retorna uma análise preditiva.

### Exemplo

```json
{
  "speciesId": "ANIMAL-001",
  "latitude": -23.5505,
  "longitude": -46.6333,
  "acceleration": 8.5,
  "heartRate": 120
}
```

---

## GET /api/telemetry/predictions

Retorna o histórico completo de análises realizadas.

---

# 📂 Estrutura do Projeto

```text
📦 SpaceTelemetry
├── 📂 Controllers
├── 📂 Services
├── 📂 Repositories
├── 📂 Interfaces
├── 📂 Models
├── 📂 DTOs
├── 📁 Hubs
├── 📂 Data
├── 📂 Profiles
├── 📂 Exceptions
└── 📂 Migrations
```

---

# ▶️ Como Executar

## 1. Clonar o projeto

```bash
git clone URL_DO_REPOSITORIO
```

```bash
cd SpaceTelemetry
```

## 2. Aplicar migrations

```bash
dotnet ef database update
```

## 3. Executar aplicação

```bash
dotnet run
```

## 4. Acessar Swagger

```text
https://localhost:[porta]/swagger
```

> Certifique-se de que o serviço Python esteja em execução antes de utilizar o endpoint `/predict`.

---

# 📸 Evidências

Adicionar capturas de tela de:

- Swagger funcionando
- Endpoint POST testado
- Histórico retornado pelo GET
- Banco populado
- Comunicação com API Python

---

# 🎥 Demonstração

<p align="center">
  <a href="https://youtu.be/5G9euYeWuxI" target="_blank">
    <img src="https://img.youtube.com/vi/5G9euYeWuxI/maxresdefault.jpg" alt="Vídeo Demonstração Space Telemetry" width="800">
  </a>
</p>

<p align="center">
  ▶️ Clique na imagem para assistir à demonstração completa do projeto
</p>

# 🚀 Evoluções Futuras

- [ ] Dashboard Web
- [ ] Notificações em tempo real
- [ ] Aplicativo Mobile
- [ ] Treinamento contínuo dos modelos
- [ ] Integração com novos sensores
- [ ] Painel de monitoramento geográfico

---

# 🌎 Impacto Ambiental

O Space Telemetry demonstra como tecnologias espaciais, inteligência artificial e sistemas distribuídos podem ser utilizadas para auxiliar a preservação da biodiversidade.

A solução permite monitoramento contínuo, identificação precoce de riscos ambientais e suporte à tomada de decisão por pesquisadores e órgãos ambientais.

---

<p align="center">

<b>FIAP • Global Solution 2026 • Engenharia de Software</b>

</p>
