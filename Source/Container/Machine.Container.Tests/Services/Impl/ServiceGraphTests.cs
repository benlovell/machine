using System;
using System.Collections.Generic;

using Machine.Container.Model;

using NUnit.Framework;

namespace Machine.Container.Services.Impl
{
  [TestFixture]
  public class ServiceGraphTests : ScaffoldTests<ServiceGraph>
  {
    [Test]
    public void Lookup_NotInTheGraph_IsNull()
    {
      Assert.IsNull(_target.Lookup(typeof(IService1)));
    }

    [Test]
    public void Lookup_InTheGraph_IsThatEntry()
    {
      ServiceEntry entry = ServiceEntryHelper.NewEntry();
      _target.Add(entry);
      Assert.AreEqual(entry, _target.Lookup(typeof(IService1)));
    }

    [Test]
    public void RegisteredServices_IsEmpty_IsEmpty()
    {
      List<ServiceRegistration> actual = new List<ServiceRegistration>(_target.RegisteredServices);
      CollectionAssert.IsEmpty(actual);
    }

    [Test]
    public void RegisteredServices_NotEmpty_HasThatEntry()
    {
      ServiceEntry entry = ServiceEntryHelper.NewEntry();
      _target.Add(entry);
      List<ServiceRegistration> actual = new List<ServiceRegistration>(_target.RegisteredServices);
      Assert.AreEqual(entry.ServiceType, actual[0].ServiceType);
      Assert.AreEqual(entry.ServiceType, actual[0].ServiceType);
    }

    [Test]
    public void Lookup_SubclassInGraph_IsThatEntry()
    {
      ServiceEntry entry = ServiceEntryHelper.NewEntry(typeof(Service1DependsOn2));
      _target.Add(entry);
      Assert.AreEqual(entry, _target.Lookup(typeof(IService1)));
    }

    [Test]
    public void Lookup_SubclassInGraphThatDoesntHaveConcreteType_IsThatEntry()
    {
      ServiceEntry entry = ServiceEntryHelper.NewEntry(typeof(IService1));
      _target.Add(entry);
      Assert.AreEqual(entry, _target.Lookup(typeof(IService1)));
    }

    [Test]
    [ExpectedException(typeof(AmbiguousServicesException))]
    public void Lookup_MultipleSubclassesInGraph_Throws()
    {
      _target.Add(ServiceEntryHelper.NewEntry(typeof(Service1DependsOn2)));
      _target.Add(ServiceEntryHelper.NewEntry(typeof(Service1)));
      _target.Lookup(typeof(IService1));
    }

    [Test]
    public void Lookup_MultipleSubclassesInGraphNotThrowing_Throws()
    {
      _target.Add(ServiceEntryHelper.NewEntry(typeof(Service1DependsOn2)));
      _target.Add(ServiceEntryHelper.NewEntry(typeof(Service1)));
      Assert.IsNull(_target.Lookup(typeof(IService1), LookupFlags.ThrowIfUnable));
    }
  }
}