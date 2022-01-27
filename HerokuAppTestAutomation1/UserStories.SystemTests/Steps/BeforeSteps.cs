using BoDi;
using TechTalk.SpecFlow;

namespace UserStories.AcceptanceTests
{
    [Binding]
    public class BeforeSteps
    {
        private readonly IObjectContainer objectContainer;

        public BeforeSteps(IObjectContainer objectContainer)
        {

        }
    }
}
