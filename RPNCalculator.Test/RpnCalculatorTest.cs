using Optional;

namespace RPNCalculator.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }


    /*
        => 20/5 = 4
4 2 + 3 -     => (4+2)-3 = 3
3 5 8 * 7 + * => ((5*8)+7)*3 = 141

9 SQRT => √9 = 3

4 5 MAX 1 +     => MAX(4, 5) + 1 = 6
    */


    [TestCase("20 5 /", "4")]
    [TestCase("20 5 +", "25")]
    [TestCase("20 5 -", "15")]
    [TestCase("20 5 *", "100")]
    [TestCase("100 5 /", "20")]
    [TestCase("20 4 / ", "5")]
    public void Calculator_solves_basic_operations(string input, string expectedResult)
    {
        var objInput = RpnNotation.From(input);
        var operation = new RpnCalculation(objInput);

        var result = operation.ResultOrError();
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    public void Calculator_returns_error(string input)
    {
        string expectedResult = "";

        var objInput = RpnNotation.From(input);
        var operation = new RpnCalculation(objInput);

        var result = operation.ResultOrError();
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}