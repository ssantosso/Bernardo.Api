using Moq;
using SecurityApi.Application.Services;
using SecurityApi.Domain.Entities;
using SecurityApi.Infra.Data.Interfaces;


namespace SecurityApiTests.Services;

[TestFixture]
public class Tests
{
    private Mock<ISecurityPriceRepository> _repoMock;
    private SecurityPriceService _service;
    private SecurityPrice _isinprice;
    private SecurityPrice _isinpriceInvalid;
    private string iSINPrice;
    private string iSINPriceInvalid;
    private IEnumerable<string> listValid;
    private IEnumerable<string> listInValid;
    [SetUp]
    public void Setup()
    {

        _repoMock = new Mock<ISecurityPriceRepository>();
        _service = new SecurityPriceService(_repoMock.Object);
        iSINPrice = "012345678912";
        iSINPriceInvalid = "0123456789131251541";

        listValid = new List<string> { iSINPrice };
        listInValid = new List<string> { iSINPriceInvalid };

        _isinprice = new SecurityPrice( iSINPrice,200.00);

        _isinpriceInvalid = new SecurityPrice(iSINPriceInvalid, 200.00);
        //_repo.GetAllPriceByISIN(iSINPrice).WaitAsync(list);
    }

    [Test]
    public async Task ISIN_WhenIsValidAddPrice_ReturnTrue()
    {
        // Arrange
        _repoMock.Setup(r => r.GetPriceByISIN(iSINPrice)).ReturnsAsync(new SecurityPrice(iSINPrice, 150.00));
        _repoMock.Setup(r => r.AddSecurityPrices(It.IsAny<SecurityPrice>())).Returns(Task.CompletedTask);

        // Act
        var result = await  _service.AddSecurityPrices(listValid);

        // Assert
        Assert.IsTrue(result);
        _repoMock.Verify(r => r.AddSecurityPrices(It.IsAny<SecurityPrice>()), Times.AtLeast(1));
    }

    [Test]
    public async Task ISIN_WhenIsInValid_NotAddPrice()
    {
        // Arrange
        _repoMock.Setup(r => r.GetPriceByISIN(iSINPriceInvalid)).ReturnsAsync(_isinpriceInvalid);
        _repoMock.Setup(r => r.AddSecurityPrices(It.IsAny<SecurityPrice>())).Returns(Task.CompletedTask);

        // Act
        var result = await _service.AddSecurityPrices(listInValid);

        // Assert
        _repoMock.Verify(r => r.AddSecurityPrices(It.IsAny<SecurityPrice>()), Times.Exactly(0));
    }

    [Test]
    public async Task ISIN_WhenOneIsValidAndOneIsInValid_AddJustOnPrice()
    {
        // Arrange
        _repoMock.Setup(r => r.GetPriceByISIN(iSINPriceInvalid)).ReturnsAsync(_isinpriceInvalid);
        _repoMock.Setup(r => r.GetPriceByISIN(iSINPrice)).ReturnsAsync(_isinprice);
        _repoMock.Setup(r => r.AddSecurityPrices(It.IsAny<SecurityPrice>())).Returns(Task.CompletedTask);

        var list = new List<string>();
        list.AddRange(listInValid);
        list.AddRange(listValid);
        // Act
        var result = await _service.AddSecurityPrices(list);

        // Assert
        _repoMock.Verify(r => r.AddSecurityPrices(It.IsAny<SecurityPrice>()), Times.Exactly(1));
    }
}