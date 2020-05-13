using Autofac;
using ResponseCreator.Abstract;

namespace ResponseCreator.DI
{
    public class ResponseCreatorAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ResponseCreator>().As<IResponseCreator>().InstancePerLifetimeScope();
            builder.RegisterType<InputValidationAggregator>().As<IInputValidationAggregator>().InstancePerLifetimeScope();
        }
    }
}
