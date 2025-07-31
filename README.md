# Result.Net

[![](https://img.shields.io/github/license/YoussefSell/Result.Net)](https://github.com/YoussefSell/Result.Net/blob/master/LICENSE)
[![](https://img.shields.io/nuget/v/Result.Net)](https://www.nuget.org/packages/Result.Net/)
![Build](https://github.com/YoussefSell/Result.Net/actions/workflows/cd.yml/badge.svg)

**Modern, expressive, and safe error handling for .NET** — ditch exception-based flows and unclear return values for a clean, composable `Result` type that clearly communicates intent.

## 🎯 Key Features

- ✅ **Clear success/failure states** — no more silent failures or unchecked exceptions
- 🎁 **Generic payload support** — `Result<T>` carries data on success
- 🧠 **Rich error info** — include messages, error codes, and structured `ResultError` entries
- 🔄 **Exception conversion** — use `ex.ToResult()` to map exceptions to `Result` objects
- 🧵 **Traceable failures** — each failure auto-generates a `LogTraceCode` for seamless logging
- 🔗 **Fluent API** — chain `.WithMessage()`, `.WithCode()`, `.WithErrors()` easily
- ✳️ **Support for localized messages** — use `.WithLocalizedMessage()` for multi-language apps
- 🧩 **Pattern matching support** — `.Match(onSuccess, onFailure)` for clean outcome handling

## 📚 Resources & Samples

### 📦 Samples

Explore example usage:

- Console app under `samples/Result.Net.Samples.Console`
- AspNetCore app (with MediatR) under `samples/Result.Net.Samples.AspNetCore`

### 📖 Documentation & Tutorials

- Official [Wiki](https://github.com/YoussefSell/Result.Net/wiki) page with full API reference
- Blog posts:
  - [To Throw or To Return (Exceptions vs Result Object)?](https://youssefsellami.com/exceptions_vs_result_object/)
  - [Working with Exceptions in C#/.NET](https://youssefsellami.com/working-with-exceptions-in-csharp/)

---

## ⚙️ Quick Setup

To get started, install the package using [NuGet](https://www.nuget.org/packages/Result.Net/):

```powershell
Install-Package Result.Net
```

Then start returning Result or Result<T> instead of throwing or returning false.

## 💡 Sample Usage

```csharp
// to create success result
var successResult = Result.Success();

// to create a failure result
var failedResult = Result.Failure();
```

To create a result wrapper for a value, you can use `Result<TValue>`:

```csharp
// to create success results with different types of values
var successResult1 = Result.Success(30.15f);
var successResult2 = Result.Success<float>(55.5f);
var successResult3 = Result.Success(new SampleObject());
var successResult4 = Result.Success(new List<string> { "item1" });
Result<List<string>> successResult5 = new List<string> { "item1" };

// to create a failed result for a float value
var failedResult = Result.Failure<float>();
```

Typically you will return the `Result` or `Result<T>` from your methods:

```csharp
// for methods with no return value (void)
public Result DoSomething()
{
  if (this.SomethingShouldNotBeTrue())
      return Result.Failure();

  // other logic

  return Result.Success();
}

// for methods with a return value
public Result<int> DoSomething()
{
  if (this.SomethingShouldNotBeTrue())
      return Result.Failure<int>();

  // other logic

  return 10;
}
```

To associate messages with the result instance for a clear idea of what happened:

```csharp
// to create success result with a message
var successResult = Result.Success<float>(55.5f)
  .WithMessage("Operation Succeeded");

// to create a failed result with a message
var failedResult = Result.Failure<float>()
  .WithMessage("Operation Failed");
```

You can also use localized messages; more details can be found [here](https://github.com/YoussefSell/Result.Net/wiki/Working-With-Localization):

```csharp
// to create a failed result with a localized message
var failedResult = Result.Failure<float>()
  .WithLocalizedMessage("some_text_code");
```

You can utilize error codes to give a better explanation of what happens (useful for machine-to-machine communication):

```csharp
// create a failed result with a message and code
var failedResult = Result.Failure()
  .WithMessage("The provided email is not valid")
  .WithCode(ResultCode.InvalidEmail);
```

**Note:** There is a list of predefined error codes that I use frequently; you can find them in [ResultCode](https://github.com/YoussefSell/Result.Net/blob/main/src/Result.Net/Constants/ResultCode.cs) or simply use any string value.

You can also provide a ResultError to better define what happened:

```csharp
var failedResult = Result.Failure()
  .WithMessage("The provided email is not valid")
  .WithCode("email_validation")
  .WithErrors(new []
  {
    new ResultError(
      message: "The email host is not allowed",
      code: "invalid_email_host")
  });

// or use the short syntax
var failedResult = Result.Failure()
  .WithMessage("The provided email is not valid")
  .WithCode("email_validation")
  .WithErrors("The email host is not allowed", "invalid_email_host");
```

If you want to encapsulate an exception, you can do the following:

```csharp
public Result DoSomeWork()
{
  try
  {
    // your code goes here
    return Result.Success();
  }
  catch(Exception ex)
  {
    return Result.Failure()
      .WithMessage("An internal exception has been thrown")
      .WithCode(ResultCode.OperationFailedException)
      .WithError(ex);
  }
}
```

Or you can use a predefined method to convert the exception to a result object:

```csharp
public Result DoSomeWork()
{
  try
  {
    // your code goes here
    return Result.Success();
  }
  catch(Exception ex)
  {
    return ex.ToResult();
  }
}
```

For more details on working with exceptions, check out our [wiki page](https://github.com/YoussefSell/Result.Net/wiki/Working-with-Exceptions).

What about logging? By default, if you create a failed result, a LogTraceCode will be generated, and you can include this tracing code in your logs to track the errors:

```csharp
public Result DoSomeWork()
{
  try
  {
    // your code goes here
    return Result.Success();
  }
  catch(Exception ex)
  {
    var result = ex.ToResult();

    _logger.LogError(ex, "exception {@info}", new
    {
      LogTraceCode = result.LogTraceCode
      // + your metadata about the error
    });

    return result;
  }
}
```

What about checking your method execution status:

```csharp
var result = DoSomeWork();

// you can use IsSuccess() or IsFailure()
if (result.IsSuccess()) {
  Console.WriteLine("Operation succeeded");
}

if (result.IsFailure()) {
  Console.WriteLine("Operation failed");
}

// or you can use the Match extension
result.Match(
  // the action to run on success
  onSuccess: result => {
    Console.WriteLine("Executed if result has a success status");
  },
  // the action to run on failure
  onFailure: result => {
    Console.WriteLine("Executed if result has a failure status");
  });

// you can also return a value
var output = result.Match(
  // the func to run on success
  onSuccess: result => {
    Console.WriteLine("Executed if result has a success status");
    return "test";
  },
  // the func to run on failure
  onFailure: result => {
    Console.WriteLine("Executed if result has a failure status");
    return "test";
  });
```

you can configure the behavior of the Result object using the central configuration method:

```csharp
Result.Configure(config => {
  // here goes the configuration
});
```

For more details, check out the [Wiki](https://github.com/YoussefSell/Result.Net/wiki) page.

---

## ❤️ Contributing & Support

Star ⭐ the repo to support the project. Share your feedback, file issues, or send PRs.
Find more examples, documentation, and usage tips in the Wiki and samples folder.

## 📝 Copyright

Copyright © Youssef SELLAMI. See [LICENSE](https://github.com/YoussefSell/Result.Net/blob/main/LICENSE) for details.

## TL;DR

Result.NET is your lightweight, intuitive solution for safer error handling in .NET.
Install it, wrap your results, add context, and handle outcomes cleanly — no more exceptions or failed methods without explanation.
