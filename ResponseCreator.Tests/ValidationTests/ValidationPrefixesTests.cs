using ResponseCreator.Abstract;
using ResponseCreator.Tests.DataFactories;
using ResponseCreator.Tests.Fakes;
using Xunit;

namespace ResponseCreator.Tests.ValidationTests
{
    public class ValidationPrefixesTests
    {
        [Fact]
        public void Key_SingleDefaultKey_ShouldBeTakenFromPropertyName()
        {
        

        }

        [Fact]
        public void Key_CustomKey_CustomKeyShouldBeTaken()
        {

        }

        [Fact]
        public void KeysWithPrefixes_NestedDefaultKeys_ShouldBeTakenFromPropertiesNamesWithPrefiexes()
        {
            // arrange
            var input = CreateStructureToValidation();
            IResponseCreator responseCreator = ResponseCreatorDataFactory.Create();

            // act
            input.ValidateInput(responseCreator);

        }

        [Fact]
        public void KeysWithPrefixes_NestedCustomKeys_ShouldBeTakenFromPropertiesNames()
        {
            
        }

        private static Level1DTO CreateStructureToValidation()
        {
            return new Level1DTO()
            {
                NameTest = string.Empty,
                Level2 = new Level2DTO()
                {
                    NameTest = string.Empty,
                    Level3Dto = new Level3DTO()
                    {
                        PropertyLevel3 = string.Empty
                    }
                }
            };
        }
    }
}
