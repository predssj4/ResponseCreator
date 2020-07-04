using System;
using System.Collections.Generic;
using System.Text;
using ResponseCreator.Abstract;
using ResponseCreator.Translations;

namespace ResponseCreator.Validators
{
    public class IntValidator : BaseTypeValidator<int>
    {
        public IntValidator(int objectUnderValidation, IResponseCreator responseCreator, string key, string keyPrefix = null) 
            : base(objectUnderValidation, responseCreator, key, keyPrefix)
        {
        }

        public new IntValidator NotDefault(string customMessage = null)
        {
            base.NotDefault(customMessage);

            return this;
        }

        public IntValidator InRange(int min = 0, int max = Int32.MaxValue, string customMessage = null)
        {
            if (!(this.ObjectUnderValidation >= min && this.ObjectUnderValidation < max))
            {
                this.InsertValidationResult(customMessage ?? this.MessagesManager.GetValidationMessageByKey(ValidationMessagesKeys.NumberInRange, min, max));
            }

            return this;
        }
    }
}
