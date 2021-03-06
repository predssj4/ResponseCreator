﻿using System.Linq;
using ResponseCreator.Abstract;
using ResponseCreator.Tests.Fakes;
using Shouldly;
using Xunit;

namespace ResponseCreator.Tests.Validation
{
    public class NastedDTOValidationTests
    {
        public const string TooShortValidationResult = "TooShort";

        [Theory]
        [InlineData("", "nameTest", "", "level2_nameTest", false, "TooShortOnBothLevels_ResultsAccessibleUnderGivenKeys" )]
        public void ValidateNestedInput(string level1Input, string level1Key, string level2Input, string level2Key,
            bool isValid, string displayMessage)
        {
            // arrange
            Level1DTO level1 = new Level1DTO()
            {
                NameTest = level1Input,
                Level2 = new Level2DTO()
                {
                    NameTest = level2Input
                }
            };

            IResponseCreator responseCreator = new global::ResponseCreator.ResponseCreator();

            // act
            level1.ValidateInput(responseCreator);

            // assert
            responseCreator.IsValid().ShouldBe(isValid);
            responseCreator.GetValidationResultsForKey(level1Key).Single().ShouldBe(TooShortValidationResult, customMessage: displayMessage);
            responseCreator.GetValidationResultsForKey(level2Key).Single().ShouldBe(TooShortValidationResult, customMessage: displayMessage);
        }
    }
}
