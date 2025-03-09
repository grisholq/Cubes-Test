using DG.Tweening;
using VContainer;
using UnityEngine;

public class GameConfigHolder : IGameConfig
{
    public IFiguresCollection DefaultFiguresCollection => _config.DefaultFiguresCollection;
    public float FigureReturnAnimationTime => _config.FigureReturnAnimationTime;
    public Ease FigureReturnEase => _config.FigureReturnEase;
    public float FigurePlaceAnimationTime => _config.FigurePlaceAnimationTime;
    public Ease FigurePlaceEase => _config.FigurePlaceEase;
    public float TrashcanAnimationDuration => _config.TrashcanAnimationDuration;
    public Vector3 TrashcanObjectRotation => _config.TrashcanObjectRotation;

    private IGameConfig _config;

    public void SetGameConfig(IGameConfig config)
    {
        _config = config;
    }
}