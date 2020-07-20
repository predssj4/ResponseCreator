using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ResponseCreator.Tests.Utils
{
    class FromStringStreamCreator
    {
        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
    }
}
