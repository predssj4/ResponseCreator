using ResponseCreator.Abstract;

namespace ResponseCreator.Validators
{
    public class BaseTypeValidator<T>
    {
        private readonly string _key;

        protected T ObjectUnderValidation;
        private readonly IResponseCreator _responseCreator;

        public BaseTypeValidator(T objectUnderValidation, IResponseCreator responseCreator, string key, string keyPrefix = null)
        {
            this.ObjectUnderValidation = objectUnderValidation;
            this._key = CreateKey(key, keyPrefix);
            this._responseCreator = responseCreator;
        }

        public void InsertValidationResult(string validationResult)
        {
            _responseCreator.AddValidationResult(this._key, validationResult);
        }

        private string CreateKey(string key, string prefix)
        {
            return KeyPrefixBuilder.CreateKeyWithPrefix(key, prefix);
        }
    }
}
