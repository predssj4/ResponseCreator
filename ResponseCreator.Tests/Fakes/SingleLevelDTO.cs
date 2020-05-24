using System;
using System.Collections.Generic;
using System.Text;
using ResponseCreator.Abstract;

namespace ResponseCreator.Tests.Fakes
{
    public class SingleLevelDTO : IInputDTO
    {
        public string NameNotAccidentalCreated { get; set; }
        public string SecondProperty { get; set; }

        public void ValidateInput(IResponseCreator responseCreator, string prefix = null)
        {
            var iv = new InputValidator<SingleLevelDTO>(this, responseCreator, prefix);

            iv.ForString(x => x.NameNotAccidentalCreated)
                .MinLength(1);
        }

        public void ValidateInputWithCustomPropertyName(IResponseCreator responseCreator, string prefix = null)
        {
            var iv = new InputValidator<SingleLevelDTO>(this, responseCreator, prefix);

            iv.ForString(x => x.NameNotAccidentalCreated, "CustomPropertyNameCreated")
                .MinLength(1);
        }

        public void ValidateInputWithCustomPropertyNameAndCustomPrefix(IResponseCreator responseCreator, string prefix, string customPrefixForSingleProperty)
        {
            var iv = new InputValidator<SingleLevelDTO>(this, responseCreator, prefix);

            iv.ForString(x => x.NameNotAccidentalCreated, "CustomPropertyNameCreated", customPrefixForSingleProperty)
                .MinLength(1);
        }
    }
}
