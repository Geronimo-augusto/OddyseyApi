namespace GsApi.Model;

public class BiotelemetryTag : SpaceEquipment
{
    public string SpeciesId { get; set; } = string.Empty;
    public override string TransmitDiagnostic() => $"Tag {ID} operando na espécie {SpeciesId}";
}