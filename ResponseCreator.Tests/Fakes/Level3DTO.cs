using System;
using System.Collections.Generic;
using System.Text;
using ResponseCreator.Abstract;

namespace ResponseCreator.Tests.Fakes
{
    public class Level3DTO : IInputDTO
    {
        public string PropertyLevel3 { get; set; }

        public void ValidateInput(IResponseCreator responseCreator, string prefix = null)
        {
            var iv = new InputValidator<Level3DTO>(this, responseCreator, prefix);

            iv.ForString(x => x.PropertyLevel3)
                .MinLength(1);
        }
    }
}
