using ResponseCreator.Abstract;
using ResponseCreator.Translations;

namespace ResponseCreator.Validators
{
    public class BoolValidator : BaseTypeValidator<bool>
    {
        public BoolValidator(bool objectUnderValidation, IResponseCreator responseCreator, string key, string keyPrefix = null) : base(objectUnderValidation, responseCreator, key, keyPrefix)
        {
        }

        public BoolValidator IsTrue(string customMessage = null)
        {
            if (!this.ObjectUnderValidation)
            {
                this.InsertValidationResult(customMessage ?? this.MessagesManager.GetValidationMessageByKey(ValidationMessagesKeys.IsNotTrue));
            }

            return this;
        }

        public BoolValidator IsFalse(string customMessage = null)
        {
            if (this.ObjectUnderValidation)
            {
                this.InsertValidationResult(customMessage ?? this.MessagesManager.GetValidationMessageByKey(ValidationMessagesKeys.IsTrue));
            }

            return this;
        }
    }
}
