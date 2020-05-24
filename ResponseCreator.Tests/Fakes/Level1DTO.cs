using ResponseCreator.Abstract;
using ResponseCreator.Tests.ValidationTests;

namespace ResponseCreator.Tests.Fakes
{
    internal class Level1DTO : IInputDTO
    {
        public string NameTest { get; set; }
        public Level2DTO Level2 { get; set; }

        public void ValidateInput(IResponseCreator responseCreator, string prefix = null)
        {
            var iv = new InputValidator<Level1DTO>(this, responseCreator);

            iv.ForString(x => x.NameTest)
                .MinLength(1, NestedDTOValidationTests.TooShortValidationResult);

            this.Level2.ValidateInput(responseCreator, nameof(Level2));
        }

        public void ValidateInputWithCustomKeys(IResponseCreator responseCreator)
        {
            var iv = new InputValidator<Level1DTO>(this, responseCreator);

            iv.ForString(x => x.NameTest, key: "nameTestCustom")
                .MinLength(1, NestedDTOValidationTests.TooShortValidationResult);

            this.Level2.ValidateInputWithCustomKeys(responseCreator, nameof(Level2));
        }

        public void ValidateInputWithPrefixOverwriting(IResponseCreator responseCreator, string prefix)
        {
            var iv = new InputValidator<Level1DTO>(this, responseCreator, prefix);

            iv.ForString(x => x.NameTest , propertyCustomPrefix: "prefixOn1")
                .MinLength(1, NestedDTOValidationTests.TooShortValidationResult);

            this.Level2.ValidateInputWithPrefixOverwriting(responseCreator, KeyPrefixBuilder.JoinPrefixes(prefix, nameof(Level2)));
        }
    }
}
