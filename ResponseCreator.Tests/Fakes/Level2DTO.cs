using ResponseCreator.Abstract;
using ResponseCreator.Tests.ValidationTests;

namespace ResponseCreator.Tests.Fakes
{
    public class Level2DTO : IInputDTO
    {
        public string NameTest { get; set; }

        public Level3DTO Level3Dto { get; set; }

        public void ValidateInput(IResponseCreator responseCreator, string prefix)
        {
            var iv = new InputValidator<Level2DTO>(this, responseCreator, prefix);

            iv.ForString(x => x.NameTest)
                .MinLength(1, NestedDTOValidationTests.TooShortValidationResult);

            this.Level3Dto.ValidateInput(responseCreator, KeyPrefixBuilder.JoinPrefixes(prefix, nameof(Level3Dto)));
        }
    }
}
