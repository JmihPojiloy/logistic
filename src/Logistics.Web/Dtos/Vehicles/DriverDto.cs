using Logistics.Domain.Enums;

namespace Logistics.Web.Dtos.Vehicles;

/// <summary>
/// Транспортный класс для сущности водитель
/// </summary>
public class DriverDto : BaseDto
{
    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; set; }
    
    /// <summary>
    /// Отчество
    /// </summary>
    public string? MiddleName { get; set; }
    
    /// <summary>
    /// Адрес электронной почты
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    public int PhoneNumber { get; set; }
    
    /// <summary>
    /// Пол
    /// </summary>
    public Gender? Gender { get; set; }
    
    /// <summary>
    /// Номер водительского удостоверения
    /// </summary>
    public required string DriverLicense { get; set; }
    
    /// <summary>
    /// Id автомобиля
    /// </summary>
    public int? VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство автомобиля
    /// </summary>
    public VehicleDto? Vehicle { get; set; }
    
    /// <summary>
    /// Статус водителя
    /// </summary>
    public DriverStatus Status { get; set; }
}