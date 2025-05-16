using Logistics.Application.Exceptions;
using Logistics.Application.Filters;
using Logistics.Application.Interfaces.Geo;
using Logistics.Application.Interfaces.Payments;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.UnitOfWork;
using Logistics.Application.Services;
using Logistics.Domain.Entities.Addresses;
using Logistics.Domain.Entities.Delivery;
using Logistics.Domain.Entities.Notifications;
using Logistics.Domain.Entities.Orders;
using Logistics.Domain.Entities.Payments;
using Logistics.Domain.Entities.Products;
using Logistics.Domain.Entities.Promotions;
using Logistics.Domain.Entities.Users;
using Logistics.Domain.Entities.Vehicles;
using Logistics.Domain.Entities.Warehouses;
using Logistics.Domain.Enums;
using Logistics.Domain.ValueObjects;
using Moq;

namespace Logistic.Tests;

[TestFixture]
    public class OrderServiceTests
    {
        private Mock<IUnitOfWork> _uowMock = null!;
        private Mock<IPaymentService> _paymentServiceMock = null!;
        private Mock<IGeoService> _geoServiceMock = null!;
        private OrderService _service = null!;
        private CancellationToken _ct;

        [SetUp]
        public void SetUp()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _paymentServiceMock = new Mock<IPaymentService>();
            _geoServiceMock = new Mock<IGeoService>();
            _service = new OrderService(_uowMock.Object, _paymentServiceMock.Object, _geoServiceMock.Object);
            _ct = CancellationToken.None;
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllOrders()
        {
            var orders = new List<Order> { 
                new Order 
                { 
                    Id = 1, 
                    Address = new Address(), 
                    User = new User
                    {
                        FirstName = null
                    }
            } };
            var repoMock = Mock.Of<IRepository<Order>>(x => x.GetAllByFilterAsync(null, _ct) == Task.FromResult((IReadOnlyList<Order>)orders));
            _uowMock.Setup(x => x.GetRepository<Order>()).Returns(repoMock);

            var result = await _service.GetAllAsync(_ct);

            Assert.That(orders, Is.EqualTo(result));
        }

        [Test]
        public void AddOrUpdateAsync_NoWarehouseAddress_ThrowsOrderException()
        {
            var address = new Address { Country = "Test", City = "City", Street = "Street", HouseNumber = "1" };
            var product = new Product
            {
                Id = 1,
                Price = new Money(100m,
                    Currency.RUB),
                Inventories = new List<Inventory>
                {
                    new Inventory
                    {
                        Warehouse = new Warehouse
                        {
                            Address = null!,
                            Name = null
                        },
                        Product = null
                    }
                },
                Name = null
            };

            var order = new Order
            {
                Id = 2,
                Address = address,
                OrderProducts = new List<OrderProduct> { new OrderProduct { ProductId = 1 } },
                User = new User
                {
                    FirstName = null
                }
            };

            var productRepoMock = Mock.Of<IRepository<Product>>(x => x.GetByIdAsync(1, _ct) == Task.FromResult(product));
            _uowMock.Setup(x => x.GetRepository<Product>()).Returns(productRepoMock);
            _uowMock.Setup(x => x.BeginTransactionAsync(_ct)).Returns(Task.CompletedTask);
            _uowMock.Setup(x => x.RollbackAsync(_ct)).Returns(Task.CompletedTask);

            Assert.ThrowsAsync<OrderException>(async () => await _service.AddOrUpdateAsync(order, _ct));
        }

        [Test]
        public async Task DeleteAsync_DeletesOrder()
        {
            var repoMock = new Mock<IRepository<Order>>();
            repoMock.Setup(r => r.DeleteAsync(1, _ct)).ReturnsAsync(1);
            _uowMock.Setup(x => x.GetRepository<Order>()).Returns(repoMock.Object);

            var result = await _service.DeleteAsync(1, _ct);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task PayOrderAsync_OrderPaid_Success()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Price = new Money(100m, Currency.RUB),
                Inventories = new List<Inventory>
                {
                    new Inventory
                    {
                        Quantity = 10,
                        Warehouse = new Warehouse
                        {
                            Address = new Address(),
                            Name = "Test"
                        },
                        Product = null
                    }
                }
            };

            var order = new Order
            {
                Id = 2,
                AddressId = 1,
                Address = new Address(),
                DeliveryCost = new Money(100m, Currency.RUB),
                OrderProducts = new List<OrderProduct> { new OrderProduct { ProductId = 1, Quantity = 2 } },
                OrderPromotions = new List<OrderPromotion>(),
                User = new User { FirstName = "Ivan" },
                Vehicle = new Vehicle
                {
                    Id = 1,
                    Name = "Тест машина",
                    Orders = new List<Order>(),
                    Status = VehicleStatus.Free
                },
                VehicleId = 1
            };

            var processedOrder = order;
            var promotion = new Promotion { Discount = 10, Description = "Promo" };
            var route = new Route { AddressId = 1, Distance = 120 };
            var vehicle = order.Vehicle;
            var driver = new Driver
            {
                DriverLicense = "abc",
                Status = DriverStatus.Free,
                FirstName = "Test",
                LastName = "Test"
            };

            var orderRepo = Mock.Of<IRepository<Order>>(x => x.GetByIdAsync(2, _ct) == Task.FromResult(processedOrder));
            var productRepo = Mock.Of<IRepository<Product>>(x => x.GetAllByFilterAsync(It.IsAny<ProductFilter>(), _ct) == Task.FromResult<IReadOnlyList<Product>>(new[] { product }));
            var inventoryRepo = new Mock<IRepository<Inventory>>();
            var promotionRepo = Mock.Of<IRepository<Promotion>>(x => x.GetAllByFilterAsync(It.IsAny<PromotionsFilter>(), _ct) == Task.FromResult<IReadOnlyList<Promotion>>(new[] { promotion }));
            var paymentRepo = new Mock<IRepository<Payment>>();
            var routeRepo = Mock.Of<IRepository<Route>>(r => r.GetAllByFilterAsync(It.IsAny<RouteFilter>(), _ct) == Task.FromResult<IReadOnlyList<Route>>(new[] { route }));
            var driverRepo = Mock.Of<IRepository<Driver>>(r => r.GetAllByFilterAsync(It.IsAny<DriverFilter>(), _ct) == Task.FromResult<IReadOnlyList<Driver>>(new[] { driver }));
            var vehicleRepo = Mock.Of<IRepository<Vehicle>>(r => r.GetAllByFilterAsync(It.IsAny<VehicleFilter>(), _ct) == Task.FromResult<IReadOnlyList<Vehicle>>(new[] { vehicle }));
            var deliveryTrackingRepo = new Mock<IRepository<DeliveryTracking>>();
            var deliveryScheduleRepo = new Mock<IRepository<DeliverySchedule>>();
            var notificationRepo = new Mock<IRepository<Notification>>();

            _paymentServiceMock.Setup(p => p.InitPayment(It.IsAny<Payment>())).Returns(123);

            _uowMock.Setup(x => x.GetRepository<Order>()).Returns(orderRepo);
            _uowMock.Setup(x => x.GetRepository<Product>()).Returns(productRepo);
            _uowMock.Setup(x => x.GetRepository<Inventory>()).Returns(inventoryRepo.Object);
            _uowMock.Setup(x => x.GetRepository<Promotion>()).Returns(promotionRepo);
            _uowMock.Setup(x => x.GetRepository<Payment>()).Returns(paymentRepo.Object);
            _uowMock.Setup(x => x.GetRepository<Route>()).Returns(routeRepo);
            _uowMock.Setup(x => x.GetRepository<Driver>()).Returns(driverRepo);
            _uowMock.Setup(x => x.GetRepository<Vehicle>()).Returns(vehicleRepo);
            _uowMock.Setup(x => x.GetRepository<DeliveryTracking>()).Returns(deliveryTrackingRepo.Object);
            _uowMock.Setup(x => x.GetRepository<DeliverySchedule>()).Returns(deliveryScheduleRepo.Object);
            _uowMock.Setup(x => x.GetRepository<Notification>()).Returns(notificationRepo.Object);

            _uowMock.Setup(x => x.SaveChangesAsync(_ct)).ReturnsAsync(1);
            _uowMock.Setup(x => x.BeginTransactionAsync(_ct)).Returns(Task.CompletedTask);
            _uowMock.Setup(x => x.CommitAsync(_ct)).Returns(Task.CompletedTask);

            var result = await _service.PayOrderAsync(order, _ct);

            Assert.That(result.Id, Is.EqualTo(2));
            Assert.That(result.DeliveryCost, Is.Not.Null);
        }
        
        [Test]
        public async Task FillRoute_AssignsDriverVehicleRouteAndLeadTime()
        {
            var order = new Order
            {
                Id = 99,
                AddressId = 1,
                User = null,
                Address = null
            };
            var route = new Route { AddressId = 1, Distance = 120 };
            var vehicle = new Vehicle
            {
                Id = 5,
                Status = VehicleStatus.Free,
                Orders = new List<Order>(),
                Name = null
            };
            var driver = new Driver
            {
                DriverLicense = "123",
                Status = DriverStatus.Free,
                FirstName = null,
                LastName = null
            };

            _uowMock.Setup(x => x.GetRepository<Driver>()).Returns(Mock.Of<IRepository<Driver>>(r => r.GetAllByFilterAsync(It.IsAny<DriverFilter>(), _ct) == Task.FromResult<IReadOnlyList<Driver>>(new[] { driver })));
            _uowMock.Setup(x => x.GetRepository<Vehicle>()).Returns(Mock.Of<IRepository<Vehicle>>(r => r.GetAllByFilterAsync(It.IsAny<VehicleFilter>(), _ct) == Task.FromResult<IReadOnlyList<Vehicle>>(new[] { vehicle })));
            _uowMock.Setup(x => x.GetRepository<Route>()).Returns(Mock.Of<IRepository<Route>>(r => r.GetAllByFilterAsync(It.IsAny<RouteFilter>(), _ct) == Task.FromResult<IReadOnlyList<Route>>(new[] { route })));

            _uowMock.Setup(x => x.BeginTransactionAsync(_ct)).Returns(Task.CompletedTask);
            _uowMock.Setup(x => x.CommitAsync(_ct)).Returns(Task.CompletedTask);

            var fillRouteTask = (Task)typeof(OrderService)
                .GetMethod("FillRoute", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .Invoke(_service, new object[] { order, _ct })!;
            await fillRouteTask;

            Assert.That(vehicle.Status, Is.EqualTo(VehicleStatus.OnRoute));
            Assert.That(driver.Status, Is.EqualTo(DriverStatus.OnRoute));
            Assert.That(route.LeadTime, Is.Not.Null);
        }

        [Test]
        public async Task CreateTrackingAndSchedule_CreatesScheduleAndTracking()
        {
            var order = new Order
            {
                Id = 7,
                VehicleId = 10,
                Vehicle = new Vehicle
                {
                    Name = null
                },
                User = null,
                Address = null
            };
            var route = new Route { LeadTime = TimeSpan.FromHours(2) };

            _uowMock.Setup(x => x.GetRepository<Route>()).Returns(Mock.Of<IRepository<Route>>(r => r.GetAllByFilterAsync(It.IsAny<RouteFilter>(), _ct) == Task.FromResult<IReadOnlyList<Route>>(new[] { route })));
            _uowMock.Setup(x => x.GetRepository<DeliveryTracking>()).Returns(Mock.Of<IRepository<DeliveryTracking>>());
            _uowMock.Setup(x => x.GetRepository<DeliverySchedule>()).Returns(Mock.Of<IRepository<DeliverySchedule>>());
            _uowMock.Setup(x => x.SaveChangesAsync(_ct)).ReturnsAsync(1);

            var method = typeof(OrderService)
                .GetMethod("CreateTrackingAndSchedule", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

            var task = (Task)method.Invoke(_service, new object[] { order, _ct })!;
            await task;
        }
    }