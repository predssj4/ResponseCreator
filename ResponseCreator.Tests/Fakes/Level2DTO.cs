using ResponseCreator.Abstract;
using ResponseCreator.Tests.Validation;

namespace ResponseCreator.Tests.Fakes
{
    public class Level2DTO : IInputDTO
    {
        public string NameTest { get; set; }

        public void ValidateInput(IResponseCreator responseCreator, string prefix)
        {
            var iv = new InputValidator<Level2DTO>(this, responseCreator, prefix);

            iv.ForString(x => x.NameTest)
                .MinLength(1, NastedDTOValidationTests.TooShortValidationResult);
        }
    }
}
