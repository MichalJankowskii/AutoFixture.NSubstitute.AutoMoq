# Objectivity.AutoFixture.XUnit2.AutoMoq

Accelerates preparation of mocked structures for unit tests under  [XUnit2](http://xunit.github.io/) by configuring [AutoFixture](https://github.com/AutoFixture/AutoFixture) data generation to use [NSubstitute](http://nsubstitute.github.io/). Gracefully handles recursive structures by omitting recursions.
It provides 3 attributes:
- AutoMoqData
- InlineAutoMoqData
- MemberAutoMoqData

#Attributes
##AutoMoqData
Provides auto-generated data specimens generated by [AutoFixture](https://github.com/AutoFixture/AutoFixture) with [NSubstitute](http://nsubstitute.github.io/) as an extension to xUnit.net's Theory attribute.

#####Arguments:
- IgnoreVirtualMembers - disables generation members marked as `virtual`; by default set to `false`

#####Example:
```csharp
[Theory]
[AutoMoqData]
public void GivenCurrencyConverter_WhenConvertToPln_ThenMustReturnCorrectConvertedAmount(
	string testCurrencySymbol,
    [Frozen] ICurrencyExchangeProvider currencyProvider,
    CurrencyConverter currencyConverter)
{
	// Arrange
    Mock.Get(currencyProvider).Setup(cp => cp.GetCurrencyExchangeRate(testCurrencySymbol)).Returns(100M);

    // Act 
    decimal result = currencyConverter.ConvertToPln(testCurrencySymbol, 100M);

	// Assert 
    Assert.Equal(10000M, result);
}
```

##InlineAutoMoqData
Provides a data source for a data theory, with the data coming from inline values combined with auto-generated data specimens generated by [AutoFixture](https://github.com/AutoFixture/AutoFixture) with [NSubstitute](http://nsubstitute.github.io/).
#####Arguments:
- IgnoreVirtualMembers - disables generation members marked as `virtual`; by default set to `false`

#####Example:
```csharp
[Theory]
[InlineAutoMoqData("USD", 3, 10, 30)]
[InlineAutoMoqData("EUR", 4, 20, 80)]
public void GivenCurrencyConverter_WhenConvertToPln_ThenMustReturnCorrectConvertedAmount(
	string testCurrencySymbol,
    decimal exchangeRate,
	decimal currencyAmount,
    decimal expectedPlnAmount,
    [Frozen] ICurrencyExchangeProvider currencyProvider,
    CurrencyConverter currencyConverter)
{
	// Arrange
    Mock.Get(currencyProvider).Setup(cp => cp.GetCurrencyExchangeRate(testCurrencySymbol)).Returns(exchangeRate);

    // Act 
    decimal result = currencyConverter.ConvertToPln(testCurrencySymbol, currencyAmount);

    // Assert 
    Assert.Equal(expectedPlnAmount, result);
}
```

##MemberAutoMoqData
Provides a data source for a data theory, with the data coming from one of the following sources:
- A static property
- A static field
- A static method (with parameters)

combined with auto-generated data specimens generated by [AutoFixture](https://github.com/AutoFixture/AutoFixture) with [NSubstitute](http://nsubstitute.github.io/).

The member must return something compatible with `Enumerable<object[]>` with the test data.
**Caution:** The property is completely enumerated by .ToList() before any test is run. Hence it should return independent object sets.
#####Arguments:
- IgnoreVirtualMembers - disables generation members marked as `virtual`; by default set to `false`
- ShareFixture - indicates whether to share a `fixture` across all data items should be used or new one; by default set to `true`

#####Example:
```csharp
public class CurrencyConverterFixture
{
	public static IEnumerable<object[]> CurrencyConversionRatesWithResult()
    {
    	return new List<object[]>
        	{
            	new object[] { "USD", 3M, 10M, 30M },
                new object[] { "EUR", 4M, 20M, 80M }
            };
    }
}

...

[Theory]
[MemberAutoMoqData("CurrencyConversionRatesWithResult", MemberType = typeof(CurrencyConverterFixture))]
public void GivenCurrencyConverter_WhenConvertToPln_ThenMustReturnCorrectConvertedAmount(
	string testCurrencySymbol,
	decimal exchangeRate,
	decimal currencyAmount,
	decimal expectedPlnAmount,
	[Frozen] ICurrencyExchangeProvider currencyProvider,
	CurrencyConverter currencyConverter)
{
	// Arrange
    Mock.Get(currencyProvider).Setup(cp => cp.GetCurrencyExchangeRate(testCurrencySymbol)).Returns(exchangeRate);

    // Act 
    decimal result = currencyConverter.ConvertToPln(testCurrencySymbol, currencyAmount);

    // Assert 
    Assert.Equal(expectedPlnAmount, result);
}
```

#Tips and tricks
##Fixture injection
You can inject same instance of `IFixture` to a test method by adding mentioned interface as an argument of test method.
```csharp
[Theory]
[AutoMoqData]
public void FixtureInjection(IFixture fixture)
{
	Assert.NotNull(fixture);
}
```

## IgnoreVirtualMembers issue
You should be aware that the *CLR* requires that interface methods be marked as virtual. Please look at the following example:
```csharp
public interface IUser
{
	string Name { get; set; }
	User Substitute { get; set; }
	string Surname { get; set; }
}

public class User : IUser
{
	public string Name { get; set; }
	public virtual User Substitute { get; set; }
	public string Surname { get; set; }
}
```
You can see than only `Substitute` property has been explicitly marked as `virtual`. In such situation *the compiler* will mark other properties as `virtual` and `sealed`. And finally *AutoFixture* will assign `null` value to those properties when option `IgnoreVirtualMembers` will be set to `true`.

```csharp
[Theory]
[AutoMoqData(IgnoreVirtualMembers = true)]
public void IssueWithClassThatImplementsInterface(User user)
{
	Assert.Null(user.Name);
    Assert.Null(user.Surname);
    Assert.Null(user.Substitute);
}
```