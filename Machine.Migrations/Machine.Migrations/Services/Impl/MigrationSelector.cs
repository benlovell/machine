﻿using System;
using System.Collections.Generic;

namespace Machine.Migrations.Services.Impl
{
  public class MigrationSelector : IMigrationSelector
  {
    #region Member Data
    private readonly ISchemaStateManager _schemaStateManager;
    private readonly IMigrationFinder _migrationFinder;
    #endregion

    #region MigrationSelector()
    public MigrationSelector(ISchemaStateManager schemaStateManager, IMigrationFinder migrationFinder)
    {
      _schemaStateManager = schemaStateManager;
      _migrationFinder = migrationFinder;
    }
    #endregion

    #region IMigrationSelector Members
    public ICollection<MigrationReference> SelectMigrations()
    {
      short version = _schemaStateManager.GetVersion();
      List<MigrationReference> migrations = new List<MigrationReference>();
      foreach (MigrationReference migration in _migrationFinder.FindMigrations())
      {
        if (migration.Version > version)
        {
          migrations.Add(migration);
        }
      }
      return migrations;
    }
    #endregion
  }
}