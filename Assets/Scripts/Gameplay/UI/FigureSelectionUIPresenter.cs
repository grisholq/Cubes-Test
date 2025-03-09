using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class FigureSelectionUIPresenter : IInitializable
{
    private readonly FiguresSelectionUIModel _model;
    private readonly FiguresSelectionUIView _view;
    
    [Inject]
    public FigureSelectionUIPresenter(FiguresSelectionUIModel model, FiguresSelectionUIView view)
    {
        _model = model;
        _view = view;
        
        _view.Initialize(this);
    }   
    
    public void Initialize()
    {
        _model.OnFiguresAdded += OnFiguresAdded; 
    }
    
    private void OnFiguresAdded(IEnumerable<FigureData> figures)
    {
        _view.AddFiguresButtons(figures);
    }

    public void CancelScrolling()
    {
        _view.CancelScrolling();
    }    
}