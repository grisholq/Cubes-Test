using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameGlobalScope : LifetimeScope
{
    [SerializeField] private SetupConfig _setupConfig;
    
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        
        builder.RegisterInstance(_setupConfig);
        
        builder.RegisterComponentInHierarchy<EntryPoint>();
        
        builder.Register<GameConfigHolder>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
    }
}