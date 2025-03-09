
public class InitializeState : IState
{
    private FiguresSelectionUIModel _figureSelectionUIModel;
    private IGameConfig _gameConfig;
    private IStateMachine _stateMachine;
    
    public InitializeState(IGameConfig gameConfig, FiguresSelectionUIModel figureSelectionUIModel)
    {
        _figureSelectionUIModel = figureSelectionUIModel;
        _gameConfig = gameConfig;
    }
    
    public void Enter()
    {
         _figureSelectionUIModel.AddFigures(_gameConfig.DefaultFiguresCollection);
         _stateMachine.SwitchState<IdleState>();
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
}