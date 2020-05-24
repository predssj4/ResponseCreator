using System.Linq;
using ResponseCreator.Abstract;
using ResponseCreator.Tests.DataFactories;
using ResponseCreator.Tests.Fakes;
using Shouldly;
using Xunit;

namespace ResponseCreator.Tests.ValidationTests
{
    public class ValidationPrefixesTests
    {
        [Fact]
        public void Key_SingleDefaultKey_ShouldBeTakenFromPropertyName()
        {
            // arrange
            SingleLevelDTO inputDTO = new SingleLevelDTO()
            {
                NameNotAccidentalCreated = string.Empty
            };

            IResponseCreator responseCreator = new ResponseCreator();

            // act
            inputDTO.ValidateInput(responseCreator);

            // assert
            responseCreator.GetValidationResultsForKey("nameNotAccidentalCreated").Count().ShouldBe(1);
        }

        [Fact]
        public void Key_CustomKey_CustomKeyShouldBeTaken()
        {
            // arrange
            SingleLevelDTO inputDTO = new SingleLevelDTO()
            {
                NameNotAccidentalCreated = string.Empty
            };

            IResponseCreator responseCreator = new ResponseCreator();

            // act
            inputDTO.ValidateInputWithCustomPropertyName(responseCreator);

            // assert
            responseCreator.GetValidationResultsForKey("customPropertyNameCreated").Count().ShouldBe(1);
        }

        [Fact]
        public void KeyWithPrefix_DefaultKeyWithObjectInputPrefix_KeyWithPrefixCreated()
        {
            // arrange
            SingleLevelDTO inputDTO = new SingleLevelDTO()
            {
                NameNotAccidentalCreated = string.Empty
            };

            IResponseCreator responseCreator = new ResponseCreator();

            string prefix = "SomePrefix";

            // act
            inputDTO.ValidateInput(responseCreator, prefix);

            // assert
            responseCreator.GetValidationResultsForKey("somePrefix_nameNotAccidentalCreated").Count().ShouldBe(1);
        }

        [Fact]
        public void KeyWithPrefix_CustomKeyWithObjectInputPrefix_KeyWithPrefixCreated()
        {
            // arrange
            SingleLevelDTO inputDTO = new SingleLevelDTO()
            {
                NameNotAccidentalCreated = string.Empty
            };

            IResponseCreator responseCreator = new ResponseCreator();

            string prefix = "SomePrefix";

            // act
            inputDTO.ValidateInputWithCustomPropertyName(responseCreator, prefix);

            // assert
            responseCreator.GetValidationResultsForKey("somePrefix_customPropertyNameCreated").Count().ShouldBe(1);
        }

        [Fact]
        public void KeyWithPrefix_DefaultKeyWithOverwrittenPrefix_PrefixForParticularPropertyShouldBeOverwritten()
        {
            // arrange
            SingleLevelDTO inputDTO = new SingleLevelDTO()
            {
                NameNotAccidentalCreated = string.Empty
            };

            IResponseCreator responseCreator = new ResponseCreator();

            string prefix = "SomePrefix";
            string singlePropertyCustomPrefix = "PropertyPrefix";

            // act
            inputDTO.ValidateInputWithCustomPropertyNameAndCustomPrefix(responseCreator, prefix, singlePropertyCustomPrefix);
        }

        [Fact]
        public void KeysWithPrefixes_NestedDefaultKeys_ShouldBeTakenFromPropertiesNamesWithPrefixes()
        {
            // arrange
            var input = CreateStructureToValidation();
            IResponseCreator responseCreator = ResponseCreatorDataFactory.Create();

            // act
            input.ValidateInput(responseCreator);

            //
            responseCreator.GetValidationResultsForKey("nameTest").Count().ShouldBe(1);
            responseCreator.GetValidationResultsForKey("level2_nameFromLevel2").Count().ShouldBe(1);
            responseCreator.GetValidationResultsForKey("level2_level3Dto_propertyLevel3").Count().ShouldBe(1);
        }

        [Fact]
        public void KeysWithPrefixes_NestedCustomKeys_ShouldBeTakenFromProvidedKeyNames()
        {
            // arrange
            var input = CreateStructureToValidation();
            IResponseCreator responseCreator = ResponseCreatorDataFactory.Create();

            // act
            input.ValidateInputWithCustomKeys(responseCreator);

            // arrange
            responseCreator.GetValidationResultsForKey("nameTestCustom").Count().ShouldBe(1);
            responseCreator.GetValidationResultsForKey("level2_nameFromLevel2Custom").Count().ShouldBe(1);
            responseCreator.GetValidationResultsForKey("level2_level3Dto_propertyLevel3Custom").Count().ShouldBe(1);
        }

        [Fact]
        public void KeysWithPrefixes_OverwrittenPrefixesInNestedStructure_OverwrittenPrefixesAreTaken()
        {
            // arrange
            var input = CreateStructureToValidation();
            IResponseCreator responseCreator = ResponseCreatorDataFactory.Create();

            string prefix = "SomeGeneralPrefix";

            // act
            input.ValidateInputWithPrefixOverwriting(responseCreator, prefix);

            // arrange
            responseCreator.GetValidationResultsForKey("prefixOn1_nameTest").Count().ShouldBe(1);
            responseCreator.GetValidationResultsForKey("someGeneralPrefix_level2_nameFromLevel2").Count().ShouldBe(1);
            responseCreator.GetValidationResultsForKey("prefixOn3_propertyLevel3").Count().ShouldBe(1);
        }

        private static Level1DTO CreateStructureToValidation()
        {
            return new Level1DTO()
            {
                NameTest = string.Empty,
                Level2 = new Level2DTO()
                {
                    NameFromLevel2 = string.Empty,
                    Level3Dto = new Level3DTO()
                    {
                        PropertyLevel3 = string.Empty
                    }
                }
            };
        }
    }
}
