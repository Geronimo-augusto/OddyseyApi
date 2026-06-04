namespace OddyseyApi.Data.DTOS;

public abstract class SpaceEquipmentDTO
{
    public Guid Id { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
