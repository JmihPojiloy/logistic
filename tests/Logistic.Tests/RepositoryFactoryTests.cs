using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities;
using Logistics.Domain.Entities.Orders;
using Logistics.Domain.Entities.Products;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.Mapper;
using Logistics.Infrastructure.Repositories;
using Logistics.Infrastructure.Repositories.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Logistic.Tests;

[TestFixture]
public class RepositoryFactoryTests
{
    private ServiceProvider _serviceProvider = null!;
    private IRepositoryFactory _repositoryFactory = null!;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddDbContext<LogisticDbContext>(opts => opts.UseInMemoryDatabase("TestDb"));
        services.AddAutoMapper(typeof(LogisticEntitiesMappingProfile));
        services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        _serviceProvider = services.BuildServiceProvider();
        _repositoryFactory = _serviceProvider.GetRequiredService<IRepositoryFactory>();
    }
    
    [Test]
    public void Should_Create_GenericRepository_For_Product()
    {
        var repo = _repositoryFactory.CreateRepository<Product>();
        Assert.That(repo, Is.InstanceOf<IRepository<Product>>());
    }

    [Test]
    public void Should_Create_CustomRepository_For_Order()
    {
        var repo = _repositoryFactory.CreateRepository<Order>();
        Assert.That(repo, Is.InstanceOf<OrderRepository>());
        Assert.That(repo, Is.InstanceOf<IOrderRepository>());
        Assert.That(repo, Is.InstanceOf<IRepository<Order>>());
    }
    
    [Test]
    public void Should_Throw_For_Unknown_Type()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            _repositoryFactory.CreateRepository<UnknownEntity>();
        });
    }

    private class UnknownEntity : BaseEntity { }
    
    [TearDown]
    public void TearDown()
    {
        _serviceProvider.Dispose();
    }
}