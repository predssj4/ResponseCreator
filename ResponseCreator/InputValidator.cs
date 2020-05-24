using System;
using System.Linq.Expressions;
using ResponseCreator.Abstract;
using ResponseCreator.Extensions;
using ResponseCreator.Validators;

namespace ResponseCreator
{
    public class InputValidator<T>
    {
        private readonly T _objectUnderValidation;
        private readonly IResponseCreator _responseCreator;
        private readonly string _prefix;

        public InputValidator(T objectUnderValidation, IResponseCreator responseCreator, string prefix = null)
        {
            this._objectUnderValidation = objectUnderValidation;
            this._responseCreator = responseCreator;
            this._prefix = prefix;
        }

        public StringValidator ForString(Expression<Func<T, string>> expression, string key = null, string propertyCustomPrefix = null)
        {
            string result = expression.Compile().Invoke(_objectUnderValidation);

            string currentKey = key ?? expression.GetNameFromMemberExpression(); 

            return new StringValidator(result, this._responseCreator, currentKey, propertyCustomPrefix ?? _prefix);
        }
    }
}