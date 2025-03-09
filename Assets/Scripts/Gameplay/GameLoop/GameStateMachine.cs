using VContainer.Unity;

public class GameStateMachine : StateMachine, IInitializable
{
    private readonly InitializeState _initializeState;
    private readonly IdleState _idleState;
    private readonly DragCreatedFigureState _dragCreatedFigureState;
    private readonly CreatedFigureReturnState _createdFigureReturnState;
    private readonly DragConstructionFigureState _dragConstructionFigureState;
    
    public GameStateMachine(IdleState idleState, 
        DragCreatedFigureState dragCreatedFigureState, 
        InitializeState initializeState,
        CreatedFigureReturnState createdFigureReturnState,
        DragConstructionFigureState dragConstructionFigureState)
    {
        _idleState = idleState;
        _dragCreatedFigureState = dragCreatedFigureState;
        _initializeState = initializeState;
        _createdFigureReturnState = createdFigureReturnState;
        _dragConstructionFigureState = dragConstructionFigureState;
    }
    
    public void Initialize()
    {
        AddState(_initializeState);
        AddState(_idleState);
        AddState(_dragCreatedFigureState);
        AddState(_createdFigureReturnState);
        AddState(_dragConstructionFigureState);
        SwitchState<InitializeState>();
    }
}
