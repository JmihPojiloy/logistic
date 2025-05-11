using Logistics.Domain.Entities;

namespace Logistics.Application.Interfaces.Repositories;

/// <summary>
/// Интерфейс фабрики репозиториев
/// </summary>
public interface IRepositoryFactory
{
    IRepository<TDom> CreateRepository<TDom>() where TDom : BaseEntity;
}