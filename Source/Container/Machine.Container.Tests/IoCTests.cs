using System;
using System.Collections.Generic;

using Machine.Container.Services;

using NUnit.Framework;

namespace Machine.Container
{
  [TestFixture]
  public class IoCTests : MachineContainerTestsFixture
  {
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Container_WhenItsNull_Throws()
    {
      IoC.Container = null;
      IMachineContainer container = IoC.Container;
    }

    [Test]
    public void Container_GetAndSet_Works()
    {
      IMachineContainer container = _mocks.DynamicMock<IMachineContainer>();
      IoC.Container = container;
      Assert.AreEqual(container, IoC.Container);
    }
  }
}