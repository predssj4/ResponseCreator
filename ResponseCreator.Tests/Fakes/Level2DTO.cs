using ResponseCreator.Abstract;
using ResponseCreator.Tests.ValidationTests;

namespace ResponseCreator.Tests.Fakes
{
    public class Level2DTO : IInputDTO
    {
        public string NameFromLevel2 { get; set; }

        public Level3DTO Level3Dto { get; set; }

        public void ValidateInput(IResponseCreator responseCreator, string prefix)
        {
            var iv = new InputValidator<Level2DTO>(this, responseCreator, prefix);

            iv.ForString(x => x.NameFromLevel2)
                .MinLength(1, NestedDTOValidationTests.TooShortValidationResult);

            this.Level3Dto.ValidateInput(responseCreator, KeyPrefixBuilder.JoinPrefixes(prefix, nameof(Level3Dto)));
        }

        public void ValidateInputWithCustomKeys(IResponseCreator responseCreator, string prefix)
        {
            var iv = new InputValidator<Level2DTO>(this, responseCreator, prefix);

            iv.ForString(x => x.NameFromLevel2, key: "nameFromLevel2Custom")
                .MinLength(1, NestedDTOValidationTests.TooShortValidationResult);

            this.Level3Dto.ValidateInputWithCustomKeys(responseCreator, KeyPrefixBuilder.JoinPrefixes(prefix, nameof(Level3Dto)));
        }

        public void ValidateInputWithPrefixOverwriting(IResponseCreator responseCreator, string prefix)
        {
            var iv = new InputValidator<Level2DTO>(this, responseCreator, prefix);

            iv.ForString(x => x.NameFromLevel2)
                .MinLength(1, NestedDTOValidationTests.TooShortValidationResult);

            this.Level3Dto.ValidateInputWithPrefixOverwriting(responseCreator, KeyPrefixBuilder.JoinPrefixes(prefix, nameof(Level3Dto)));
        }
    }
}
