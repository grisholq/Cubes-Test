using DG.Tweening;

public class CreatedFigureReturnState : IState
{
    private FiguresSelectionUIView _figuresSelectionUI;
    private IGameConfig _gameConfig;
    private IStateMachine _stateMachine;
    private DragableFigure _dragableFigure;

    public CreatedFigureReturnState(IGameConfig gameConfig, FiguresSelectionUIView figuresSelectionUI, DragableFigure dragableFigure)
    {
        _figuresSelectionUI = figuresSelectionUI;
        _gameConfig = gameConfig;
        _dragableFigure = dragableFigure;
    }

    public void Enter()
    {
        var button = _figuresSelectionUI.FindFigureButton(_dragableFigure.Current.FigureData);
        _dragableFigure.Current.transform
            .DOMove(button.transform.position, _gameConfig.FigureReturnAnimationTime)
            .SetEase(_gameConfig.FigureReturnEase)
            .OnComplete(OnFigureReturnAnimationComplete);
    }

    public void Update()
    {
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

    private void OnFigureReturnAnimationComplete()
    {
        _dragableFigure.Destroy();
        _stateMachine.SwitchState<IdleState>();
    }
}