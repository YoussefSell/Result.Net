namespace ResultNet.Tests
{
    using Xunit;

    public class ResultExtensionsShould
    {
        [Fact]
        public void Match_Result_On_Success()
        {
            // arrange
            var result = Result.Success();
            var expected = "run";

            // act
            var outPut = string.Empty;

            result.Match(
                onSuccess: result => outPut = "run",
                onFailure: result => outPut = "run1");

            // assert
            Assert.Equal(expected, outPut);
        }

        [Fact]
        public void Not_Match_Result_On_Success_When_Action_Null()
        {
            // arrange
            var result = Result.Success();

            // act
            var outPut = string.Empty;

            result.Match(
                onSuccess: null,
                onFailure: result => outPut = "run1");

            // assert
            Assert.Empty(outPut);
        }

        [Fact]
        public void Match_Result_On_Failure()
        {
            // arrange
            var result = Result.Failure();
            var expected = "run";

            // act
            var outPut = string.Empty;

            result.Match(
                onSuccess: result => outPut = "run1",
                onFailure: result => outPut = "run");

            // assert
            Assert.Equal(expected, outPut);
        }
        
        [Fact]
        public void Not_Match_Result_On_Failure_When_Action_Null()
        {
            // arrange
            var result = Result.Failure();

            // act
            var outPut = string.Empty;

            result.Match(
                onSuccess: result => outPut = "run1",
                onFailure: null);

            // assert
            Assert.Empty(outPut);
        }

        [Fact]
        public void Match_Result_On_Success_And_Return_Output()
        {
            // arrange
            var result = Result.Success();
            var expected = "run";

            // act
            var outPut = result.Match(
                onSuccess: result => "run",
                onFailure: result => "run1");

            // assert
            Assert.Equal(expected, outPut);
        }

        [Fact]
        public void Match_Result_On_Failure_And_Return_Output()
        {
            // arrange
            var result = Result.Failure();
            var expected = "run";

            // act
            var outPut = result.Match(
                onSuccess: result => "run1",
                onFailure: result => "run");

            // assert
            Assert.Equal(expected, outPut);
        }

        [Fact]
        public void Match_Result_On_Success_And_Return_Default_When_Null()
        {
            // arrange
            var result = Result.Success();

            // act
            var outPut = result.Match(
                onSuccess: null,
                onFailure: result => "run1");

            // assert
            Assert.Equal(default, outPut);
        }

        [Fact]
        public void Match_Result_On_Failure_And_Return_Default_When_Null()
        {
            // arrange
            var result = Result.Failure();

            // act
            var outPut = result.Match(
                onSuccess: result => "run1",
                onFailure: null);

            // assert
            Assert.Equal(default, outPut);
        }
    }
}
