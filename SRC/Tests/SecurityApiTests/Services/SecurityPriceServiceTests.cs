using Moq;
using SecurityApi.Application.Interfaces;
using SecurityApi.Application.Services;
using SecurityApi.Domain.Entities;
using SecurityApi.Infra.Data.Interfaces;
using System.Diagnostics;


namespace SecurityApiTests.Services;

[TestFixture]
public class Tests
{
    private Mock<ISecurityPriceRepository> _repoMock;
    private SecurityPriceService _service;
    private List<SecurityPrice> list;
    private List<SecurityPrice> listInvalid;
    private string iSINPrice;
    private string iSINPriceInvalid;

    [SetUp]
    public void Setup()
    {

        _repoMock = new Mock<ISecurityPriceRepository>();
        _service = new SecurityPriceService(_repoMock.Object);
        iSINPrice = "012345678912";
        iSINPriceInvalid = "0123456789131251541";
        list = new List<SecurityPrice>
            {
                new SecurityPrice( iSINPrice,150.00),
                new SecurityPrice( iSINPrice,200.00) 
            };

        listInvalid = new List<SecurityPrice>
            {
                new SecurityPrice( iSINPriceInvalid,150.00)
            };
        list.Add(new SecurityPrice(iSINPrice, 152.00));
        //_repo.GetAllPriceByISIN(iSINPrice).WaitAsync(list);
    }

    [Test]
    public async Task ISIN_WhenIsValidAddPrice_ReturnTrue()
    {
        // Arrange
        _repoMock.Setup(r => r.GetAllPriceByISIN(iSINPrice)).ReturnsAsync(list);
        _repoMock.Setup(r => r.AddSecurityPrices(It.IsAny<SecurityPrice>())).Returns(Task.CompletedTask);

        // Act
        var result = await  _service.AddSecurityPrices(iSINPrice);

        // Assert
        Assert.IsTrue(result);
        _repoMock.Verify(r => r.AddSecurityPrices(It.IsAny<SecurityPrice>()), Times.AtLeast(2));
    }

    [Test]
    public async Task ISIN_WhenIsInValid_ReturnFalse()
    {
        // Arrange
        _repoMock.Setup(r => r.GetAllPriceByISIN(iSINPriceInvalid)).ReturnsAsync(listInvalid);
        _repoMock.Setup(r => r.AddSecurityPrices(It.IsAny<SecurityPrice>())).Returns(Task.CompletedTask);

        // Act
        var result = await _service.AddSecurityPrices(iSINPriceInvalid);

        // Assert
        _repoMock.Verify(r => r.AddSecurityPrices(It.IsAny<SecurityPrice>()), Times.Exactly(0));
        Assert.IsTrue(result);
    }
}