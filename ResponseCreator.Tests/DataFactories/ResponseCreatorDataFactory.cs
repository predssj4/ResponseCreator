using ResponseCreator.Abstract;

namespace ResponseCreator.Tests.DataFactories
{
    public class ResponseCreatorDataFactory
    {
        public static IResponseCreator Create()
        {
            return new ResponseCreator();
        }
    }
}
