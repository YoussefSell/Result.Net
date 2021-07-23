namespace Result.Net.Tests
{
    using System;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// the test class for result with data object
    /// </summary>
    public class ResultWithdataShould
    {
        [Fact]
        public void Create_Success_Result_With_Default_data()
        {
            // arrange
            var expectedStatus = ResultStatus.Succeed;
            var expecteddata = 10;

            // act
            var result = Result.Success(10);

            // assert
            Assert.True(result.HasData());
            Assert.Equal(expecteddata, result.Data);
            Assert.Equal(expectedStatus, result.Status);
        }

        [Fact]
        public void Create_Failed_Result_With_Default_data()
        {
            // arrange
            var expectedStatus = ResultStatus.Failed;

            // act
            var result = Result.Failure<int>();

            // assert
            Assert.False(result.HasData());
            Assert.Equal(expectedStatus, result.Status);
        }

        [Fact]
        public void Convert_data_Implicitly()
        {
            // arrange 
            var result = Result.Success(150);
            var expecteddata = 150;

            // act
            int data = result;

            // assert
            Assert.Equal(expecteddata, data);
        }
    }
}
