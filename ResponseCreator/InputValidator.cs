using System;
using System.Linq.Expressions;
using System.Reflection;
using ResponseCreator.Abstract;
using ResponseCreator.Extensions;
using ResponseCreator.Validators;
using ResponseCreator.Validators.GoogleRecaptcha;

namespace ResponseCreator
{
    public class InputValidator<TInputDTO>
    {
        private readonly TInputDTO _objectUnderValidation;
        private readonly IResponseCreator _responseCreator;
        private readonly string _prefix;

        public InputValidator(TInputDTO objectUnderValidation, IResponseCreator responseCreator, string prefix = null)
        {
            this._objectUnderValidation = objectUnderValidation;
            this._responseCreator = responseCreator;
            this._prefix = prefix;
        }

        public StringValidator ForString(Expression<Func<TInputDTO, string>> expression, string key = null, string propertyCustomPrefix = null) => this.CreateSpecifiedValidatorFromGeneric<StringValidator, string>(expression, key, propertyCustomPrefix);
        public IntValidator ForInt(Expression<Func<TInputDTO, int>> expression, string key = null, string propertyCustomPrefix = null) => this.CreateSpecifiedValidatorFromGeneric<IntValidator, int>(expression, key, propertyCustomPrefix);
        public GoogleReCaptchaValidator ForReCaptcha (Expression<Func<TInputDTO, string>> expression, string key = null, string propertyCustomPrefix = null) => this.CreateSpecifiedValidatorFromGeneric<GoogleReCaptchaValidator, string>(expression, key, propertyCustomPrefix);

        private TValidatior CreateSpecifiedValidatorFromGeneric<TValidatior, TPropertyType>(Expression<Func<TInputDTO, TPropertyType>> expression, string key = null, string propertyCustomPrefix = null) 
            where TValidatior : BaseTypeValidator<TPropertyType>
        {
            TPropertyType selectedPropertyFromExpression = expression.Compile().Invoke(_objectUnderValidation);

            string currentKey = key ?? expression.GetNameFromMemberExpression();

            var prefix = propertyCustomPrefix ?? _prefix;

            var instance =  (TValidatior)Activator.CreateInstance(typeof(TValidatior), args: new object[] {selectedPropertyFromExpression, this._responseCreator, currentKey, prefix });

            return instance;
        }
    }
}