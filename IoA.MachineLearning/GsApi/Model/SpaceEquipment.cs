namespace GsApi.Model;

public abstract class SpaceEquipment
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;

    // Método polimórfico
    public abstract string TransmitDiagnostic();
}
