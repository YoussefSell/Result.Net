namespace Result.Net.Tests
{
    using Xunit;

    /// <summary>
    /// the test class for success result object
    /// </summary>
    public class SuccessResultShould
    {
        [Fact]
        public void Create_Success_Result_With_Default_data()
        {
            // arrange
            var expectedMessage = string.Empty;
            var expectedStatus = ResultStatus.Succeed;

            // act
            var result = Result.Success();

            // assert
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedStatus, result.Status);

            Assert.False(result.HasData());
            Assert.False(result.HasErrors());
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

            // act
            var result = Result.Success()
                .WithMessage("success message")
                .WithCode("success_code");

            // assert
            Assert.Equal(expectedMessage, result.Message);
            Assert.Equal(expectedCode, result.Code);

            Assert.False(result.HasData());
            Assert.False(result.HasErrors());
            Assert.Equal(0, result.Errors.Count);
            Assert.Equal(string.Empty, result.LogTraceCode);
            Assert.Equal(ResultStatus.Succeed, result.Status);
        }

        [Fact]
        public void Create_Success_Result_With_MetaData()
        {
            // arrange
            var expectedMetaDataCount = 2;

            // act
            var result = Result.Success()
                .WithMataData("test1", new { key = 1 })
                .WithMataData("test2", new { key = 2 });

            // assert
            Assert.Equal(expectedMetaDataCount, result.MetaData.Count);
        }
    }
}
