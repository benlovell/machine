using System;
using System.Collections.Generic;

using Machine.Container.Model;
using Machine.Container.Plugins;

namespace Machine.Container.Services.Impl
{
  [CoverageExclude]
  public class ContainerServices : IContainerServices
  {
    private readonly ContainerStatePolicy _statePolicy;
    private readonly IActivatorFactory _activatorFactory;
    private readonly IActivatorStore _activatorStore;
    private readonly ILifestyleFactory _lifestyleFactory;
    private readonly IServiceEntryFactory _serviceEntryFactory;
    private readonly IServiceEntryResolver _serviceEntryResolver;
    private readonly IListenerInvoker _listenerInvoker;
    private readonly IObjectInstances _objectInstances;
    private readonly IServiceGraph _serviceGraph;
    private readonly IResolvableTypeMap _resolvableTypeMap;

    public ContainerServices(IActivatorStore activatorStore, IActivatorFactory activatorFactory, ILifestyleFactory lifestyleFactory, IListenerInvoker listenerInvoker, IObjectInstances objectInstances, IServiceEntryFactory serviceEntryFactory, IServiceEntryResolver serviceEntryResolver, IServiceGraph serviceGraph, ContainerStatePolicy statePolicy, IResolvableTypeMap resolvableTypeMap)
    {
      _activatorStore = activatorStore;
      _resolvableTypeMap = resolvableTypeMap;
      _serviceEntryFactory = serviceEntryFactory;
      _statePolicy = statePolicy;
      _serviceGraph = serviceGraph;
      _activatorFactory = activatorFactory;
      _lifestyleFactory = lifestyleFactory;
      _listenerInvoker = listenerInvoker;
      _objectInstances = objectInstances;
      _serviceEntryResolver = serviceEntryResolver;
    }

    public ContainerStatePolicy StatePolicy
    {
      get { return _statePolicy; }
    }

    public IActivatorFactory ActivatorFactory
    {
      get { return _activatorFactory; }
    }

    public IActivatorStore ActivatorStore
    {
      get { return _activatorStore; }
    }

    public ILifestyleFactory LifestyleFactory
    {
      get { return _lifestyleFactory; }
    }

    public IServiceEntryFactory ServiceEntryFactory
    {
      get { return _serviceEntryFactory; }
    }

    public IServiceEntryResolver ServiceEntryResolver
    {
      get { return _serviceEntryResolver; }
    }

    public IListenerInvoker ListenerInvoker
    {
      get { return _listenerInvoker; }
    }

    public IObjectInstances ObjectInstances
    {
      get { return _objectInstances; }
    }

    public IServiceGraph ServiceGraph
    {
      get { return _serviceGraph; }
    }

    public IResolvableTypeMap ResolvableTypeMap
    {
      get { return _resolvableTypeMap; }
    }

    public IResolutionServices CreateResolutionServices(IOverrideLookup overrides, LookupFlags flags)
    {
      return new ResolutionServices(this, overrides, flags);
    }
  }
}