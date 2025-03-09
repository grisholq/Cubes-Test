using DG.Tweening;
using UnityEngine;

public interface IGameConfig
{
    public IFiguresCollection DefaultFiguresCollection { get; }
    public float FigureReturnAnimationTime { get; }
    public Ease FigureReturnEase { get; }   
    public float FigurePlaceAnimationTime { get; }
    public Ease FigurePlaceEase { get; }
    public float TrashcanAnimationDuration { get; }
    public Vector3 TrashcanObjectRotation { get; }
}