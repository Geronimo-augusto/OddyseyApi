using AutoMapper;
using GsApi.Data.DTOS;
using GsApi.Model;
using GsApi.Models;
using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;
using System;

namespace GsApi.Profiles;

public class TelemetryProfile : Profile
{
    public TelemetryProfile()
    {
        // Mapeamentos de Telemetria
        CreateMap<TelemetryDTO, Telemetry>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CollectedAt, opt => opt.Ignore());

        // Mapeamentos de Histórico de Previsão
        CreateMap<PredictionHistory, PredictionResponseDTO>();

        // ==========================================
        // MAPEAMENTO COM HERANÇA (EQUIPAMENTOS)
        // ==========================================

        // Ensina o AutoMapper que "SpaceEquipment" pode ser Tag ou Satélite
        CreateMap<SpaceEquipment, SpaceEquipmentDTO>()
            .Include<BiotelemetryTag, BiotelemetryTagDTO>()
            .Include<Satellite, SatelliteDTO>();

        // Mapeamento Bidirecional das classes filhas
        CreateMap<BiotelemetryTag, BiotelemetryTagDTO>().ReverseMap();
        CreateMap<Satellite, SatelliteDTO>().ReverseMap();
    }

}
