using ResponseCreator.Abstract;
using ResponseCreator.Translations;
using ResponseCreator.Utils;

namespace ResponseCreator.Validators
{
    public class LongValidator : BaseTypeValidator<long>
    {
        public LongValidator(long objectUnderValidation, IResponseCreator responseCreator, string key, string keyPrefix = null)
            : base(objectUnderValidation, responseCreator, key, keyPrefix)
        {
        }

        public new LongValidator NotDefault(string customMessage = null)
        {
            base.NotDefault(customMessage);

            return this;
        }

        public LongValidator InRange(long min = 0, long max = long.MaxValue, string customMessage = null)
        {
            if (!(this.ObjectUnderValidation >= min && this.ObjectUnderValidation <= max))
            {
                this.InsertValidationResult(customMessage ?? this.MessagesManager.GetValidationMessageByKey(ValidationMessagesKeys.NumberInRange, min, max));
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxSize">In megabytes</param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public LongValidator MaxSize(decimal maxSize, string customMessage = null)
        {
            if (SizeConvertionUtil.FromByteLongToDecimalMB(this.ObjectUnderValidation) > maxSize)
            {
                this.InsertValidationResult(customMessage ?? this.MessagesManager.GetValidationMessageByKey(ValidationMessagesKeys.FileTooBig));
            }

            return this;
        }
    }
}
