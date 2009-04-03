using System;
using System.Collections.Generic;

using Machine.Container.Model;
using Machine.Container.Plugins;
using NUnit.Framework;

namespace Machine.Container.Services.Impl
{
  [TestFixture]
  public class PluginManager_NotInitialized : ScaffoldTests<PluginManager>
  {
    private PluginServices _pluginServices;

    [Test]
    public void AddPlugin_Always_Just_Adds_It()
    {
      _target.AddPlugin(_mocks.DynamicMock<IServiceContainerPlugin>());
      _target.AddPlugin(_mocks.DynamicMock<IServiceContainerPlugin>());
    }

    [Test]
    public void Initialize_With_Plugin_Initializes_It()
    {
      IServiceContainerPlugin plugin = _mocks.DynamicMock<IServiceContainerPlugin>();
      _target.AddPlugin(plugin);
      plugin.Initialize(_pluginServices);

      _mocks.ReplayAll();
      _target.Initialize(_pluginServices);

      _mocks.Verify(plugin);
    }

    protected override PluginManager Create()
    {
      _pluginServices = new PluginServices(new ContainerStatePolicy(), Get<IMachineContainer>(), Get<IRootActivatorResolver>(), Get<IRootActivatorFactory>());
      return new PluginManager();
    }
  }

  [TestFixture]
  public class PluginManager_Initialized : ScaffoldTests<PluginManager>
  {
    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AddPlugin_Throws()
    {
      _target.Initialize(new PluginServices(new ContainerStatePolicy(), Get<IMachineContainer>(), Get<IRootActivatorResolver>(), Get<IRootActivatorFactory>()));
      _target.AddPlugin(_mocks.DynamicMock<IServiceContainerPlugin>());
    }
  }
}
