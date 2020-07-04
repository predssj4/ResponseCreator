using ResponseCreator.Abstract;
using ResponseCreator.Translations;

namespace ResponseCreator.Validators.GoogleRecaptcha
{
    public class GoogleReCaptchaValidator : BaseTypeValidator<string>
    {
        public GoogleReCaptchaValidator(string objectUnderValidation, IResponseCreator responseCreator, string key, string keyPrefix = null) 
            : base(objectUnderValidation, responseCreator, key, keyPrefix)
        {
        }

        public GoogleReCaptchaValidator ValidateCaptcha(string privateKey, string customMessage = null)
        {
            var client = new System.Net.WebClient();

            var googleReply = client.DownloadString($"https://www.google.com/recaptcha/api/siteverify?secret={privateKey}&response={this.ObjectUnderValidation}");

            var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleReCaptchaResponse>(googleReply);

            if (captchaResponse.Success.ToLower() == "false")
            {
                this.InsertValidationResult(customMessage ?? this.MessagesManager.GetValidationMessageByKey(ValidationMessagesKeys.InvalidCaptcha));
            }

            return this;
        }
    }
}
