using System.Collections.Generic;
using System.IO;

namespace ResponseCreator.Validators.ImageValidation
{
    class ImageHeaderValidator
    {
        private static IDictionary<string, byte[]> imageHeaders = new Dictionary<string, byte[]>()
        {
            {"bmp", new byte[] {0x42, 0x4D}},
            {"gif87a", new byte[] {0x47, 0x49, 0x46, 0x38, 0x37, 0x61}},
            {"gif89a", new byte[] {0x47, 0x49, 0x46, 0x38, 0x39, 0x61}},
            {"png", new byte[] {0x89, 0x50, 0x4e, 0x47, 0x0D, 0x0A, 0x1A, 0x0A}},
            {"tiffI", new byte[] {0x49, 0x49, 0x2A, 0x00}},
            {"tiffM", new byte[] {0x4D, 0x4D, 0x00, 0x2A}},
            {"jpeg", new byte[] {0xFF, 0xD8, 0xFF}},
            {"jpg", new byte[] {0xFF, 0xD8}}
        };

        private static byte[] jpegEnd = { 0xFF, 0xD9 };

        /// <summary>
        /// Reads the header of different image formats
        /// </summary>
        /// <param name="file">Image file</param>
        /// <returns>true if valid file signature (magic number/header marker) is found</returns>
        public static bool IsValidImageFile(Stream fs, string[] imageFileExtensions)
        {
            byte[] buffer = new byte[8];
            byte[] bufferEnd = new byte[2];

            GetFileHeadersIntoBuffers(fs, buffer, bufferEnd);

            fs.Close();

            foreach (var imageFileExtension in imageFileExtensions)
            {
                if (imageFileExtension == "jpeg")
                {
                    // Offset 0 (Two Bytes): JPEG SOI marker (FFD8 hex)
                    // Offest 1 (Two Bytes): Application segment (FF?? normally ??=E0)
                    // Trailer (Last Two Bytes): EOI marker FFD9 hex
                    if (ByteArrayStartsWith(buffer, imageHeaders[imageFileExtension]) && ByteArrayStartsWith(bufferEnd, jpegEnd))
                    {
                        return true;
                    }
                }
                else
                {
                    if (ByteArrayStartsWith(buffer, imageHeaders[imageFileExtension]))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        private static void GetFileHeadersIntoBuffers(Stream fs, byte[] buffer, byte[] bufferEnd)
        {
            if (fs.Length > buffer.Length)
            {
                fs.Read(buffer, 0, buffer.Length);
                fs.Position = (int) fs.Length - bufferEnd.Length;
                fs.Read(bufferEnd, 0, bufferEnd.Length);
            }
        }

        /// <summary>
        /// Returns mainArray value indicating whether mainArray specified subarray occurs within array
        /// </summary>
        /// <param name="mainArray">Main array</param>
        /// <param name="subArray">Subarray to seek within main array</param>
        /// <returns>true if mainArray array starts with subArray subarray or if subArray is empty; otherwise false</returns>
        private static bool ByteArrayStartsWith(byte[] mainArray, byte[] subArray)
        {
            if (mainArray.Length < subArray.Length)
            {
                return false;
            }

            for (int i = 0; i < subArray.Length; i++)
            {
                if (mainArray[i] != subArray[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}
