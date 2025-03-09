using System.Collections.Generic;
using System;
using UniRx;
using UnityEngine;

public class FiguresSelectionUIModel
{
    private readonly IGameConfig _config;
    public List<FigureData> FigureDatas { get; private set; } = new List<FigureData>();

    public event Action<IEnumerable<FigureData>> OnFiguresAdded;
    
    public FiguresSelectionUIModel(IGameConfig config)
    {
        _config = config;
    }

    public void AddFigures(IFiguresCollection figuresCollection)
    {
        var newFigures = figuresCollection.GetFiguresCollection();
        FigureDatas.AddRange(newFigures);
        OnFiguresAdded?.Invoke(newFigures);
    }
}