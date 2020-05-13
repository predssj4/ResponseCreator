using ResponseCreator.Abstract;
using ResponseCreator.Tests.Validation;

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
                .MinLength(1, NastedDTOValidationTests.TooShortValidationResult);

            this.Level2.ValidateInput(responseCreator, nameof(Level2));
        }
    }
}
