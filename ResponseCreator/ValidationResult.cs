using System;
using System.Collections.Generic;
using System.Linq;

namespace ResponseCreator
{
    public class ValidationResult
    {
        public string Key { get; private set; }
        public IList<string> Errors { get; private set; }

        public ValidationResult(string key, IList<string> errors)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (errors == null) throw new ArgumentNullException(nameof(errors));
            if (!errors.Any()) throw new ArgumentException("Value cannot be an empty collection.", nameof(errors));

            Key = key;
            Errors = errors;
        }

        public ValidationResult(string key, string validationError) : this(key, new List<string>() {validationError})
        {
        }

        /// <summary>
        /// Use string key to compare
        /// If keys are equals it means that the objects represents the same value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="key">Object's key to compare</param>
        /// <returns></returns>
        public bool Equals(string key)
        {
            return this.Key == key;
        }

        public bool Equals(ValidationResult validationResult)
        {
            return validationResult != null && this.Key == validationResult.Key;
        }

        /// <summary>
        /// Appends error to existing error collection for given key
        /// </summary>
        /// <param name="error"></param>
        public void AppendError(string error)
        {
            if (!this.Errors.Contains(error))
            {
                this.Errors.Add(error);
            }
        }

        public void Merge(ValidationResult validationResult)
        {
            this.Errors = Errors.Concat(validationResult.Errors).ToList();
        }
    }
}
