namespace GsApi.Model;

public class Satellite : SpaceEquipment
{
    public double OrbitAltitude { get; set; }
    public override string TransmitDiagnostic() => $"Satélite LEO {ID} operando a {OrbitAltitude}km";
}
