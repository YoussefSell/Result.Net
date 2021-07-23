namespace Result.Net.Tests
{
    using System.Linq;
    using System;
    using Xunit;

    /// <summary>
    /// the test class for failed result object
    /// </summary>
    public class FailedResultShould
    {
        [Fact]
        public void Create_Failed_Result_With_Default_data()
        {
            // arrange
            var expectedMessage = string.Empty;
            var expectedStatus = ResultStatus.Failed;

            // act
            var result = Result.Failure();

            // assert
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedStatus, result.Status);

            Assert.False(result.HasData());
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
            var result = Result.Failure()
                .WithMessage("failed message")
                .WithCode("test_code");

            // assert
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedCode, result.Code);

            Assert.False(result.HasData());
            Assert.False(result.HasErrors());
            Assert.Equal(0, result.Errors.Count);
            Assert.NotEqual(string.Empty, result.LogTraceCode);
            Assert.Equal(ResultStatus.Failed, result.Status);
        }

        [Fact]
        public void Create_Failed_Result_With_MetaData()
        {
            // arrange
            var expectedMetaDataCount = 2;

            // act
            var result = Result.Failure()
                .WithMataData("test1", new { key = 1 })
                .WithMataData("test2", new { key = 2 });

            // assert
            Assert.Equal(expectedMetaDataCount, result.MetaData.Count);
        }

        [Fact]
        public void Create_Failed_Result_With_ResultErrors()
        {
            // arrange
            var expectedErrorsCount = 2;

            // act
            var result = Result.Failure()
                .WithErrors(new[]
                {
                    new ResultError("test", "test_code", "source", "errorType"),
                    new ResultError("test", "test_code", "source", "errorType")
                });

            // assert
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }

        [Fact]
        public void Create_Failed_Result_With_ExceptionError()
        {
            // arrange
            var expectedErrorsCount = 1;
            var expectedExceptionErrorMessage = "exception message";

            // act
            var result = Result.Failure()
                .WithErrors(new Exception("exception message"));

            // assert
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
            Assert.Equal(expectedExceptionErrorMessage, result.Errors.First().Message);
        }
    }
}
