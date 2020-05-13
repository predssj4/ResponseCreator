using System;
using System.Collections.Generic;

namespace ResponseCreator
{
    public class ResponseMetadata<T>
    {
        /// <summary>
        /// Validation results for properties from input
        /// </summary>
        public IEnumerable<ValidationResult> ValidationResults { get; private set; }

        /// <summary>
        /// Information about business processing
        /// </summary>
        public IEnumerable<MessageResult> Messages { get; private set; }

        /// <summary>
        /// Datetime when response has been created
        /// </summary>
        public long TimeStamp { get; private set; }

        /// <summary>
        /// Defines if request has been successful
        /// </summary>
        public AppResponseStatus Status { get; private set; }

        /// <summary>
        /// Response data
        /// </summary>
        public T Data { get; private set; }

        public ResponseMetadata(T data, IEnumerable<ValidationResult> validationResults, IEnumerable<MessageResult> messages, AppResponseStatus responseStatus)
        {
            this.Data = data;
            this.ValidationResults = validationResults;
            this.Messages = messages;
            this.Status = responseStatus;
            this.TimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }
    }
}
