using Moq;
using System.Threading.Tasks;
using Xunit;
using RRBank.Application.Services;
using RRBank.Application.Model;
using RRBank.Domain.Database;
using Microsoft.EntityFrameworkCore;
using RRBank.Application.Services.Caching;
using RRBank.Infra;
using System.Linq.Expressions;
using MassTransit;
using System.Net.Sockets;

public class ClientServiceTests
{
    private readonly Mock<DataContext> _mockContext;
    private readonly Mock<ICachingService> _mockCache;
    private readonly ClientService _clientService;

    public ClientServiceTests()
    {
        _mockContext = new Mock<DataContext>();
        _mockCache = new Mock<ICachingService>();
        var mockBus = new Mock<MassTransit.IBus>();  

        _clientService = new ClientService(_mockContext.Object, _mockCache.Object, mockBus.Object);
    }

    [Fact]
    public async Task GetClientByIdAsync_ReturnsClientFromCache_WhenClientIsInCache()
    {
        // Arrange
        int clientId = 1;
        var expectedClient = new Client { Id = clientId, Name = "John Doe" };

        _mockCache.Setup(c => c.GetAsync<Client>(clientId.ToString()))
                  .ReturnsAsync(expectedClient);

        // Act
        var result = await _clientService.GetClientByIdAsync(clientId);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(expectedClient, result.Data);
    }

    //[Fact]
    //public async Task GetClientByIdAsync_ReturnsClientFromDatabase_WhenClientIsNotInCache()
    //{
    //    // Arrange
    //    int clientId = 1;
    //    var expectedClient = new Client { Id = clientId, Name = "John Doe", IsActive = true };

    //    // Simula o cache vazio
    //    _mockCache.Setup(c => c.GetAsync<Client>(clientId.ToString()))
    //              .ReturnsAsync((Client)null);

    //    // Configurando o DbSet mockado
    //    var clientData = new List<Client> { expectedClient }.AsQueryable();
    //    var mockSet = new Mock<DbSet<Client>>();

    //    // Configurando o mock para retornar o expectedClient no FirstOrDefaultAsync
    //    mockSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<Client, bool>>>()))
    //           .ReturnsAsync((Client)null);  // Simula não encontrar o cliente com o filtro

    //    var mockContext = new Mock<DataContext>();
    //    mockContext.Setup(c => c.Clients).Returns(mockSet.Object);

    //    var mockCache = new Mock<ICachingService>();
    //    var mockBus = new Mock<IBus>();

    //    var service = new ClientService(mockContext.Object, mockCache.Object, mockBus.Object);

    //    // Act
    //    var result = await service.GetClientByIdAsync(clientId);

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.True(result.Success);
    //    Assert.Equal(expectedClient, result.Data);
    //}

    //[Fact]
    //public async Task GetClientByIdAsync_ReturnsError_WhenClientNotFoundInDatabase()
    //{
    //    // Arrange
    //    int clientId = 1;

    //    _mockCache.Setup(c => c.GetAsync<Client>(clientId.ToString()))
    //              .ReturnsAsync((Client)null);  // Simula o cache vazio

    //    _mockContext.Setup(c => c.Clients.FirstOrDefaultAsync(It.IsAny<Expression<Func<Client, bool>>>()))
    //                .ReturnsAsync((Client)null);  // Simula a ausência do cliente no banco

    //    // Act
    //    var result = await _clientService.GetClientByIdAsync(clientId);

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.False(result.Success);
    //    Assert.Equal("Cliet not found.", result.Message);
    //}
}
