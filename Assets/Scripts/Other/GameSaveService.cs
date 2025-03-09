using System;
using System.Collections.Generic;
using VContainer.Unity;

public class GameSaveService : IPostStartable
{
    private readonly IReadOnlyList<ISaveable> _saveables;

    public GameSaveService(IReadOnlyList<ISaveable> saveables)
    {
        _saveables = saveables;
    }

    public void Save()
    {
        foreach (var saveable in _saveables)
        {
            saveable.Save();
        }
    }

    public void Load()
    {
        foreach (var saveable in _saveables)
        {
            saveable.Load();
        }
    }

    public void PostStart()
    {
        Load();
    }
}
