using System.Linq;
using ResponseCreator.Abstract;
using ResponseCreator.Tests.DataFactories;
using Shouldly;
using Xunit;

namespace ResponseCreator.Tests.ValidationTests
{
    public class StringValidationTests
    {
        private const string MinLengthValidationResultMessage = "minLength";
        private const string MaxLengthValidationResultMessage = "maxLength";

        [Theory]
        [InlineData("input-length-15", "key1", 5, 20, true, 0, new string[0], "CorrectLength_NoValidationErrors")]
        [InlineData("", "key1", 5, 20, false, 1, new string[] { MinLengthValidationResultMessage }, "EmptyString_ValidationResultMinLength")]
        [InlineData(null, "key1", 5, 15, false, 1, new string[] { MinLengthValidationResultMessage }, "NullStringInput_ValidationResultMinLength")]
        [InlineData("very-long-input-over-15", "key1", 5, 15, false, 1, new string[] { MaxLengthValidationResultMessage }, "TooLongStringInput_ValidationResultMaxLength")]
        [InlineData("very-lo-10", "key1", 10, 10, true, 0, new string[0], "SameMinMaxLength_NoValidationErrors")]
        public void ValidateString(string input, string key, int minLength, int maxLength,
            bool expectedIsValid, int expectedNumberOfValidationResults,
            string[] expectedErrors,
            string displayMessage)
        {
            // arrange
            IResponseCreator responseCreator = ResponseCreatorFactory.Create();
            var inputValidator = new InputValidator<string>(input, responseCreator);

            // act
            inputValidator.ForString(x => x, key)
                .MinLength(minLength, MinLengthValidationResultMessage)
                .MaxLength(maxLength, MaxLengthValidationResultMessage);

            var actual = responseCreator.IsValid();
            var actualNumberOfValidationResultsForKey = responseCreator.NumberOfValidationResultsForKey(key);
            var actualValidationResults = responseCreator.GetValidationResultsForKey(key);

            // assert
            actual.ShouldBe(expectedIsValid, customMessage: displayMessage);
            actualNumberOfValidationResultsForKey.ShouldBe(expectedNumberOfValidationResults, customMessage: displayMessage);

            responseCreator.GetValidationResultsForKey(key).Count().ShouldBe(expectedErrors.Length);

            expectedErrors.ToList().ForEach(expectedError =>
            {
                actualValidationResults.ShouldContain(expectedError);
            });
        }

        [Theory]
        [InlineData(default(string), true, "DefaultValue_HasValidationErrors")]
        [InlineData("some_not_default_value", false, "NotDefaultValue_NoValidationErrors")]
        public void NotDefault(string input, bool hasErrors, string displayMessage)
        {
            // arrange
            string key = "string_key";

            IResponseCreator responseCreator = ResponseCreatorFactory.Create();
            var inputValidator = new InputValidator<string>(input, responseCreator);

            // act
            inputValidator.ForString(x => x, key)
                .NotDefault();

            // arrange
            responseCreator.GetValidationResultsForKey(key).Any().ShouldBe(hasErrors, displayMessage);
        }
    }
}
