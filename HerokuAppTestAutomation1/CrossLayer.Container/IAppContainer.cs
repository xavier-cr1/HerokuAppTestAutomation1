using BoDi;

namespace CrossLayer.Container
{
    public interface IAppContainer
    {
        void RegisterAPIs(IObjectContainer objectContainer);
    }
}
