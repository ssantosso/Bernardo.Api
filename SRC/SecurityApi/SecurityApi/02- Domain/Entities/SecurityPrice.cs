using FluentValidation;

namespace SecurityApi.Domain.Entities;
public class SecurityPrice
{
    public string ISIN { get; private set; }
    public double Price { get; private set; }

    public SecurityPrice(){}
    public SecurityPrice(string iSIN, double price)
    {
        ISIN = iSIN;
        Price = price;
    }
    public SecurityPrice(string iSIN) => ISIN = iSIN;

    public void SetPrice(double price) => Price = price;

    public bool IsValid() => new SecurityPriceValidation().Validate(this).IsValid;
    
    //incluindo o validator com uma classe aninhada, porem pode ser incluida em classe separada caso achem necessário
    public class SecurityPriceValidation : AbstractValidator<SecurityPrice>
    {
        public SecurityPriceValidation()
        {
            RuleFor(c => c.ISIN)
                .NotNull().NotEmpty()
                .Length(12)
                .WithMessage("ISIN an alphanumeric code of 12 characters");
        }
    }
}