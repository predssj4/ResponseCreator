using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using ResponseCreator.Tests.Utils;

namespace ResponseCreator.Tests.Fakes
{
    public class InputWithFormFileDTO
    {
        public IFormFile File { get; set; }

        public static InputWithFormFileDTO Create(long fileLength)
        {
            return new InputWithFormFileDTO()
            {
                File = new FormFile(FromStringStreamCreator.GenerateStreamFromString("some value"), 0, fileLength, "some name", "filename.txt")
            };
        }

        public static InputWithFormFileDTO Create(string base64File, string fileName)
        {
            var memSteram = new MemoryStream(Convert.FromBase64String(base64File));

            return new InputWithFormFileDTO()
            {
                File = new FormFile(memSteram, 0, memSteram.Length, "someName", fileName)
            };
        }
    }
}
