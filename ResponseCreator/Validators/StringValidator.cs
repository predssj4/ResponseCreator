using System.Diagnostics;
using System.Threading;
using ResponseCreator.Abstract;
using ResponseCreator.Extensions.String;
using ResponseCreator.Translations;

namespace ResponseCreator.Validators
{
    public class StringValidator : BaseTypeValidator<string>
    {
        public StringValidator(string @object, IResponseCreator responseCreator, string key, string prefix = null) : base(@object, responseCreator, key, prefix) { }

        /// <summary>
        /// Shouldn't be null, empty or shorter then min length provided as parameter
        /// </summary>
        /// <param name="minLength"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public StringValidator MinLength(int minLength, string customMessage = null)
        {
            if (string.IsNullOrWhiteSpace(this.ObjectUnderValidation) || this.ObjectUnderValidation.Length < minLength)
            {
                this.InsertValidationResult(customMessage ?? this.MessagesManager.GetValidationMessageByKey(ValidationMessagesKeys.NoShorterThen, minLength));
            }

            return this;
        }

        /// <summary>
        /// Could be null or reaches max length provided as parameter
        /// </summary>
        /// <param name="maxLength"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public StringValidator MaxLength(int maxLength, string customMessage = null)
        {
            if (this.ObjectUnderValidation != null && this.ObjectUnderValidation.Length > maxLength)
            {
                this.InsertValidationResult(customMessage ?? this.MessagesManager.GetValidationMessageByKey(ValidationMessagesKeys.NoShorterThen, maxLength));
            }

            return this;
        }

        public new StringValidator NotDefault(string customMessage = null)
        {
            base.NotDefault(customMessage);

            return this;
        }
    }
}
