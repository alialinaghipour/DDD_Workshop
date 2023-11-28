using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Services.Spec.Infrastructures;

namespace DomainTests;


public class MoneySpecs
{
    static T A<T>(Func<T, T>? customization = null)
    {
        var t = new Fixture().Create<T>();
        if (null != customization)
            t = customization(t);
        return t;
    }

    Money aValidMoney() => A<Money>(with => new Money(Math.Abs(with.Value)));

    // class MoneyDto
    // {
    //     public decimal Amount { get; set; }
    //     public string Currency1 { get; set; }
    //     public string Currency2 { get; set; }
    //     public string Currency3 { get; set; }
    //     public string Currency4 { get; set; }
    //     public string Currency5 { get; set; }
    //     public string Currency6 { get; set; }
    //     public string Currency7 { get; set; }
    //     public string Currency8 { get; set; }
    // }

    void x()
    {
        // var money = A<MoneyDto>.But(with => new MoneyDto
        // {
        //     Amount = Math.Abs(with.Amount)
        // });

    }

    [Theory, AutoData]
    public void Money_cannot_be_negative(decimal amount)
    => new Action(() =>               //Arrange
       new Money(-Math.Abs(amount))   //Act
       ).Should().Throw<Exception>(); //Assert

    [Theory, AutoData]
    public void Supports_subtraction(uint five)
    {
        //Arrange
        var smallerNumber = aValidMoney();
        var biggerNumber = new Money(smallerNumber.Value + five);

        //Act
        (biggerNumber - smallerNumber)

        //Assert
        .Value.Should().Be(five);
    }
    
    //***************************************
    
    [Theory]
    [RandomMinMaxValue]
    public void Money_subtraction_should_return_correct_result(
        decimal minValue,decimal maxValue)
    {
        Money money1 = new Money(maxValue);
        Money money2 = new Money(minValue);
        decimal expectedValue = (money1 - money2).Value;

        Money result = money1 - money2;

        result.Value.Should().Be(expectedValue);
    }

    [Theory, AutoData]
    public void Money_can_not_be_negative(decimal amount)
    {
        amount = amount.ConvertToNegative();

        var actual = () => new Money(amount);

        actual.Should().Throw<Exception>();
    } 
}