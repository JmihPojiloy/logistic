using Logistics.Domain.Entities.Delivery;

namespace Logistics.Application.Interfaces.Repositories;

public interface IRouteRepository : IRepository<Route>
{
    public Task<Route> GetRouteByAddressId(int id, CancellationToken cancellationToken = default);
}