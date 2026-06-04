using System;

namespace GsApi.Profiles;

public class TelemetryProfile : Profile
{
    public TelemetryProfile()
    {
        // Mapeia o DTO de entrada para a Entidade, ignorando propriedades geradas internamente (como o Id)
        CreateMap<TelemetryDto, Telemetry>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CollectedAt, opt => opt.Ignore());

        // Caso precise enviar a entidade de volta em consultas futuras
        CreateMap<Telemetry, TelemetryDto>();
    }

}
