namespace Result.Net.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// the test class for result with data object
    /// </summary>
    public class ListResultShould
    {
        [Fact]
        public void Create_Success_Result_With_Default_data()
        {
            // arrange
            var expectedStatus = ResultStatus.Succeed;
            var expecteddata = new[] { 10, 12, 15 };

            // act
            var result = Result.ListSuccess(new[] { 10, 12, 15 });

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
            var result = Result.ListFailure<int>();

            // assert
            Assert.False(result.HasData());
            Assert.Equal(expectedStatus, result.Status);
        }
    }
}
