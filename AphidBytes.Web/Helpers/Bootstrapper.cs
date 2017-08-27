using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.Mvc4;

namespace AphidBytes.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer InitialiUnityContainer()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));


            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            IUnityContainer ContainerUnity = new UnityContainer();
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configuration\UnityConfiguration.config") };

            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            var unitySection = (UnityConfigurationSection)configuration.GetSection("unity");
            ContainerUnity.LoadConfiguration(unitySection, "Accounts");
            ContainerUnity.LoadConfiguration(unitySection, "AphidTise");
            ContainerUnity.LoadConfiguration(unitySection, "Byter");
            ContainerUnity.LoadConfiguration(unitySection, "Basic");
            ContainerUnity.LoadConfiguration(unitySection, "Common");
            ContainerUnity.LoadConfiguration(unitySection, "Premium");
            ContainerUnity.LoadConfiguration(unitySection, "Home");
            ContainerUnity.LoadConfiguration(unitySection, "SocialNetwork");
            ContainerUnity.LoadConfiguration(unitySection, "FeedBack");
            ContainerUnity.LoadConfiguration(unitySection, "UserSponsored");
            ContainerUnity.LoadConfiguration(unitySection, "Chat");
            ContainerUnity.LoadConfiguration(unitySection, "AphidLab");
            return ContainerUnity;
        }


    }
}