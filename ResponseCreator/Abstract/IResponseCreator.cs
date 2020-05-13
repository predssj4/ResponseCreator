using System.Collections.Generic;

namespace ResponseCreator.Abstract
{
    public interface IResponseCreator
    {
        ResponseMetadata<T> CreteResponse<T>(T data);

        ResponseMetadata<object> CreateResponseWithNoData();

        /// <summary>
        /// Insert message validation result under given key.
        /// If already exists - the collection will be merged.
        /// </summary>
        /// <param name="key">Needed</param>
        /// <param name="result">Needed</param>
        void AddValidationResult(string key, string result);

        void AddValidationResult(ValidationResult validationResult);

        void AddValidationResult(string key, IList<string> validationErrors);

        void RemoveValidationResult(string key);

        void AddMessage(string message, BusinessResultType type);
        void RemoveMessage(string message);

        bool IsValid();

        IEnumerable<string> GetValidationResultsForKey(string key);

        int NumberOfValidationResultsForKey(string key);

        /// <summary>
        /// Use only in unexpected situation - this generates issue number displayed for end user
        /// </summary>
        void ChangeResponseStateToFatal();
    }
}
