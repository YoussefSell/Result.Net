namespace Result.Net.Tests
{
    using System;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// the test class for result with value object
    /// </summary>
    public class ListResultShould
    {
        [Fact]
        public void Create_Success_Result_With_Default_Value()
        {
            // arrange
            var expectedMessage = "Operation Succeeded";
            var expectedStatus = ResultStatus.Succeed;
            var expectedValue = new[] { 10, 12, 15 };

            // act
            var result = Result.ListSuccess(new[] { 10, 12, 15 });

            // assert
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedStatus, result.Status);
            Assert.Equal(expectedValue, result.Value);
            Assert.True(result.HasValue());

            Assert.Equal(0, result.Errors.Count);
            Assert.Equal(string.Empty, result.LogTraceCode);
            Assert.Equal(ResultCode.OperationSucceeded, result.Code);
        }

        [Fact]
        public void Create_Success_Result_With_Message_And_Code()
        {
            // arrange
            var expectedMessage = "success message";
            var expectedCode = "success_code";
            var expectedValue = new[] { 10, 12, 15 };

            // act
            var result = Result.ListSuccess(new[] { 10, 12, 15 })
                .WithMessage("success message")
                .WithCode("success_code");

            // assert
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedValue, result.Value);
            Assert.Equal(expectedCode, result.Code);

            Assert.True(result.HasValue());
            Assert.False(result.HasErrors());
            Assert.Equal(0, result.Errors.Count);
            Assert.Equal(string.Empty, result.LogTraceCode);
            Assert.Equal(ResultStatus.Succeed, result.Status);
        }

        [Fact]
        public void Create_Failed_Result_With_Default_Value()
        {
            // arrange
            var expectedMessage = "Operation Failed";
            var expectedStatus = ResultStatus.Failed;

            // act
            var result = Result.ListFailed<int>();

            // assert
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedStatus, result.Status);

            Assert.False(result.HasValue());
            Assert.False(result.HasErrors());
            Assert.Equal(0, result.Errors.Count);
            Assert.NotEqual(string.Empty, result.LogTraceCode);
            Assert.Equal(ResultCode.OperationFailed, result.Code);
        }

        [Fact]
        public void Create_Failed_Result_With_Message_And_Code()
        {
            // arrange
            var expectedMessage = "failed message";
            var expectedCode = "test_code";

            // act
            var result = Result.ListFailed<int>()
                .WithMessage("failed message")
                .WithCode("test_code");

            // assert
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedCode, result.Code);

            Assert.False(result.HasValue());
            Assert.False(result.HasErrors());
            Assert.Equal(0, result.Errors.Count);
            Assert.NotEqual(string.Empty, result.LogTraceCode);
            Assert.Equal(ResultStatus.Failed, result.Status);
        }

        [Fact]
        public void Create_Failed_Result_With_ResultErrors()
        {
            // arrange
            var expectedMessage = "failed message";
            var expectedCode = "test_code";
            var expectedErrorsCount = 2;

            // act
            var result = Result.ListFailed<int>()
                .WithMessage("failed message")
                .WithCode("test_code")
                .WithErrors(new[]
                {
                    new ResultError("test", "test_code", "source", "errorType"),
                    new ResultError("test", "test_code", "source", "errorType")
                });

            // assert
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedCode, result.Code);

            Assert.False(result.HasValue());
            Assert.True(result.HasErrors());
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
            Assert.NotEqual(string.Empty, result.LogTraceCode);
            Assert.Equal(ResultStatus.Failed, result.Status);
        }

        [Fact]
        public void Create_Failed_Result_With_ExceptionError()
        {
            // arrange
            var expectedMessage = "failed message";
            var expectedCode = "test_code";
            var expectedExceptionErrorMessage = "exception message";

            // act
            var result = Result.ListFailed<int>()
                .WithMessage("failed message")
                .WithCode("test_code")
                .WithErrors(new Exception("exception message"));

            // assert
            Assert.Equal(expectedCode, result.Code);
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedExceptionErrorMessage, result.Errors.First().Message);

            Assert.False(result.HasValue());
            Assert.True(result.HasErrors());
            Assert.NotEqual(string.Empty, result.LogTraceCode);
            Assert.Equal(ResultStatus.Failed, result.Status);
        }
    }
}
