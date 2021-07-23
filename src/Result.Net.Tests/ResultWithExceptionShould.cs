namespace Result.Net.Tests
{
    using System;
    using Xunit;

    public class ResultWithExceptionShould
    {
        [Fact]
        public void Convert_Result_To_Exception()
        {
            // arrange
            var result = Result.Failure()
                .WithMessage("failed to complete the order, customer not found")
                .WithCode("customer_not_found");

            // act
            var exception = result.ToException();

            // assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void Convert_Result_To_Exception_And_Throw()
        {
            // arrange
            var result = Result.Failure()
                .WithMessage("failed to process the file, not found")
                .WithCode("file_not_found");

            // act
            // assert
            Assert.Throws<ResultException>(() => result.ToException().Throw());
        }

        [Fact]
        public void Convert_Result_To_Custom_Exception_If_Mapping_Exist()
        {
            // arrange
            ResultExceptionMapper.AddMapping("customer_not_found", result => new CustomExceptionException(result));
            
            var result = Result.Failure()
                .WithMessage("failed to complete the order, customer not found")
                .WithCode("customer_not_found");

            // act
            var exception = result.ToException();

            // assert
            Assert.IsType<CustomExceptionException>(exception);
        }

        [Fact]
        public void Not_Convert_Result_To_Custom_Exception_If_Mapping_Not_Exist()
        {
            // arrange
            var result = Result.Failure()
                 .WithMessage("failed to process the file, not found")
                 .WithCode("file_not_found");

            // act
            var exception = result.ToException();

            // assert
            Assert.IsNotType<CustomExceptionException>(exception);
        }

        [Fact]
        public void Convert_Result_To_Custom_Exception_And_Throw()
        {
            // arrange
            ResultExceptionMapper.AddMapping("customer_not_found", result => new CustomExceptionException(result));

            var result = Result.Failure()
                .WithMessage("failed to complete the order, customer not found")
                .WithCode("customer_not_found");

            // act
            // assert
            Assert.Throws<CustomExceptionException>(() => result.ToException().Throw());
        }

        [Fact]
        public void Throw_Exception_If_Mapping_Not_Exist()
        {
            // arrange
            var result = Result.Failure()
                 .WithMessage("failed to process the file, not found")
                 .WithCode("file_not_found");

            // act
            // assert
            Assert.Throws<ResultExceptionMappingNotFoundException>(() => result.ToException<CustomExceptionException>().Throw());
        }
    }
}
