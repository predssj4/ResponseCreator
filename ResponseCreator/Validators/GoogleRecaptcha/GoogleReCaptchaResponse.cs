using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ResponseCreator.Validators.GoogleRecaptcha
{
    public class GoogleReCaptchaResponse
    {
        private string m_Success;
        private List<string> m_ErrorCodes;

        [JsonProperty("success")]
        public string Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }
    }
}
