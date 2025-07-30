namespace ResultNet.Tests
{
    using Xunit;

    public class ResultExceptionMapperShould
    {
        [Fact]
        public void Add_New_Mapping()
        {
            // arrange
            var errorCode = "some_error_code";

            // act
            Result.Configure(config => config.ExceptionMapper.AddMapping(errorCode, result => new ResultException(result)));

            // assert
            Assert.NotNull(Result.Configuration.ExceptionMapper.GetMapping<ResultException>(errorCode));
        }

        [Fact]
        public void Get_Existing_Mapping()
        {
            // arrange
            var errorCode = "some_error_code";
            Result.Configure(config => config.ExceptionMapper.AddMapping(errorCode, result => new ResultException(result)));

            // act
            var mapping = Result.Configuration.ExceptionMapper.GetMapping<ResultException>(errorCode);

            // assert
            Assert.NotNull(mapping);
        }

        [Fact]
        public void Get_Null_For_Non_Existing_Mapping()
        {
            // arrange
            var errorCode = "non_existing_error_code";

            // act
            var mapping = Result.Configuration.ExceptionMapper.GetMapping<ResultException>(errorCode);

            // assert
            Assert.Null(mapping);
        }

        [Fact]
        public void Add_Custom_Exception_To_Mapping()
        {
            // arrange
            var errorCode = "some_error_code";
            Result.Configure(config => config.ExceptionMapper.AddMapping(errorCode, result => new CustomExceptionException(result)));

            // act
            var mapping = Result.Configuration.ExceptionMapper.GetMapping<CustomExceptionException>(errorCode);

            // assert
            Assert.NotNull(mapping);
        }
    }


    [System.Serializable]
    public class CustomExceptionException : ResultException
    {
        public CustomExceptionException(Result result) : base(result)
        {
        }
    }
}
