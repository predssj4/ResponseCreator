using System.Collections.Generic;
using System.Dynamic;
using ResponseCreator.Abstract;
using ResponseCreator.Translations;

namespace ResponseCreator.Validators
{
    public class BaseTypeValidator<T>
    {
        protected T ObjectUnderValidation;
        protected IValidationMessagesManager MessagesManager;

        private readonly string _key;
        private readonly IResponseCreator _responseCreator;

        public BaseTypeValidator(T objectUnderValidation, IResponseCreator responseCreator, string key, string keyPrefix = null)
        {
            this.ObjectUnderValidation = objectUnderValidation;
            this._key = CreateKey(key, keyPrefix);
            this._responseCreator = responseCreator;

            this.MessagesManager = ValidationMessagesManager.GetValidationMessagesManager;
        }

        protected void NotDefault(string customMessage = null)
        {
            if (EqualityComparer<T>.Default.Equals(this.ObjectUnderValidation, default(T)))
            {
                this.InsertValidationResult(customMessage ?? this.MessagesManager.GetValidationMessageByKey(ValidationMessagesKeys.IntNotDefault));
            }
        }

        protected void InsertValidationResult(string validationResult)
        {
            _responseCreator.AddValidationResult(this._key, validationResult);
        }

        private string CreateKey(string key, string prefix)
        {
            return KeyPrefixBuilder.CreateKeyWithPrefix(key, prefix);
        }

    }
}
