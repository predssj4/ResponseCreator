using System;
using System.Collections.Generic;
using System.Linq;
using ResponseCreator.Abstract;

namespace ResponseCreator
{
    public class InputValidationAggregator : IInputValidationAggregator
    {
        private readonly IList<ValidationResult> _validationResults;

        public InputValidationAggregator()
        {
            this._validationResults = new List<ValidationResult>();
        }

        public IList<ValidationResult> GetValidationResult()
        {
            return this._validationResults;
        }

        public void AddValidationResultForKey(string key, string result)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(nameof(key));
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                throw new ArgumentException(nameof(result));
            }

            var validationResultsEntry = this._validationResults.FirstOrDefault(x => x.Equals(key));
            if (validationResultsEntry == null)
            {
                this._validationResults.Add(new ValidationResult(key, new List<string> {result}));
            }
            else
            {
                validationResultsEntry.AppendError(result);
            }
        }

        public void AddValidationErrorsForKey(string key, IList<string> results)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(nameof(key));
            }

            if (results == null || !results.Any())
            {
                throw new ArgumentException(nameof(results));
            }

            var validationResultsEntry = this._validationResults.FirstOrDefault(x => x.Equals(key));
            if (validationResultsEntry != null)
            {
                throw new ArgumentException($@"There are already some validation results for key {key}.
                                                    If you want to combine many validation results into one use Merge method instead.");
            }
            else
            {
                this._validationResults.Add(new ValidationResult(key, results));
            }
        }

        public void RemoveValidationResult(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            var validationResultToRemove = this._validationResults.FirstOrDefault(x => x.Equals(key));
            if (validationResultToRemove != null)
            {
                this._validationResults.Remove(validationResultToRemove);
            }
        }

        public bool HasAnyValidationErrors()
        {
            return this._validationResults.Any();
        }

        public void AddValidationResult(ValidationResult validationResult)
        {
            if (validationResult == null) throw new ArgumentNullException(nameof(validationResult));

            if (this._validationResults.Any(x => x.Equals(validationResult)))
            {
                throw new ArgumentException($@"There are already some validation results for key {validationResult.Key}.
                                                    If you want to combine many validation results into one use MergeOrAdd method instead.");
            }
            else
            {
                this._validationResults.Add(validationResult);
            }
        }

        public void MergeOrAdd(ValidationResult validationResult)
        {
            var currentValidationResult = this._validationResults.FirstOrDefault(x => x.Equals(validationResult));

            if (currentValidationResult != null)
            {
                currentValidationResult.Merge(validationResult);
            }
            else
            {
                this._validationResults.Add(validationResult);
            }
        }

        public int NumberOfValidationResultsForKey(string key)
        {
            var validationResult = this._validationResults.FirstOrDefault(vr => vr.Equals(key));

            if (validationResult != null)
            {
                return validationResult.Errors.Count();
            }

            return 0;
        }
    }
}
