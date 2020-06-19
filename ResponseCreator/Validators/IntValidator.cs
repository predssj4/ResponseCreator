using System;
using System.Collections.Generic;
using System.Text;
using ResponseCreator.Abstract;

namespace ResponseCreator.Validators
{
    public class IntValidator : BaseTypeValidator<int>
    {
        public IntValidator(int objectUnderValidation, IResponseCreator responseCreator, string key, string keyPrefix = null) : base(objectUnderValidation, responseCreator, key, keyPrefix)
        {
        }

        public IntValidator Required(string customMessage = null)
        {
            
        }
    }
}
