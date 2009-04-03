using System;
using System.Collections.Generic;
using System.Threading;

using Machine.Container.Model;
using Machine.Core.Utility;

namespace Machine.Container.Services.Impl
{
  public class ActivatorStore : IActivatorStore
  {
    private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ActivatorStore));

    #if DEBUGGING_LOCKS
    private readonly IReaderWriterLock _lock = ReaderWriterLockFactory.CreateLock("ActivatorStore");
    #else
    private readonly ReaderWriterLock _lock = new ReaderWriterLock();
    #endif
    private readonly Dictionary<ServiceEntry, IActivator> _cache = new Dictionary<ServiceEntry, IActivator>();

    public IActivator ResolveActivator(ServiceEntry entry)
    {
      using (RWLock.AsReader(_lock))
      {
        return _cache[entry];
      }
    }

    public void AddActivator(ServiceEntry entry, IActivator activator)
    {
      using (RWLock.AsWriter(_lock))
      {
        if (_cache.ContainsKey(entry))
        {
          throw new ServiceContainerException("Multiple activators for one entry!");
        }
        _log.Info("Adding: " + entry + " " + activator);
        _cache[entry] = activator;
      }
    }

    public bool HasActivator(ServiceEntry entry)
    {
      using (RWLock.AsReader(_lock))
      {
        return _cache.ContainsKey(entry);
      }
    }
  }
}