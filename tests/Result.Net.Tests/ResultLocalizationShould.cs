namespace ResultNet.Tests
{
    using Xunit;

    public class ResultLocalizationShould
    {
        [Fact]
        public void ThrowIfGeTextMethodIsNotSet()
        {
            // arrange & act
            Result.Configure(config => config.Localization.GetText = null);

            // assert
            Assert.Throws<LocalizationGetTextMethodNotImplementedException>(() 
                => Result.Success().WithLocalizedMessage("some_text"));
        }

        [Fact]
        public void ThrowIfTextCodeIsNull()
        {
            // arrange & act
            Result.Configure(config => config.Localization.GetText = (text_code, language_code) => text_code);

            // assert
            Assert.Throws<ResultTextCodeNotSpecifiedException>(()
                => Result.Success().WithLocalizedMessage(null));
        }

        [Fact]
        public void ThrowIfTextCodeIsEmpty()
        {
            // arrange & act
            Result.Configure(config => config.Localization.GetText = (text_code, language_code) => text_code);

            // assert
            Assert.Throws<ResultTextCodeNotSpecifiedException>(()
                => Result.Success().WithLocalizedMessage(""));
        }

        [Fact]
        public void ThrowIfErrorCodeIsNull()
        {
            // arrange & act
            Result.Configure(config => config.Localization.GetText = (text_code, language_code) => text_code);

            // assert
            Assert.Throws<ResultTextCodeNotSpecifiedException>(()
                => Result.Failure()
                    .WithCode(null)
                    .WithLocalizedMessage());
        }

        [Fact]
        public void ThrowIfErrorCodeIsEmpty()
        {
            // arrange & act
            Result.Configure(config => config.Localization.GetText = (text_code, language_code) => text_code);

            // assert
            Assert.Throws<ResultTextCodeNotSpecifiedException>(()
                => Result.Failure()
                    .WithCode("")
                    .WithLocalizedMessage());
        }

        [Fact]
        public void ThrowIfNoTranslationHasBeenHound()
        {
            // arrange & act
            Result.Configure(config =>
            {
                config.Localization.ThrowIfNotFound = true;
                config.Localization.GetText = (text_code, language_code) => null;
            });

            // assert
            Assert.Throws<ResultMessageLocalizationNotFoundException>(()
                => Result.Success()
                    .WithLocalizedMessage("some_text"));
        }

        [Fact]
        public void NotThrowIfNoTranslationHasBeenFound()
        {
            // arrange
            Result.Configure(config =>
            {
                config.Localization.ReturnNull = false;
                config.Localization.ThrowIfNotFound = false;
                config.Localization.GetText = (text_code, language_code) => null;
            });

            // act
            var result = Result.Success().WithLocalizedMessage("some_text");

            // assert
            Assert.NotNull(result.Message);
        }

        [Fact]
        public void ReturnNullIfNoTranslationHasBeenFound()
        {
            // arrange
            Result.Configure(config =>
            {
                config.Localization.ReturnNull = true;
                config.Localization.ThrowIfNotFound = false;
                config.Localization.GetText = (text_code, language_code) => null;
            });

            // act
            var result = Result.Success().WithLocalizedMessage("some_text");

            // assert
            Assert.Null(result.Message);
        }

        [Fact]
        public void ReturnSameTextCodeIfNoTranslationHasBeenHound()
        {
            // arrange
            Result.Configure(config =>
            {
                config.Localization.ReturnNull = false;
                config.Localization.ThrowIfNotFound = false;
                config.Localization.GetText = (text_code, language_code) => null;
            });

            // act
            var result = Result.Success().WithLocalizedMessage("some_text");

            // assert
            Assert.Equal("some_text", result.Message);
        }
    }
}
