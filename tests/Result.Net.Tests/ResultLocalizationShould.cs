namespace ResultNet.Tests
{
    using Xunit;

    public class ResultLocalizationShould
    {
        [Fact]
        public void ThrowIfGeTextMethodIsNotSet()
        {
            // arrange
            ResultMessageLocalizer.GetText = null;

            // act

            // assert
            Assert.Throws<LocalizationGetTextMethodNotImplementedException>(() 
                => Result.Success().WithLocalizedMessage("some_text"));
        }

        [Fact]
        public void ThrowIfTextCodeIsNull()
        {
            // arrange
            ResultMessageLocalizer.GetText = (text_code, language_code) => text_code;

            // act

            // assert
            Assert.Throws<ResultTextCodeNotSpecifiedException>(()
                => Result.Success().WithLocalizedMessage(null));
        }

        [Fact]
        public void ThrowIfTextCodeIsEmpty()
        {
            // arrange
            ResultMessageLocalizer.GetText = (text_code, language_code) => text_code;

            // act

            // assert
            Assert.Throws<ResultTextCodeNotSpecifiedException>(()
                => Result.Success().WithLocalizedMessage(""));
        }

        [Fact]
        public void ThrowIfErrorCodeIsNull()
        {
            // arrange
            ResultMessageLocalizer.GetText = (text_code, language_code) => text_code;

            // act

            // assert
            Assert.Throws<ResultTextCodeNotSpecifiedException>(()
                => Result.Failure()
                    .WithCode(null)
                    .WithLocalizedMessage());
        }

        [Fact]
        public void ThrowIfErrorCodeIsEmpty()
        {
            // arrange
            ResultMessageLocalizer.GetText = (text_code, language_code) => text_code;

            // act

            // assert
            Assert.Throws<ResultTextCodeNotSpecifiedException>(()
                => Result.Failure()
                    .WithCode("")
                    .WithLocalizedMessage());
        }

        [Fact]
        public void ThrowIfNoTranlsationHasBeenHound()
        {
            // arrange
            ResultMessageLocalizer.ThrowIfNotFound = true;
            ResultMessageLocalizer.GetText = (text_code, language_code) => null;

            // act

            // assert
            Assert.Throws<ResultMessageLocalizationNotFoundException>(()
                => Result.Success()
                    .WithLocalizedMessage("some_text"));
        }

        [Fact]
        public void NotThrowIfNoTranslationHasBeenFound()
        {
            // arrange
            ResultMessageLocalizer.ReturnNull = true;
            ResultMessageLocalizer.ThrowIfNotFound = false;
            ResultMessageLocalizer.GetText = (text_code, language_code) => null;

            // act
            var result = Result.Success().WithLocalizedMessage("some_text");

            // assert
            Assert.Null(result.Message);
        }

        [Fact]
        public void ReturnNullIfNoTranslationHasBeenFound()
        {
            // arrange
            ResultMessageLocalizer.ReturnNull = true;
            ResultMessageLocalizer.ThrowIfNotFound = false;
            ResultMessageLocalizer.GetText = (text_code, language_code) => null;

            // act
            var result = Result.Success().WithLocalizedMessage("some_text");

            // assert
            Assert.Null(result.Message);
        }

        [Fact]
        public void ReturnSameTextCodeIfNoTranlsationHasBeenHound()
        {
            // arrange
            ResultMessageLocalizer.ReturnNull = false;
            ResultMessageLocalizer.ThrowIfNotFound = false;
            ResultMessageLocalizer.GetText = (text_code, language_code) => null;

            // act
            var result = Result.Success().WithLocalizedMessage("some_text");

            // assert
            Assert.Equal("some_text", result.Message);
        }
    }
}
