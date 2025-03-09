using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class IdleState : IState
{
    private IDragInput _dragInput;
    private Clicker _clicker;
    private IStateMachine _stateMachine;
    private DragableFigure _dragableFigure;
    private IObjectResolver _objectResolver;
    private Camera _camera;
    private FigureSelectionUIPresenter _figureSelectionUIPresenter;
    private ActionComment _actionComment;

    public IdleState(IDragInput dragInput, 
        Clicker clicker, 
        DragableFigure dragableFigure,
        IObjectResolver objectResolver, 
        Camera camera, 
        FigureSelectionUIPresenter figureSelectionUIPresenter
        ,ActionComment actionComment)
    {
        _dragInput = dragInput;
        _clicker = clicker;
        _dragableFigure = dragableFigure;
        _objectResolver = objectResolver;
        _camera = camera;
        _figureSelectionUIPresenter = figureSelectionUIPresenter;
        _actionComment = actionComment;
    }

    public void Enter()
    {
    }

    public void Update()
    {
        if (_dragInput.DragStarted)
        {
            HandleClick();
        }
    }

    public void FixedUpdate()
    {
    }

    public void Exit()
    {
    }

    public void SetStateMachine(IStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void HandleClick()
    {
        var clickData = _clicker.Click(_dragInput.DragPosition);

        if (clickData.HasInUIHierarchy(out FigureButton figureButton))
        {
            FigureData figureData = figureButton.FigureData;
            _dragableFigure.Current = CreateFigure(figureData);
            PositionDragObjectAtFigureButton(figureButton);
            _stateMachine.SwitchState<DragCreatedFigureState>();
            _figureSelectionUIPresenter.CancelScrolling();
            _actionComment.ShowComment(ActionsText.CubeChoosen);
        }
        else if (clickData.HasInWorldHierarchy(out Figure figure))
        {
            var construction = figure.Construction;

            if (construction.OnlyBase)
            {
                construction.RemoveFigure(figure);
                _actionComment.ShowComment(ActionsText.ConstructionDestroyed);
            }
            else
            {
                construction.TakeFigure(figure);
                _actionComment.ShowComment(ActionsText.CubeTakenFromConstruction);
            }
            
            _dragableFigure.Current = figure;
            _stateMachine.SwitchState<DragConstructionFigureState>();
        }
    }

    private void PositionDragObjectAtFigureButton(FigureButton figureButton)
    {
        _dragableFigure.SetPosition(figureButton.transform.position);
    }

    private Figure CreateFigure(FigureData figureData)
    {
        Figure figure = _objectResolver.Instantiate(figureData.Prefab);
        figure.SetData(figureData);
        return figure;
    }
}