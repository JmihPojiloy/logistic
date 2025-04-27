namespace Logistics.Domain.Enums;

public enum LetterStatus
{
    InQueue = 0,
    Error = 1,
    Success = 2,
    NotWhiteAddress = 3,
    Duplicate = 4,
    InvalidEmail = 5,
    Unsubscribed = 6
}