using AstronnERP.Domain.SharedObjects.ValueObjects;
using AstronnERP.Domain.SharedObjects.Enums;

namespace AstronnERP.Domain.UnitTests;

public class Shared_Price
{
    [Fact]
    public void Price_WhenValueBelowZero_ShouldReturnIsFailedTrue()
    {
        var result = Price.Create(-10, Currency.USD);

        Assert.IsNotType<Price>(result);
        Assert.True(result.IsFailed);
        Assert.Single(result.Errors);
        Assert.Equal("Price must be greater than zero.", result.Errors[0].Message);
    }

    [Fact]
    public void Price_WhenCurrencyIsNotInEnum_ShouldReturnIsFailedTrue()
    {
        var result = Price.Create(10, (Currency)999);

        Assert.IsNotType<Price>(result);
        Assert.True(result.IsFailed);
        Assert.Single(result.Errors);
        Assert.Equal("Currency must be of expected list.", result.Errors[0].Message);
    }

    [Fact]
    public void Price_WhenBothValueAndCurrencyWrong_ShouldReturnTwoErrors()
    {
        var result = Price.Create(-10, (Currency)999);

        Assert.IsNotType<Price>(result);
        Assert.True(result.IsFailed);
        Assert.Equal(2, result.Errors.Count);
        Assert.Equal("Price must be greater than zero.", result.Errors[0].Message);
        Assert.Equal("Currency must be of expected list.", result.Errors[1].Message);
    }

    [Fact]
    public void Price_WhenBothValueAndCurrencyValid_ShouldReturnPrice()
    {
        var result = Price.Create(10, Currency.USD);

        Assert.IsType<Price>(result.ValueOrDefault);
        Assert.Equal(10, result.Value.Value);
        Assert.Equal(Currency.USD, result.Value.Currency);
    }
}
