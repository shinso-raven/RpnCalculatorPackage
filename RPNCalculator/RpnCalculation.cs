using Optional;

namespace RPNCalculator;

public class RpnCalculation
{
    private readonly Guid _requestId;
    private Option<ResultNumber, string> _response;

    /// <summary>
    /// Try to solve a Revers Polish Notation and returns a response or a result
    /// </summary>
    /// <param name="rpnNotation"></param>
    public RpnCalculation(Option<RpnNotation, string> notation)
    {
        _requestId = Guid.NewGuid();


        notation.MatchNone(obj => { _response = Option.None<ResultNumber, string>(obj); });
        notation.MatchSome(
            obj => _response = ResultNumber.From(obj));
    }

    public string ResultOrError()
    {
        return _response.Match(response => response.Value, errorMsg => errorMsg);
    }
}

public class RpnNotation
{
    // private readonly string _value;

    public string Value { get; }

    private RpnNotation(string rawValue)
    {
        Value = rawValue.Trim();
    }

    public static Option<RpnNotation, string> From(string? rawValue)
    {
        if (rawValue == null)
            return Option.None<RpnNotation, string>("Not input added");

        //TODO: check regex for numbers and operations 
        return Option.Some<RpnNotation, string>(new RpnNotation(rawValue));
    }
}

public class ResultNumber
{
    // private string value;

    public string Value { get; }

    private ResultNumber(string result)
    {
        Value = GetResult(result);
    }

    public static Option<ResultNumber, string> From(RpnNotation notation)
    {
        //TODO: Solves operation
        try
        {
            return Option.Some<ResultNumber, string>(new ResultNumber(notation.Value));
        }
        catch (Exception e)
        {
            return Option.None<ResultNumber, string>("Error trying to solve the operation");
        }
    }

    private Stack<string> _stackOperations = new Stack<string>();

    private string GetResult(string rpnNotation)
    {
        string[] lstOperations = rpnNotation.Split(" ");

        foreach (var operation in lstOperations)
        {
            _stackOperations.Push(operation);
        }

        return GetNumber();
    }

    private string GetNumber()
    {
        string nextOperation = _stackOperations.Peek();

        switch (nextOperation)
        {
            case "+":
                _stackOperations.Pop();
                return Add();
            case "-":
                _stackOperations.Pop();
                return Substract();
            case "*":
                _stackOperations.Pop();
                return Multiplication();
            case "/":
                _stackOperations.Pop();
                return Division();
            default:
                return _stackOperations.Pop();
        }

    }

    private string Add()
    {
        double secondValue = double.Parse(GetNumber());
        double firstvalue = double.Parse(GetNumber());

        return (firstvalue + secondValue).ToString();
    }

    private string Substract()
    {
        double secondValue = double.Parse(GetNumber());
        double firstvalue = double.Parse(GetNumber());

        return (firstvalue - secondValue).ToString();
    }

    private string Multiplication()
    {
        double secondValue = double.Parse(GetNumber());
        double firstvalue = double.Parse(GetNumber());

        return (firstvalue * secondValue).ToString();
    }

    private string Division()
    {
        double secondValue = double.Parse(GetNumber());
        double firstvalue = double.Parse(GetNumber());

        return (firstvalue / secondValue).ToString();
    }
}