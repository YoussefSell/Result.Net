# Result.Net
a simple wrapper for an operation result to indicate success or failure, instead of throwing exceptions or return false without explanation.

## Quick setup
to get started install the package using the Nuget package manager `Install-Package YoussefSell.Result.Net`.

## Simple Usage
```csharp
// to create success result
var successResult = Result.Success();

// to create a failure result
var failedResult = Result.Failure();
```
to create a result wrapper for a value you can use Result<<TValue>>
```csharp
// to create success result with flout value
var successResult = Result.Success<float>(55.5);

// to create a failed result for a float value
var failedResult = Result.Failure<float>();
```
you can also add a message associated with the result
```csharp
// to create success result with a message
var successResult = Result.Success<float>(55.5)
  .WithMessage("Operation Succeeded");

// to create a failed result with a message
var failedResult = Result.Failed<float>()
  .WithMessage("Operation Failed");
```
you can use error code in order to organize and better document your errors.
```csharp
// create a failed result with a message and code
var failedResult = Result.Failed()
  .WithMessage("Othe provided email is not valid")
  .WithCode(ResultCode.InvalidEmail);
```
**Note** there is a list of predefined error codes that I use frequently, you can find them in [ResultCode](https://github.com/YoussefSell/Result.Net/blob/main/src/Result.Net/Constants/ResultCode.cs).

you can also provide an ResultError to better define what happened
```csharp
var failedResult = Result.Failed()
  .WithMessage("Othe provided email is not valid")
  .WithCode("email_validation")
  .WithError(new []
  {
    new ResultError(
      message: "the email host is not allowed",
      code: "invalid_email_host")
  });
```
if you want to encapsulate an exception you can do the following
```csharp
public Result DoSomeWork()
{
  try
  {
    // you code goes here
    return Result.Success();
  }
  catch(Exception ex)
  {
    return Result.Failed()
      .WithMessage("an internal exception has been thrown")
      .WithCode(ResultCode.OperationFailedException)
      .WithError(ex);
  }
}
```
or you can use a pre-defined method to convert the exception to result object
```csharp
public Result DoSomeWork()
{
  try
  {
    // you code goes here
    return Result.Success();
  }
  catch(Exception ex)
  {
    return ex.ToResult();
  }
}
```
what about logging! by default, if you created a Failed result a LogTraceCode will be generated, and you can include this tracing code in your logs to track the errors.
```csharp
public Result DoSomeWork()
{
  try
  {
    // you code goes here
    return Result.Success();
  }
  catch(Exception ex)
  {
    var result = ex.ToResult();
      
    _logger.LogError(ex, "exception {@info}", new 
    {
      LogTraceCode = result.LogTraceCode
      // + your meta data about the error
    });
    
    return result;
  }
}
```
  
for more details check out the [Wiki](https://github.com/YoussefSell/Result.Net/wiki) page.


  
  
  
