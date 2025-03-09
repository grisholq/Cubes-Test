using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class GameSceneScope : LifetimeScope
{ 
    [SerializeField] private Camera _camera;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GraphicRaycaster _graphicRaycaster;
    [SerializeField] private FiguresConstruction _figuresConstruction;
    [SerializeField] private FiguresTrashcan _figureTrashcan;
    [SerializeField] private ActionComment _actionComment;
    [SerializeField] private SaveTriggersMono _saveTriggersMono;
    
    [SerializeField] private FiguresSelectionUIView _figuresSelectionUIView;
    [SerializeField] private FigureButtonsPool _figureButtonsPool;
    
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);

        builder.RegisterComponent(_camera);
        builder.RegisterComponent(_eventSystem);
        builder.RegisterComponent(_graphicRaycaster);
        builder.RegisterComponent(_figuresConstruction).AsImplementedInterfaces().AsSelf();
        builder.RegisterComponent(_figureTrashcan);
        builder.RegisterComponent(_actionComment);
        builder.RegisterComponent(_saveTriggersMono);
        
        builder.Register<FiguresSelectionUIModel>(Lifetime.Scoped);
        builder.Register<FigureSelectionUIPresenter>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
        builder.RegisterComponent(_figuresSelectionUIView);
        builder.RegisterComponent(_figureButtonsPool);
        
        builder.Register<GameStateMachine>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces(); 
        builder.Register<InitializeState>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();  
        builder.Register<IdleState>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces(); 
        builder.Register<DragCreatedFigureState>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces(); 
        builder.Register<CreatedFigureReturnState>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces(); 
        builder.Register<DragConstructionFigureState>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces(); 
        
        builder.Register<DragableFigure>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces(); 
        builder.Register<Clicker>(Lifetime.Scoped); 
        builder.Register<RaycastUtility2d>(Lifetime.Scoped); 
        builder.Register<DragMouseInput>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces(); 
        
        builder.Register<GameSaveService>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces(); 
    }
}