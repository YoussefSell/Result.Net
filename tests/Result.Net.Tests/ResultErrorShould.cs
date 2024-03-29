﻿namespace ResultNet.Tests
{
    using System;
    using Xunit;

    /// <summary>
    /// the test class for result error with data object
    /// </summary>
    public class ResultErrorShould
    {
        [Fact]
        public void Create_Instance_From_Exception()
        {
            // arrange
            var exception = new Exception("test exception");

            // act
            ResultError error = new ResultError(exception);

            // assert
            Assert.Equal("test exception", error.Message);
            Assert.Equal(exception.Source, error.Source);
            Assert.Equal(ResultCode.OperationFailedException, error.Code);
        }
    }
}
