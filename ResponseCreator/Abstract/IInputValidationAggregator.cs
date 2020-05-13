using System.Collections.Generic;

namespace ResponseCreator.Abstract
{
    public interface IInputValidationAggregator
    {
        IList<ValidationResult> GetValidationResult();
        void AddValidationResultForKey(string key, string result);
        void AddValidationErrorsForKey(string key, IList<string> result);
        void RemoveValidationResult(string key);
        bool HasAnyValidationErrors();
        void AddValidationResult(ValidationResult validationResult);
        void MergeOrAdd(ValidationResult validationResult);
        int NumberOfValidationResultsForKey(string key);
    }
}
