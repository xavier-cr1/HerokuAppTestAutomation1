using APILayer.Client;
using APILayer.Client.Contracts;
using BoDi;

namespace CrossLayer.Container
{
    public class AppContainer : IAppContainer
    {
        public void RegisterAPIs(IObjectContainer objectContainer)
        {
            //Register API's
            objectContainer.RegisterTypeAs<PersonRestService, IPersonRestService>();
        }
    }
}
