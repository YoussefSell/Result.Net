# Result.Net

[![](https://img.shields.io/github/license/YoussefSell/Result.Net)](https://github.com/YoussefSell/Result.Net/blob/master/LICENSE)
[![](https://img.shields.io/nuget/v/Result.Net)](https://www.nuget.org/packages/Result.Net/)
![Build](https://github.com/YoussefSell/Result.Net/actions/workflows/ci.yml/badge.svg)

a simple wrapper over an operation execution results to indicate success or failure, instead of throwing exceptions or returning false without explanation.

## Quick setup

to get started install the package using the [NuGet](https://www.nuget.org/packages/Result.Net/) package manager `Install-Package Result.Net`.

## Simple Usage

```csharp
// to create success result
var successResult = Result.Success();

// to create a failure result
var failedResult = Result.Failure();
```

to create a result wrapper for a value you can use `Result<TValue>`

```csharp
// to create success result with float value
var successResult = Result.Success<float>(55.5f);

// to create a failed result for a float value
var failedResult = Result.Failure<float>();
```

you can also add a message associated with the result

```csharp
// to create success result with a message
var successResult = Result.Success<float>(55.5f)
  .WithMessage("Operation Succeeded");

// to create a failed result with a message
var failedResult = Result.Failure<float>()
  .WithMessage("Operation Failed");
  
// to create a failed result with a localized message
var failedResult = Result.Failure<float>()
  .WithLocalizedMessage("some_text_code");
```
you can utilize error codes to give a better explanation of what happens (useful for machine-to-machine communication).

```csharp
// create a failed result with a message and code
var failedResult = Result.Failure()
  .WithMessage("Othe provided email is not valid")
  .WithCode(ResultCode.InvalidEmail);
```

**Note** there is a list of predefined error codes that I use frequently, you can find them in [ResultCode](https://github.com/YoussefSell/Result.Net/blob/main/src/Result.Net/Constants/ResultCode.cs) or simply use any strings value.

you can also provide a ResultError to better define what happened

```csharp
var failedResult = Result.Failure()
  .WithMessage("the provided email is not valid")
  .WithCode("email_validation")
  .WithErrors(new []
  {
    new ResultError(
      message: "the email host is not allowed",
      code: ("invalid_email_host")
  });

// or use the short syntax
var failedResult = Result.Failure()
  .WithMessage("the provided email is not valid")
  .WithCode("email_validation")
  .WithErrors("the email host is not allowed", "invalid_email_host");
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
    return Result.Failure()
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

what about checking your method execution status:

```csharp
var result = DoSomeWork();

// you can use IsSuccess() or IsFailure()
if (result.IsSuccess()) {
  Console.WriteLine("operation succeeded");
}

if (result.IsFailure()) {
  Console.WriteLine("operation failed");
}

// or you can use the Match extension
result.Match(
      // the action to run on success
      onSuccess: result => {
        Console.WriteLine("executed if result has a success status");
      },
      // the action to run on failure
      onFailure: result => {
        Console.WriteLine("executed if result has a failure status");
      });

// you can also return a value
var output = result.Match(
      // the func to run on success
      onSuccess: result => {
        Console.WriteLine("executed if result has a success status");
        return "test";
      },
      // the func to run on failure
      onFailure: result => {
        Console.WriteLine("executed if result has a failure status");
        return "test";
      });
```

for more details check out the [Wiki](https://github.com/YoussefSell/Result.Net/wiki) page.

## Samples

here are some samples of how you can integrate Result.Net with different app types:

- [Console app](https://github.com/YoussefSell/Result.Net/tree/main/samples/Result.Net.Samples.Console)

## Blog posts

here you will find a list of blog posts explaining how to integrate Result.Net in your applications, also if you have written one let's add it here:

- [To Throw or To Return (Exceptions vs Result Object)?](https://youssefsellami.com/exceptions_vs_result_object/)
