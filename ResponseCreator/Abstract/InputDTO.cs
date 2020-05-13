namespace ResponseCreator.Abstract
{
    public interface IInputDTO
    {
        /// <summary>
        /// Validates this input DTO and saves result to IResponseCreator
        /// </summary>
        /// <param name="responseCreator"></param>
        /// <param name="prefix"></param>
        void ValidateInput(IResponseCreator responseCreator, string prefix = null);
    }
}
