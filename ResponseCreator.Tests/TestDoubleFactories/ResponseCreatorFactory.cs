using ResponseCreator.Abstract;

namespace ResponseCreator.Tests.DataFactories
{
    public class ResponseCreatorFactory
    {
        public static IResponseCreator Create()
        {
            return new ResponseCreator();
        }
    }
}
