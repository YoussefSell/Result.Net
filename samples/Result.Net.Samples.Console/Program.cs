using ResultNet;

// create a success result
var successResult = Result.Success();
Console.WriteLine(successResult);

// to create a failure result
var failedResult = Result.Failure();
Console.WriteLine(failedResult);

// to create success result with flout value
var successResultForValue = Result.Success<float>(55.5f);
Console.WriteLine(successResultForValue);

// to create a failed result for a float value
var failedResultForValue = Result.Failure<float>();
Console.WriteLine(failedResultForValue);

Result FromException()
{
    try
    {
        throw new DirectoryNotFoundException("test exception");
    }
    catch (Exception ex)
    {
        return ex.ToResult();
    }
}

var failedResultFormException = FromException();
Console.WriteLine(failedResultFormException);