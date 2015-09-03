using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using OnlineBankingForManagers.Domain.Abstract;
using OnlineBankingForManagers.Domain.Concrete;
using OnlineBankingForManagers.WebUI.Infrastructure.Abstract;
using OnlineBankingForManagers.WebUI.Infrastructure.Concrete;

namespace OnlineBankingForManagers.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
    public NinjectControllerFactory()
        {
            
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
    protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
    {
       
        return controllerType == null
          ? null
          : (IController)ninjectKernel.Get(controllerType);
    }
    private void AddBindings()
    {
        ninjectKernel.Bind<IClientRepository>().To<EntityFrameworkClientRepository>();
        ninjectKernel.Bind<IUserProvider>().To<EntityFrameworkUserUserProvider>();
        ninjectKernel.Bind<IAuthCookie>().To<AuthRememberMe>();
    }
    }
}