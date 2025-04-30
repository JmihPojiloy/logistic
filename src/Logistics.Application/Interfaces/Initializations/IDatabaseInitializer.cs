namespace Logistics.Application.Interfaces.Initializations;

public interface IDatabaseInitializer
{
    Task InitializeAsync(CancellationToken cancellationToken = default);
}