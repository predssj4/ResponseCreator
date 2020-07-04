using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResponseCreator.Abstract;
using ResponseCreator.Tests.DataFactories;
using ResponseCreator.Validators;
using Shouldly;
using Xunit;

namespace ResponseCreator.Tests.ValidationTests
{
    public class IntValidationTests
    {
        [Theory]
        [InlineData(5, 0, 10, false, "InputInRange_NoValidationErrors")]
        [InlineData(5, 9, 10, true, "InputNotInRange_HasValidationErrors")]
        [InlineData(5, default(int), int.MaxValue, false, "DefaultMinMaxValues_HasNoValidationErrors")]

        public void ValidateRangeInput(int inputValue, int min, int max, bool hasError, string displayMessage)
        {
            // arrange
            string key = "input_int_key";

            IResponseCreator responseCreator = new ResponseCreator();
            InputValidator<int> objectUnderTest = new InputValidator<int>(inputValue, responseCreator);

            // act
            objectUnderTest.ForInt(x => x, key)
                .InRange(min, max);
            
            // assert
            responseCreator.GetValidationResultsForKey(key).Any().ShouldBe(hasError, displayMessage);
        }

        [Theory]
        [InlineData(5, false, "NotDefaultValue_NoValidationErrors")]
        [InlineData(default(int), true, "DefaultValue_HasValidationErrors")]

        public void ValidateNotDefaultInput(int inputValue, bool hasError, string displayMessage)
        {
            // arrange
            string key = "input_int_key";

            IResponseCreator responseCreator = new ResponseCreator();
            InputValidator<int> objectUnderTest = new InputValidator<int>(inputValue, responseCreator);

            // act
            objectUnderTest.ForInt(x => x, key)
                .NotDefault();

            // assert
            responseCreator.GetValidationResultsForKey(key).Any().ShouldBe(hasError, displayMessage);
        }
    }
}
