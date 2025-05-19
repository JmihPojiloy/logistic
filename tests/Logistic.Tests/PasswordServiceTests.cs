using Logistics.Infrastructure.Security;

namespace Logistic.Tests;

[TestFixture]
public class PasswordServiceTests
{
    private PasswordService _passwordService = null!;

    [SetUp]
    public void SetUp() => _passwordService = new PasswordService();

    [Test]
    public void HashPassword_ShouldReturnHashedPassword()
    {
        var password = "SecurePassword123";
        var hashed = _passwordService.HashPassword(password);

        Assert.That(hashed, Is.Not.Empty);
        Assert.That(password, Is.Not.EqualTo(hashed));
    }

    [Test]
    public void VerifyPassword_ShouldReturnTrue_WhenPasswordMatches()
    {
        var password = "SecurePassword123";
        var hashed = _passwordService.HashPassword(password);

        var result = _passwordService.VerifyPassword(hashed, password);

        Assert.That(result, Is.True);
    }

    [Test]
    public void VerifyPassword_ShouldReturnFalse_WhenPasswordDoesNotMatch()
    {
        var password = "SecurePassword123";
        var hashed = _passwordService.HashPassword(password);

        var result = _passwordService.VerifyPassword(hashed, "WrongPassword");

        Assert.That(result, Is.False);
    }
}