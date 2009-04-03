using System;
using System.Collections.Generic;

using Machine.Container.Activators;
using Machine.Container.Model;

using NUnit.Framework;

namespace Machine.Container.Services.Impl
{
  [TestFixture]
  public class DefaultActivatorStrategyTests : MachineContainerTestsFixture
  {
    private IObjectFactory _objectFactory;
    private IServiceDependencyInspector _serviceDependencyInspector;
    private IServiceEntryResolver _serviceEntryResolver;
    private DefaultActivatorFactory _activatorFactory;
    private ServiceEntry _entry;

    public override void Setup()
    {
      base.Setup();
      _entry = ServiceEntryHelper.NewEntry();
      _objectFactory = _mocks.StrictMock<IObjectFactory>();
      _serviceDependencyInspector = _mocks.DynamicMock<IServiceDependencyInspector>();
      _serviceEntryResolver = _mocks.DynamicMock<IServiceEntryResolver>();
      _activatorFactory = new DefaultActivatorFactory(_objectFactory, _serviceDependencyInspector, _serviceEntryResolver);
    }

    [Test]
    public void CreateActivatorInstance_ReturnsInstanceActivator_ReturnsSameOne()
    {
      Assert.IsInstanceOfType(typeof(StaticActivator), _activatorFactory.CreateStaticActivator(_entry, new SimpleService1()));
    }

    [Test]
    public void CreateDefaultActivator_Always_Is_Property_Setting_Activator_For_Now()
    {
      Assert.IsInstanceOfType(typeof(PropertySettingActivator), _activatorFactory.CreateDefaultActivator(_entry));
    }
  }
}