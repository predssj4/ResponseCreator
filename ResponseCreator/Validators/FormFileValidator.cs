using Microsoft.AspNetCore.Http;
using ResponseCreator.Abstract;
using ResponseCreator.Translations;
using ResponseCreator.Utils;
using ResponseCreator.Validators.ImageValidation;

namespace ResponseCreator.Validators
{
    public class FormFileValidator : BaseTypeValidator<IFormFile>
    {
        public FormFileValidator(IFormFile objectUnderValidation, IResponseCreator responseCreator, string key, string keyPrefix = null) : base(objectUnderValidation, responseCreator, key, keyPrefix)
        {
        }

        public new FormFileValidator NotDefault(string customMessage = null)
        {
            base.NotDefault(customMessage);

            return this;
        }

        public FormFileValidator MaxSize(decimal maxSize, string customMessage = null)
        {
            if (SizeConvertionUtil.FromByteLongToDecimalMB(this.ObjectUnderValidation.Length) > maxSize)
            {
                this.InsertValidationResult(customMessage ?? this.MessagesManager.GetValidationMessageByKey(ValidationMessagesKeys.FileTooBig));
            }

            return this;
        }

        public FormFileValidator IsImageWithExtension(string[] imageFileExtensions, string customMessage = null)
        {
            if (!ImageHeaderValidator.IsValidImageFile(this.ObjectUnderValidation.OpenReadStream(), imageFileExtensions))
            {
                this.InsertValidationResult(customMessage ?? this.MessagesManager.GetValidationMessageByKey(ValidationMessagesKeys.WrongFileFormat, string.Join(",", imageFileExtensions)));
            }

            return this;
        }
    }
}
