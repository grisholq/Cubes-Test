using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class GameConfig : IGameConfig
{ 
    [SerializeField] private FiguresCollectionByColor figuresCollection;
    [SerializeField] private float _figureReturnAnimationTime;
    [SerializeField] private Ease _figureReturnEase;
    [SerializeField] private float _figurePlaceAnimationTime;
    [SerializeField] private Ease _figurePlaceEase;
    [SerializeField] private float _trashcanAnimationDuration;
    [SerializeField] private Vector3 _trashcanObjectRotation;
    
    public IFiguresCollection DefaultFiguresCollection => figuresCollection;
    public float FigureReturnAnimationTime => _figureReturnAnimationTime;
    public Ease FigureReturnEase => _figureReturnEase;
    public float FigurePlaceAnimationTime => _figurePlaceAnimationTime;
    public Ease FigurePlaceEase => _figurePlaceEase;
    public float TrashcanAnimationDuration => _trashcanAnimationDuration;
    public Vector3 TrashcanObjectRotation => _trashcanObjectRotation;
    
}