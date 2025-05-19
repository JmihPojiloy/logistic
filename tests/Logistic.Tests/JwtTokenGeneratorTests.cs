using Logistics.Domain.Entities.Users;
using Logistics.Domain.Enums;
using Logistics.Infrastructure.Security;
using Microsoft.Extensions.Configuration;

namespace Logistic.Tests;

[TestFixture]
public class JwtTokenGeneratorTests
{
    private JwtTokenGenerator _tokenGenerator = null!;

    [SetUp]
    public void SetUp()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            {"JwtSettings:Secret", "super_secret_key_1234567890123456"},
            {"JwtSettings:ExpiresInMinutes", "60"},
            {"JwtSettings:Issuer", "LogisticsApp"},
            {"JwtSettings:Audience", "LogisticsUsers"}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        _tokenGenerator = new JwtTokenGenerator(configuration);
    }

    [Test]
    public void GenerateJwtToken_ShouldReturnToken()
    {
        var credentials = new UserCredential
        {
            Id = 1,
            Phone = 123456789,
            Role = UserRole.Admin
        };

        var token = _tokenGenerator.GenerateJwtToken(credentials);

        Assert.That(token, Is.Not.Null);
        Assert.That(token, Is.Not.Empty);
    }
}