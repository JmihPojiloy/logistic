using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.Security;
using Logistics.Application.Interfaces.Services;
using Logistics.Application.Interfaces.UnitOfWork;
using Logistics.Application.Services;
using Logistics.Domain.Entities.Users;
using Moq;

namespace Logistic.Tests;

[TestFixture]
public class AuthServiceTests
{
    private Mock<IUserCredentialRepository> _credentialRepo = null!;
    private Mock<IUnitOfWork> _unitOfWork = null!;
    private Mock<IPasswordService> _passwordService = null!;
    private Mock<IJwtTokenGenerator> _tokenGenerator = null!;
    private AuthService _authService = null!;

    [SetUp]
    public void SetUp()
    {
        _credentialRepo = new Mock<IUserCredentialRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _passwordService = new Mock<IPasswordService>();
        _tokenGenerator = new Mock<IJwtTokenGenerator>();

        _authService = new AuthService(
            _credentialRepo.Object,
            _unitOfWork.Object,
            _passwordService.Object,
            _tokenGenerator.Object);
    }

    [Test]
    public async Task CheckCredentialAsync_ShouldReturnTrue_WhenPasswordMatches()
    {
        var credential = new UserCredential { PasswordHash = "hash", Phone = 123 };
        _credentialRepo.Setup(r => r.GetByPhoneAsync(123, It.IsAny<CancellationToken>())).ReturnsAsync(credential);
        _passwordService.Setup(p => p.VerifyPassword("hash", "password")).Returns(true);

        var result = await _authService.CheckCredentialAsync(123, "password", CancellationToken.None);

        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckCredentialAsync_ShouldThrow_WhenUserNotFound()
    {
        _credentialRepo.Setup(r => r.GetByPhoneAsync(123, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserCredential?)null);

        Assert.ThrowsAsync<NotFoundException>(() =>
            _authService.CheckCredentialAsync(123, "pass", CancellationToken.None));
    }

    [Test]
    public async Task LoginAsync_ShouldReturnToken_WhenCredentialsValid()
    {
        var credential = new UserCredential { PasswordHash = "hash", Phone = 123 };
        _credentialRepo.Setup(r => r.GetByPhoneAsync(123, It.IsAny<CancellationToken>())).ReturnsAsync(credential);
        _passwordService.Setup(p => p.VerifyPassword("hash", "pass")).Returns(true);
        _tokenGenerator.Setup(t => t.GenerateJwtToken(credential)).Returns("jwt_token");

        var result = await _authService.LoginAsync(123, "pass", CancellationToken.None);

        Assert.That(result, Is.EqualTo("jwt_token"));
    }

    [Test]
    public void LoginAsync_ShouldThrow_WhenInvalidCredentials()
    {
        var credential = new UserCredential { PasswordHash = "hash", Phone = 123 };
        _credentialRepo.Setup(r => r.GetByPhoneAsync(123, It.IsAny<CancellationToken>())).ReturnsAsync(credential);
        _passwordService.Setup(p => p.VerifyPassword("hash", "wrong")).Returns(false);

        Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            _authService.LoginAsync(123, "wrong", CancellationToken.None));
    }
}