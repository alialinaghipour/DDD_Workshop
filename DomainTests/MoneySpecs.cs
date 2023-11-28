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
    
    [Theory]
    [PositiveRandomRange]
    public void Money_addition_should_return_correct_result(
        decimal value1,
        decimal value2)
    {
        Money money1 = new Money(value1);
        Money money2 = new Money(value2);
        var expectedValue = (money1 + money2).Value;

        Money result = money1 + money2;

        result.Value.Should().Be(expectedValue);
    }
    
    [Theory]
    [RandomMinMaxValue]
    public void Money_less_than_operator_should_return_true_when_left_value_is_less(
        decimal minValue,
        decimal maxValue)
    {
        Money money1 = new Money(minValue);
        Money money2 = new Money(maxValue);

        bool result = money1 < money2;

        result.Should().BeTrue();
    }
    
    [Theory]
    [RandomMinMaxValue]
    public void Money_less_than_operator_should_return_false_when_left_value_is_not_less(
        decimal minValue,
        decimal maxValue)
    { ;
        Money money1 = new Money(maxValue);
        Money money2 = new Money(minValue);

        bool result = money1 < money2;

        result.Should().BeFalse();
    }
    
    [Theory,AutoData]
    public void Money_implicit_conversion_should_convert_decimal_to_money(
        decimal value)
    {
        value = value.ConvertToPositive();

        Money money = value;

        money.Should().NotBeNull();
        money.Value.Should().Be(value);
    }
}