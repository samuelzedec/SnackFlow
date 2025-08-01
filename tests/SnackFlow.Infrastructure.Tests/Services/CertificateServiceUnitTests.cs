using FluentAssertions;
using SnackFlow.Domain.Tests;
using SnackFlow.Infrastructure.Services;

namespace SnackFlow.Infrastructure.Tests.Services;

public class CertificateServiceUnitTests : BaseTest
{
    #region Setup
    
    private readonly CertificateService _certificateService = new();

    #endregion
    
    #region LoadCertificateAsync Tests

    [Fact(DisplayName = "Should throw exception when certificate file does not exist")]
    public void LoadCertificateAsync_WhenFileDoesNotExist_ShouldThrowException()
    {
        // Arrange
        var nonExistentFile = "non-existent-certificate.pfx";
        var password = "test-password";
        
        // Act
        var act = () => _certificateService.LoadCertificate(nonExistentFile, password);
        
        // Assert
        act.Should().Throw<Exception>();
    }

    [Fact(DisplayName = "Should throw exception when key parameter is null")]
    public void LoadCertificateAsync_WhenKeyIsNull_ShouldThrowException()
    {
        // Arrange
        var password = "test-password";
        
        // Act
        var act = () => _certificateService.LoadCertificate(string.Empty, password);
        
        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact(DisplayName = "Should throw exception when key parameter is empty")]
    public void LoadCertificate_WhenKeyIsEmpty_ShouldThrowException()
    {
        // Arrange
        var emptyKey = string.Empty;
        var password = "test-password";
        
        // Act
        var act = () => _certificateService.LoadCertificate(emptyKey, password);
        
        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact(DisplayName = "Should throw exception when password is null")]
    public void LoadCertificate_WhenPasswordIsNull_ShouldThrowException()
    {
        // Arrange
        var key = "test-certificate.pfx";
        
        // Act
        var act = () => _certificateService.LoadCertificate(key, string.Empty);
        
        // Assert
        act.Should().Throw<Exception>();
    }

    #endregion
}