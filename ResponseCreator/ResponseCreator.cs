using System.Collections.Generic;
using System.Linq;
using ResponseCreator.Abstract;

namespace ResponseCreator
{
    public class ResponseCreator : IResponseCreator
    {
        private readonly IList<MessageResult> _messages;
        private readonly IInputValidationAggregator _inputValidationAggregator;

        public IEnumerable<ValidationResult> ValidationResults { get { return this._inputValidationAggregator.GetValidationResult(); } }
        public IEnumerable<MessageResult> Messages { get { return this._messages; } }
        public AppResponseStatus Status { get; private set; }

        public ResponseCreator()
        {
            this._inputValidationAggregator = new InputValidationAggregator();
            this._messages = new List<MessageResult>();
        }

        public ResponseMetadata<T> CreteResponse<T>(T data)
        {
            return new ResponseMetadata<T>(data, this._inputValidationAggregator.GetValidationResult(), this._messages, this.Status);
        }

        public ResponseMetadata<object> CreateResponseWithNoData()
        {
            return this.CreteResponse<object>(null);
        }

        public bool IsValid()
        {
            return !HasAnyValidationOrBusinessErrors();
        }

        public IEnumerable<string> GetValidationResultsForKey(string key)
        {
            var validationResult =  this._inputValidationAggregator.GetValidationResult()
                .FirstOrDefault(x => x.Key == key);

            if (validationResult != null)
            {
                return validationResult.Errors;
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }

        public int NumberOfValidationResultsForKey(string key)
        {
            return this._inputValidationAggregator.NumberOfValidationResultsForKey(key);
        }

        public void ChangeResponseStateToFatal()
        {
            this.Status = AppResponseStatus.Fatal;
        }

        public void AddValidationResult(string key, string result)
        {
            this._inputValidationAggregator.AddValidationResultForKey(key, result);
            this.Status = AppResponseStatus.Errors;
        }

        public void AddValidationResult(ValidationResult validationResult)
        {
            this._inputValidationAggregator.AddValidationResult(validationResult);
            this.Status = AppResponseStatus.Errors;
        }

        public void AddValidationResult(string key, IList<string> validationErrors)
        {
            this._inputValidationAggregator.AddValidationErrorsForKey(key, validationErrors);
            this.Status = AppResponseStatus.Errors;
        }

        public void RemoveValidationResult(string key)
        {
            this._inputValidationAggregator.RemoveValidationResult(key);
            this.CheckAndChangeStatus();
        }

        public void AddMessage(string message, BusinessResultType type)
        {
            this._messages.Add(new MessageResult() { Message = message, Type = type });

            if (type == BusinessResultType.Error)
            {
                this.Status = AppResponseStatus.Errors;
            }
        }

        public void AddSuccessMessage(string message)
        {
            this.AddMessage(message, BusinessResultType.Success);
        }

        public void AddErrorMessage(string message)
        {
            this.AddMessage(message, BusinessResultType.Error);
        }

        public void AddWarningMessage(string message)
        {
            this.AddMessage(message, BusinessResultType.Warning);
        }

        public void AddInfoMessage(string message)
        {
            this.AddMessage(message, BusinessResultType.Info);
        }

        public void RemoveMessage(string message)
        {
            this._messages.Remove(this._messages.FirstOrDefault(x => x.Message == message));
        }

        private void CheckAndChangeStatus()
        {
            if (!HasAnyValidationOrBusinessErrors())
            {
                this.Status = default(AppResponseStatus);
            }
            else
            {
                this.Status = AppResponseStatus.Errors;
            }
        }

        private bool HasAnyValidationOrBusinessErrors()
        {
            return this._inputValidationAggregator.HasAnyValidationErrors() || this._messages.Any(x => x.Type == BusinessResultType.Error);
        }
    }
}
