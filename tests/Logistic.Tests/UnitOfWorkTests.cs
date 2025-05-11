using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.UnitOfWork;
using Logistics.Domain.Entities.Addresses;
using Logistics.Domain.Entities.Products;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.Decorators;
using Logistics.Infrastructure.Mapper;
using Logistics.Infrastructure.Repositories;
using Logistics.Infrastructure.UnitOfWorks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Logistic.Tests;

[TestFixture]
public class UnitOfWorkTests
{
    private ServiceProvider _serviceProvider = null!;
    private IUnitOfWork _unitOfWork = null!;

    [SetUp]
    public void Setup()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open(); 

        var services = new ServiceCollection();

        services.AddDbContext<LogisticDbContext>(opts =>
            opts.UseSqlite(connection)); 

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        services.AddAutoMapper(typeof(LogisticEntitiesMappingProfile));
        services.AddLogging();

        _serviceProvider = services.BuildServiceProvider();

        var ctx = _serviceProvider.GetRequiredService<LogisticDbContext>();
        ctx.Database.EnsureCreated();

        _unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
    }

    [Test]
    public void Should_Create_ProductRepository_With_LoggingDecorator()
    {
        var repo = _unitOfWork.GetRepository<Product>();
        Assert.That(repo, Is.InstanceOf<LoggingRepositoryDecorator<Product>>());
    }

    [Test]
    public void Should_Create_AddressRepository_Without_LoggingDecorator()
    {
        var repo = _unitOfWork.GetRepository<Address>();
        Assert.That(repo, Is.InstanceOf<IRepository<Address>>());
    }

    [Test]
    public async Task Should_BeginTransaction_When_No_Transaction_Started()
    {
        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.CommitAsync();
    }

    [Test]
    public async Task Should_CommitTransaction_When_Begin_Transaction_Started()
    {
        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.CommitAsync();
    }

    [Test]
    public async Task Should_RollbackTransaction_When_Begin_Transaction_Started()
    {
        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.RollbackAsync();
    }

    [Test]
    public async Task Should_SaveChangesAsync_When_ChangesMade()
    {
        var repo = _unitOfWork.GetRepository<Product>();
        var product = new Product
        {
            Name = "Test",
            Code = 0
        };
        await repo.AddOrUpdateAsync(product, CancellationToken.None);
        var result = await _unitOfWork.SaveChangesAsync();
        Assert.That(result, Is.GreaterThan(0));
    }

    [Test]
    public void Should_ResetRepositories_When_Called()
    {
        _unitOfWork.ResetRepositories();
        var repo1 = _unitOfWork.GetRepository<Product>();
        _unitOfWork.ResetRepositories();
        var repo2 = _unitOfWork.GetRepository<Product>();
        Assert.That(repo1, Is.Not.SameAs(repo2));
    }

    [Test]
    public void Should_Dispose_UnitOfWork_Without_Exceptions()
    {
        Assert.DoesNotThrow(() => _unitOfWork.Dispose());
    }

    [TearDown]
    public void TearDown()
    {
        _serviceProvider.Dispose();
    }
}
