namespace Result.Net.Tests
{
    using System;
    using Xunit;

    public class ExceptionShould
    {
        [Fact]
        public void Convert_Result_Exception_To_Result()
        {
            // arrange
            var result = Result.Failure()
                .WithMessage("some message")
                .WithCode("some_code")
                .WithMataData("some_key", new { key = 1});
            
            var exception = result.ToException();

            // act
            var resultFromException = exception.ToResult();

            // assert
            Assert.Equal(result.Code, resultFromException.Code);
            Assert.Equal(result.Message, resultFromException.Message);
            Assert.Equal(result.LogTraceCode, resultFromException.LogTraceCode);
            Assert.Equal(result.MetaData.Count, resultFromException.MetaData.Count);
        }

        [Fact]
        public void Convert_Exception_To_Result()
        {
            // arrange
            var exception = new Exception(message: "test message", new Exception("inner"));

            // act
            var resultFromException = exception.ToResult();

            // assert
            Assert.Equal(ResultCode.OperationFailedException, resultFromException.Code);
            Assert.Equal("test message", resultFromException.Message);
            Assert.Equal(1, resultFromException.Errors.Count);
        }
    }
}
