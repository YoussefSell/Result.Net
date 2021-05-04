# Result.Net
a simple wrapper for an operation result to indicate success or failure, instead of throwing exceptions or return false without explanation.
### Simple Usage
```csharp
// to create success result
var successResult = Result.Success();

// to create a failed result
var failedResult = Result.Failed();
```
to create a result wrapper for a value you can use Result<<TValue>>
```csharp
// to create success result with flout value
var successResult = Result.Success<float>(55.5);

// to create a failed result for a float value
var failedResult = Result.Failed<float>();
```
you can also define a message associated with the result in bout cases (success and failure)
```csharp
// to create success result with a message
var successResult = Result.Success<float>(55.5)
  .WithMessage("Operation Succeeded");

// to create a failed result with a message
var failedResult = Result.Failed<float>()
  .WithMessage("Operation Failed");
```
**Note** if no message is provided the default messages are "Operation Succeeded" for success, and "Operation Failed" for failure.
in case of an error, you can specie an error code to describe what happened.
```csharp
// create a failed result with a message and code
var failedResult = Result.Failed()
  .WithMessage("Othe provided email is not valid")
  .WithCode(ResultCode.InvalidEmail);
```
**Note** there is a list of predefined error codes that I use frequently you can find at [ResultCode](https://github.com/YoussefSell/Result.Net/blob/main/src/Result.Net/Constants/ResultCode.cs).


you can also provide an ResultError to better define what happened
```csharp
var failedResult = Result.Failed()
  .WithMessage("Othe provided email is not valid")
  .WithCode("invalid_email")
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
    var result = Result.Failed()
      .WithMessage("an internal exception has been thrown")
      .WithCode(ResultCode.OperationFailedException)
      .WithError(ex);
      
    _logger.LogError(ex, "exception {@info}", new 
    {
      LogTraceCode = result.LogTraceCode
      // + your meta data about the error
    });
    
    return result;
  }
}
```


  
  
  
