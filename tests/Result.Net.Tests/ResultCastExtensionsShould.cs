namespace ResultNet.Tests
{
    using Xunit;

    public class ResultCastExtensionsShould
    {
        [Fact]
        public void Cast_Success_Result_To_Result_Data()
        {
            // arrange
            var result = Result.Success();

            // act
            var cast = result.Cast<decimal>();

            // assert
            Assert.IsType<decimal>(cast.Data);
            Assert.Equal(result.Status, cast.Status);
        }

        [Fact]
        public void Cast_Failure_Result_To_Result_Data()
        {
            // arrange
            var result = Result.Failure()
                .WithCode("test_code")
                .WithMessage("failed message")
                .WithMataData("test1", new { key = 1 })
                .WithErrors(new ResultError("test", "test_code", "source")); ;

            // act
            var cast = result.Cast<decimal>();

            // assert
            Assert.IsType<decimal>(cast.Data);
            Assert.Equal(result.Code, cast.Code);
            Assert.Equal(result.Status, cast.Status);
            Assert.Equal(result.Message, cast.Message);
            Assert.Equal(result.Errors.Count, cast.Errors.Count);
            Assert.Equal(result.MetaData.Count, cast.MetaData.Count);
        }
    }
}
