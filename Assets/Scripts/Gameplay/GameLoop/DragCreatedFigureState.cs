using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCreatedFigureState : IState
{
    private IStateMachine _stateMachine;
    private DragableFigure _dragableFigure;
    private IDragInput _dragInput;
    private Vector2 _previousPosition;
    private Camera _camera;
    private Clicker _clicker;
    private ActionComment _actionComment;

    public DragCreatedFigureState(DragableFigure dragableFigure, IDragInput dragInput, Camera camera, Clicker clicker, ActionComment actionComment)
    {
        _dragableFigure = dragableFigure;
        _dragInput = dragInput;
        _camera = camera;
        _clicker = clicker;
        _actionComment = actionComment;
    }

    public void Enter()
    {
        _previousPosition = _dragInput.DragPosition;
    }

    public void Update()
    {
        HandleMovement();
        if(_dragInput.DragEnded) HandleDragRelease();
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

    private void HandleDragRelease()
    {
        var clickData = _clicker.Click(_dragInput.DragPosition);

        if (clickData.HasInUIHierarchy(out FiguresSelectionUIView figureSelectionUI))
        {
            _dragableFigure.Destroy();
            _stateMachine.SwitchState<IdleState>();
            _actionComment.ShowComment(ActionsText.CubeChoiceCancel);
        }
        else if (clickData.HasInWorldHierarchy(out FiguresTrashcan trashcan))
        {
            trashcan.Utilize(_dragableFigure.Current);
            _dragableFigure.Deselect();
            _stateMachine.SwitchState<IdleState>();
            _actionComment.ShowComment(ActionsText.CubeThrownInTrashcan);
        }
        else if (clickData.HasInWorldHierarchy(out FiguresTrashcanZone trashcanZone))
        {
             _stateMachine.SwitchState<CreatedFigureReturnState>();
             _actionComment.ShowComment(ActionsText.CubeChoiceCancel);
        }
        else if (clickData.HasInWorldHierarchy(out FiguresConstructionZone constructionZone))
        {
            var construction = constructionZone.Construction;
            var figure = _dragableFigure.Current;

            if (construction.Empty)
            {
                construction.FoundConstruction(figure);
                _dragableFigure.Deselect();
                _stateMachine.SwitchState<IdleState>();
                _actionComment.ShowComment(ActionsText.CubeConstructionCreated);
            }
            else if (construction.IsFigureAboveTopFigure(figure) && construction.CanAddFigure)
            {
                construction.AddFigureOnTop(figure);
                _dragableFigure.Deselect();
                _stateMachine.SwitchState<IdleState>();
                _actionComment.ShowComment(ActionsText.CubeAddedToConstruction);
            }
            else
            {
                _stateMachine.SwitchState<CreatedFigureReturnState>();

                if (construction.IsFigureAboveTopFigure(figure) == false)
                    _actionComment.ShowComment(ActionsText.CubePlacementHint); 
                else _actionComment.ShowComment(ActionsText.CubeHeightRestriction);
            }
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