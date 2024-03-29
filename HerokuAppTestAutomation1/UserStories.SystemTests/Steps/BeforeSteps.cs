﻿using BoDi;
using CrossLayer.Container;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace UserStories.AcceptanceTests
{
    [Binding]
    public class BeforeSteps
    {
        private readonly IObjectContainer objectContainer;
        private readonly IAppContainer appContainers;

        public BeforeSteps(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;

            // Inject app containers.
            this.RegisterAppContainerToObjectContainer();
            this.appContainers = this.objectContainer.Resolve<IAppContainer>();

            // Inject configuration to object container
            this.RegisterConfigurationToObjectContainer();
        }

        /// <summary>
        /// Sets up API scenarios. Triggers appcontainers
        /// </summary>
        [BeforeScenario]
        [Scope(Tag = "Type:API")]
        public void SetUpAPIScenarios()
        {
            this.appContainers.RegisterAPIs(this.objectContainer);
        }

        private void RegisterConfigurationToObjectContainer()
        {
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var environment = configurationRoot.GetSection("AppConfiguration")["Environment"];

            var configurationEnvironment = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();

            this.objectContainer.RegisterInstanceAs(configurationEnvironment);
        }

        private void RegisterAppContainerToObjectContainer()
        {
            this.objectContainer.RegisterTypeAs<AppContainer, IAppContainer>();
        }
    }
}
