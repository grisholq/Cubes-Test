using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class DragConstructionFigureState : IState
{
    private IDragInput _dragInput;
    private Clicker _clicker;
    private IStateMachine _stateMachine;
    private DragableFigure _dragableFigure;
    private Vector2 _previousPosition;
    private Camera _camera;
    private ActionComment _actionComment;

    public DragConstructionFigureState(IDragInput dragInput, Clicker clicker, DragableFigure dragableFigure, Camera camera, ActionComment actionComment)
    {
        _dragInput = dragInput;
        _clicker = clicker;
        _dragableFigure = dragableFigure;
        _camera = camera;
        _actionComment = actionComment;
    }

    public void Enter()
    {
    }

    public void Update()
    {
        HandleMovement();
        
        if (_dragInput.DragEnded)
        {
            HandleRelease();
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

    private void HandleRelease()
    {
        var clickData = _clicker.Click(_dragInput.DragPosition);

        if (clickData.HasInUIHierarchy(out FiguresSelectionUIView figuresSelectionUI))
        {
            var figure = _dragableFigure.Current;
            var construction = figure.Construction;
            
            if(figure.TakenFromConstruction) construction.RemoveFigure(figure);
            
            _dragableFigure.Destroy();
            _stateMachine.SwitchState<IdleState>();
            
            _actionComment.ShowComment(ActionsText.CubeChoiceCancel);
        }
        else if (clickData.HasInWorldHierarchy(out FiguresConstructionZone constructionZone))
        {
            var figure = _dragableFigure.Current;
            var construction = constructionZone.Construction;

            if (construction.Empty)
            {
                construction.FoundConstruction(figure);
                _dragableFigure.Deselect();
                _stateMachine.SwitchState<IdleState>();
                _actionComment.ShowComment(ActionsText.CubeConstructionCreated);
            }
            else
            {
                construction.ReturnFigure(figure);
                _dragableFigure.Deselect();
                _stateMachine.SwitchState<IdleState>();
                _actionComment.ShowComment(ActionsText.CubeReturnedToConstruction);
            }
        }
        else if (clickData.HasInWorldHierarchy(out FiguresTrashcan trashcan))
        {
            var figure = _dragableFigure.Current;
            var construction = figure.Construction;
            
            if(figure.TakenFromConstruction) construction.RemoveFigure(figure);
            
            trashcan.Utilize(_dragableFigure.Current);
            _dragableFigure.Deselect();
            _stateMachine.SwitchState<IdleState>();
            _actionComment.ShowComment(ActionsText.CubeThrownInTrashcan);
        }
        else if (clickData.HasInWorldHierarchy(out FiguresTrashcanZone trashcanZone))
        {
            var figure = _dragableFigure.Current;
            var construction = figure.Construction;
            if(figure.TakenFromConstruction) construction.RemoveFigure(figure);
            
            _stateMachine.SwitchState<CreatedFigureReturnState>();
            _actionComment.ShowComment(ActionsText.CubeChoiceCancel);
        }
    }
    
    private void HandleMovement()
    {
        if (_previousPosition == _dragInput.DragPosition) return;

        Vector3 newPosition = ConvertInputToWorldSpace(_dragInput.DragPosition);
        _dragableFigure.SetPosition(newPosition);

        _previousPosition = _dragInput.DragPosition;
    }

    private Vector3 ConvertInputToWorldSpace(Vector3 inputPosition)
    {
        return _camera.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, _camera.nearClipPlane));
    }
}