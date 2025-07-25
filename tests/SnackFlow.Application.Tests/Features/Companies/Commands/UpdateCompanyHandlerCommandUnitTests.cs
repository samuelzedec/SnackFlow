using System.Linq.Expressions;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SnackFlow.Application.Exceptions;
using SnackFlow.Application.Features.Companies.Commands.UpdateCompany;
using SnackFlow.Domain.Constants;
using SnackFlow.Domain.Entities;
using SnackFlow.Domain.Enums;
using SnackFlow.Domain.Repositories;
using SnackFlow.Domain.Tests;
using SnackFlow.Domain.ValueObjects.Email;

namespace SnackFlow.Application.Tests.Features.Companies.Commands;

public class UpdateCompanyHandlerCommandUnitTests : BaseTest
{
    #region Setup

    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<ICompanyRepository> _mockCompanyRepository;
    private readonly Mock<ILogger<UpdateCompanyCommandHandler>> _mockLogger;
    private readonly UpdateCompanyCommandHandler _commandHandler;
    private readonly Company _baseCompany;

    public UpdateCompanyHandlerCommandUnitTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockCompanyRepository = new Mock<ICompanyRepository>();
        _mockLogger = new Mock<ILogger<UpdateCompanyCommandHandler>>();

        _commandHandler = new UpdateCompanyCommandHandler(
            _mockUnitOfWork.Object,
            _mockLogger.Object
        );

        _baseCompany = Company.Create(
            _faker.Person.FullName,
            _faker.Company.CompanyName(),
            _faker.Person.Cpf(),
            _faker.Person.Email,
            _faker.Phone.PhoneNumber("(92) 9####-####"),
            TaxIdType.IndividualWithCpf
        );
    }

    #endregion

    #region Success Tests

    [Fact(DisplayName = "Should update company successfully when all data is valid")]
    public async Task Handle_WhenAllDataIsValid_ShouldUpdateCompanySuccessfully()
    {
        // Arrange
        var request = new UpdateCompanyCommand(
            CompanyId: Guid.CreateVersion7(),
            Email: _faker.Person.Email,
            Phone: _faker.Phone.PhoneNumber("(11) 9####-####"),
            FantasyName: _faker.Company.CompanyName()
        );

        _mockUnitOfWork
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()));

        _mockCompanyRepository
            .Setup(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_baseCompany);

        _mockUnitOfWork
            .Setup(x => x.Companies)
            .Returns(_mockCompanyRepository.Object);

        _mockCompanyRepository
            .Setup(x => x.Update(It.IsAny<Company>()));

        _mockUnitOfWork
            .Setup(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()));

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.Value.Email.Should().Be(Email.Standardization(request.Email));
        result.Value.Phone.Should().Be(request.Phone);
        result.Value.FantasyName.Should().Be(request.FantasyName);

        _mockUnitOfWork.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);

        _mockCompanyRepository.Verify(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _mockUnitOfWork.Verify(x => x.Companies, Times.Exactly(2));
        _mockCompanyRepository.Verify(x => x.Update(It.IsAny<Company>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Should update only email when only email is provided")]
    public async Task Handle_WhenOnlyEmailProvided_ShouldUpdateOnlyEmail()
    {
        // Arrange
        var request = new UpdateCompanyCommand(
            CompanyId: Guid.CreateVersion7(),
            Email: _faker.Person.Email,
            Phone: string.Empty,
            FantasyName: string.Empty
        );

        _mockUnitOfWork
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()));

        _mockCompanyRepository
            .Setup(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_baseCompany);

        _mockUnitOfWork
            .Setup(x => x.Companies)
            .Returns(_mockCompanyRepository.Object);

        _mockCompanyRepository
            .Setup(x => x.Update(It.IsAny<Company>()));

        _mockUnitOfWork
            .Setup(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()));

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.Value.Email.Should().Be(Email.Standardization(request.Email));
        result.Value.Phone.Should().Be(_baseCompany.Phone);
        result.Value.FantasyName.Should().Be(_baseCompany.Name);

        _mockUnitOfWork.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);

        _mockCompanyRepository.Verify(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _mockUnitOfWork.Verify(x => x.Companies, Times.Once);
        _mockCompanyRepository.Verify(x => x.Update(It.IsAny<Company>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Should update only phone when only phone is provided")]
    public async Task Handle_WhenOnlyPhoneProvided_ShouldUpdateOnlyPhone()
    {
        // Arrange
        var request = new UpdateCompanyCommand(
            CompanyId: Guid.CreateVersion7(),
            Email: string.Empty,
            Phone: _faker.Phone.PhoneNumber("(11) 9####-####"),
            FantasyName: string.Empty
        );

        _mockUnitOfWork
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()));

        _mockCompanyRepository
            .Setup(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_baseCompany);

        _mockUnitOfWork
            .Setup(x => x.Companies)
            .Returns(_mockCompanyRepository.Object);

        _mockCompanyRepository
            .Setup(x => x.Update(It.IsAny<Company>()));

        _mockUnitOfWork
            .Setup(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()));

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.Value.Email.Should().Be(_baseCompany.Email);
        result.Value.Phone.Should().Be(request.Phone);
        result.Value.FantasyName.Should().Be(_baseCompany.Name);

        _mockUnitOfWork.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);

        _mockCompanyRepository.Verify(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _mockUnitOfWork.Verify(x => x.Companies, Times.Exactly(2));
        _mockCompanyRepository.Verify(x => x.Update(It.IsAny<Company>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Should update only trade name when only fantasy name is provided")]
    public async Task Handle_WhenOnlyFantasyNameProvided_ShouldUpdateOnlyFantasyName()
    {
        // Arrange
        var request = new UpdateCompanyCommand(
            CompanyId: Guid.CreateVersion7(),
            Email: string.Empty,
            Phone: string.Empty,
            FantasyName: _faker.Company.CompanyName()
        );

        _mockUnitOfWork
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()));

        _mockCompanyRepository
            .Setup(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_baseCompany);

        _mockUnitOfWork
            .Setup(x => x.Companies)
            .Returns(_mockCompanyRepository.Object);

        _mockCompanyRepository
            .Setup(x => x.Update(It.IsAny<Company>()));

        _mockUnitOfWork
            .Setup(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()));

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.Value.Email.Should().Be(_baseCompany.Email);
        result.Value.Phone.Should().Be(_baseCompany.Phone);
        result.Value.FantasyName.Should().Be(request.FantasyName);

        _mockUnitOfWork.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);

        _mockCompanyRepository.Verify(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _mockUnitOfWork.Verify(x => x.Companies, Times.Once);
        _mockCompanyRepository.Verify(x => x.Update(It.IsAny<Company>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region Not Found Tests

    [Fact(DisplayName = "Should throw NotFoundException when company is not found")]
    public async Task Handle_WhenCompanyNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var request = new UpdateCompanyCommand(
            CompanyId: Guid.CreateVersion7(),
            Email: _faker.Person.Email,
            Phone: _faker.Phone.PhoneNumber("(11) 9####-####"),
            FantasyName: _faker.Company.CompanyName()
        );

        _mockUnitOfWork
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()));

        _mockCompanyRepository
            .Setup(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Company?)null);

        _mockUnitOfWork
            .Setup(x => x.Companies)
            .Returns(_mockCompanyRepository.Object);

        _mockUnitOfWork
            .Setup(x => x.RollbackTransactionAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = async () => await _commandHandler.Handle(request, CancellationToken.None);

        await result.Should()
            .ThrowExactlyAsync<NotFoundException>()
            .WithMessage(ErrorMessage.NotFound.Company);

        _mockUnitOfWork.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockCompanyRepository.Verify(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _mockUnitOfWork.Verify(x => x.Companies, Times.Once);
        _mockCompanyRepository.Verify(x => x.Update(It.IsAny<Company>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.RollbackTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);

        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(ErrorMessage.NotFound.Company)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    #endregion

    #region Cancellation Tests

    [Fact(DisplayName = "Should respect cancellation token during company search")]
    public async Task Handle_WhenCancellationTokenDuringCompanySearch_ShouldRespectCancellationToken()
    {
        // Arrange
        var command = new UpdateCompanyCommand(
            Guid.NewGuid(),
            _faker.Person.Email,
            _faker.Phone.PhoneNumber("(11) 9####-####"),
            _faker.Company.CompanyName()
        );

        var mockCancellationToken = new CancellationToken(true);

        _mockCompanyRepository
            .Setup(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.Is<CancellationToken>(ct => ct.IsCancellationRequested)))
            .ThrowsAsync(new OperationCanceledException());

        _mockUnitOfWork
            .Setup(x => x.Companies)
            .Returns(_mockCompanyRepository.Object);

        _mockUnitOfWork
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = async () => await _commandHandler.Handle(command, mockCancellationToken);

        // Assert
        await result.Should()
            .ThrowAsync<OperationCanceledException>();

        _mockCompanyRepository.Verify(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.Is<CancellationToken>(ct => ct.IsCancellationRequested)),
            Times.Once);

        _mockUnitOfWork.Verify(x => x.BeginTransactionAsync(
            It.Is<CancellationToken>(ct => ct.IsCancellationRequested)), Times.Once);

        _mockUnitOfWork.Verify(x => x.RollbackTransactionAsync(
            It.IsAny<CancellationToken>()), Times.Once);

        _mockCompanyRepository.Verify(x => x.Update(It.IsAny<Company>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact(DisplayName = "Should respect cancellation token during conflict check")]
    public async Task Handle_WhenCancellationTokenDuringConflictCheck_ShouldRespectCancellationToken()
    {
        // Arrange
        var companyId = Guid.NewGuid();
        var command = new UpdateCompanyCommand(
            companyId,
            _faker.Person.Email,
            _faker.Phone.PhoneNumber("(11) 9####-####"),
            _faker.Company.CompanyName()
        );

        var mockCancellationToken = new CancellationToken(true);

        _mockCompanyRepository
            .Setup(x => x.GetSingleAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_baseCompany);

        _mockCompanyRepository
            .Setup(x => x.ExistsAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.Is<CancellationToken>(ct => ct.IsCancellationRequested)))
            .ThrowsAsync(new OperationCanceledException());

        _mockUnitOfWork
            .Setup(x => x.Companies)
            .Returns(_mockCompanyRepository.Object);

        _mockUnitOfWork
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = async () => await _commandHandler.Handle(command, mockCancellationToken);

        // Assert
        await result.Should()
            .ThrowAsync<OperationCanceledException>();

        _mockCompanyRepository.Verify(x => x.GetSingleAsync(
            It.IsAny<Expression<Func<Company, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Once);

        _mockCompanyRepository.Verify(x => x.ExistsAsync(
                It.IsAny<Expression<Func<Company, bool>>>(),
                It.Is<CancellationToken>(ct => ct.IsCancellationRequested)),
            Times.Once);

        _mockUnitOfWork.Verify(x => x.RollbackTransactionAsync(
            It.IsAny<CancellationToken>()), Times.Once);

        _mockCompanyRepository.Verify(x => x.Update(It.IsAny<Company>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion
}