using System.Linq;
using ResponseCreator.Abstract;
using ResponseCreator.Tests.Assets;
using ResponseCreator.Tests.DataFactories;
using ResponseCreator.Tests.Fakes;
using Shouldly;
using Xunit;

namespace ResponseCreator.Tests.ValidationTests
{
    public class FormFileValidationTests
    {
        private string uniqueKeyForTest = "uniqueKeyForTest";
        private IResponseCreator responseCreator = ResponseCreatorFactory.Create();

        public FormFileValidationTests()
        {
        }

        [Fact]
        public void NotDefault_ProvidedNull_ValidationErrors()
        {
            // arrange
            InputWithFormFileDTO input = new InputWithFormFileDTO()
            {
                File = null
            };

            InputValidator<InputWithFormFileDTO> iv = new InputValidator<InputWithFormFileDTO>(input, responseCreator);

            string customMessage = "IS_DEFAULT";

            // act
            iv.ForFormFile(x => x.File, uniqueKeyForTest )
                .NotDefault(customMessage);

            // assert
            responseCreator.GetValidationResultsForKey(uniqueKeyForTest).First().ShouldBe(customMessage);
        }

        [Fact]
        public void NotDefault_HasSomeValue_NoValidationErrors()
        {
            // arrange
            InputWithFormFileDTO input = InputWithFormFileDTO.Create(2000);

            InputValidator<InputWithFormFileDTO> iv = new InputValidator<InputWithFormFileDTO>(input, responseCreator);

            string customMessage = "IS_DEFAULT";

            // act
            iv.ForFormFile(x => x.File, uniqueKeyForTest)
                .NotDefault(customMessage);

            // assert
            responseCreator.GetValidationResultsForKey(uniqueKeyForTest).ShouldBeEmpty();
        }

        /*
         * 10.000 - 0.01 MB
         * 120.000 - 0.11 MB
         * 200.000 ~ 0.19 MB
         */

        [Fact]
        public void MaxSize_BiggerLengthOfFileThenExpected_ValidationErrors()
        {
            // arrange
            InputWithFormFileDTO input = InputWithFormFileDTO.Create(200000);

            InputValidator<InputWithFormFileDTO> iv = new InputValidator<InputWithFormFileDTO>(input, responseCreator);

            string customMessage = "MAX_SIZE_ERROR";

            // act
            iv.ForFormFile(x => x.File, uniqueKeyForTest)
                .MaxSize(0.15m, customMessage);

            // assert
            responseCreator.GetValidationResultsForKey(uniqueKeyForTest).First().ShouldBe(customMessage);
        }

        [Fact]
        public void MaxSize_SmallerFileThenExpected_ValidationErrors()
        {
            // arrange
            InputWithFormFileDTO input = InputWithFormFileDTO.Create(10000);

            InputValidator<InputWithFormFileDTO> iv = new InputValidator<InputWithFormFileDTO>(input, responseCreator);

            string customMessage = "MAX_SIZE_ERROR";

            // act
            iv.ForFormFile(x => x.File, uniqueKeyForTest)
                .MaxSize(0.15m, customMessage);

            // assert
            responseCreator.GetValidationResultsForKey(uniqueKeyForTest).FirstOrDefault().ShouldBeNull();
        }

        [Fact]
        public void IsImageFile_JPGFile_NoValidationErrors()
        {
            // arrange
            InputWithFormFileDTO input = InputWithFormFileDTO.Create(Base64StaticFiles.jpgFile1, "image4 - Copy.jpg");

            InputValidator<InputWithFormFileDTO> iv = new InputValidator<InputWithFormFileDTO>(input, responseCreator);

            // act
            iv.ForFormFile(x => x.File, this.uniqueKeyForTest)
                .IsImageWithExtension(new []{ "jpg"});

            // assert
            responseCreator.GetValidationResultsForKey(uniqueKeyForTest).ShouldBeEmpty();
        }

        [Fact]
        public void IsImageFile_NotImageFile_ValidationErrors()
        {
            // arrange
            InputWithFormFileDTO input = InputWithFormFileDTO.Create(Base64StaticFiles.configFile1, "NHibernateMappingGenerator.exe.config");

            InputValidator<InputWithFormFileDTO> iv = new InputValidator<InputWithFormFileDTO>(input, responseCreator);

            string customMessage = "FILE_EXTENSION_ERROR";

            string[] allSupportedFileExtensions = new []{ "bmp", "gif87a", "gif89a", "png", "tiffI", "tiffM", "jpeg", "jpg" };

            // act
            iv.ForFormFile(x => x.File, this.uniqueKeyForTest)
                .IsImageWithExtension(allSupportedFileExtensions, customMessage);

            // assert
            responseCreator.GetValidationResultsForKey(uniqueKeyForTest).First().ShouldBe(customMessage);
        }
    }
}
