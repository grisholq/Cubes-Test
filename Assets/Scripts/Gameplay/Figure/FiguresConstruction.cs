using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class FiguresConstruction : MonoBehaviour, ISaveable
{
    [SerializeField] private Transform _figuresParent;
    
    public bool CanAddFigure => TopFigure.IsOffScreen == false;
    public Figure TopFigure => _figuresStructure.Last.Value;
    public bool OnlyBase => _figuresStructure.Count == 1;
    public bool Empty => _figuresStructure.Count == 0;
    
    private Figure _basicFigure => _figuresStructure.First.Value;
    private LinkedList<Figure> _figuresStructure = new LinkedList<Figure>();
    private List<Figure> _takenFigures = new List<Figure>();
    private List<Tween> _currentRebuildAnimations = new List<Tween>();
    
    [Inject] private IGameConfig _config;
    [Inject] private IObjectResolver _objectResolver;
    
    public void AddFigureOnTop(Figure figure)
    {
        if(CanAddFigure == false) return;
        PlaceFigure(figure, TopFigure, Vector3.up);
        _figuresStructure.AddLast(figure);
        figure.Construction = this;
        figure.transform.SetParent(_figuresParent);
    }

    public void RemoveFigure(Figure figure)
    {
        figure.Construction = null;
        
        if (_figuresStructure.Contains(figure))
        {
            _figuresStructure.Remove(figure);
            _takenFigures.Remove(figure);
            return;
        }
        
        if (IsFigureBaseOrTop(figure))
        {
            _figuresStructure.Remove(figure);
            _takenFigures.Remove(figure);
            return;
        }
        
        var previous = _figuresStructure.Find(figure).Previous;
        _figuresStructure.Remove(figure);
        _takenFigures.Remove(figure);
        Rebuild(previous.Value);
    }
    
    public void TakeFigure(Figure figure)
    {           
        _takenFigures.Add(figure);
        
        if (IsFigureBaseOrTop(figure)) return;
        
        var previous = _figuresStructure.Find(figure).Previous;
        Rebuild(previous.Value);
    }

    public void ReturnFigure(Figure figure)
    {
     _takenFigures.Remove(figure);   
        
        if (figure == TopFigure)
        {
            var previous = _figuresStructure.Find(figure).Previous;
            PlaceFigure(figure, previous.Value, Vector3.up);
            return;
        }

        if (figure == _basicFigure)
        { 
            var next = _figuresStructure.Find(figure).Next;
            PlaceFigure(figure, next.Value, Vector3.down);
            return;
        }
        
        var prev = _figuresStructure.Find(figure).Previous;
        Rebuild(prev.Value);
    }

    public void FoundConstruction(Figure figure)
    {
        _figuresStructure.AddLast(figure);
        figure.Construction = this;
        figure.transform.SetParent(_figuresParent);
    }

    public bool IsFigureTaken(Figure figure)
    {
        return _figuresStructure.Contains(figure) && _takenFigures.Contains(figure);
    }

    public bool IsFigureAboveTopFigure(Figure figure)
    {
        return figure.transform.position.y >= TopFigure.transform.position.y;
    }
    
    public void Rebuild(Figure startFigure = null)
    {
        if(_figuresStructure.Count == 1) return;
        
        CancelCurrentRebuildAnimations();
        
        var figuresIterator =
            startFigure == null ? 
                _figuresStructure.First : 
                _figuresStructure.Find(startFigure);
        
        var placeOnIterator = figuresIterator;
        
        do
        {
            figuresIterator = figuresIterator.Next;
            
            if(_takenFigures.Contains(figuresIterator.Value)) continue;
            
            PlaceFigure(figuresIterator.Value, placeOnIterator.Value, Vector3.up); 
            
            placeOnIterator = figuresIterator;
        } 
        while (figuresIterator.Next != null);
    }

    private void PlaceFigure(Figure figure, Figure placedOnFigure, Vector3 placementAxis)
    {
        var basicPosition = placedOnFigure.IsAnimated ? placedOnFigure.AnimationTargetPosition : placedOnFigure.transform.position;
        
        var position = basicPosition 
                       + placementAxis * (placedOnFigure.HalfHeight + figure.HalfHeight)
                       + Vector3.right * GenerateRandomOffset(placedOnFigure.HalfWidth);
        
        figure.IsAnimated = true;
        figure.AnimationTargetPosition = position;
        
        var tween = figure.transform.DOMove(position, _config.FigurePlaceAnimationTime)
            .SetEase(_config.FigurePlaceEase)
            .OnComplete(() =>
            {
                figure.IsAnimated = false;
            });
        
        _currentRebuildAnimations.Add(tween);
    }
    
    private float GenerateRandomOffset(float halfWidht) => Random.Range(-halfWidht, halfWidht);

    private void CancelCurrentRebuildAnimations()
    {
        foreach (var animation in _currentRebuildAnimations)
        {
            animation.Kill();   
        }
        
        _currentRebuildAnimations.Clear();
    }

    private bool IsFigureBaseOrTop(Figure figure)
    {
        return figure == TopFigure || figure == _basicFigure;
    }

    public void Save()
    {
        List<FigureSaveData> figures = new List<FigureSaveData>();

        foreach (var figure in _figuresStructure)
        {
            var saveData = new FigureSaveData();
            saveData.FigureData = figure.FigureData;
            saveData.Position = figure.transform.position;
            figures.Add(saveData);
        }
        
        var constructionSaveData = new FiguresConstructionSaveData();
        constructionSaveData.FiguresData = figures;
        
        PlayerPrefs.SetString("Construction", JsonUtility.ToJson(constructionSaveData));
    }

    public void Load()
    {
        var data = JsonUtility.FromJson<FiguresConstructionSaveData>(PlayerPrefs.GetString("Construction"));

        foreach (var figure in data.FiguresData)
        {
            LoadFigure(figure);
        }
    }

    private void LoadFigure(FigureSaveData figure)
    { 
        var instance = _objectResolver.Instantiate(figure.FigureData.Prefab, _figuresParent);
        instance.transform.position = figure.Position;
        instance.SetData(figure.FigureData);
        instance.Construction = this;
        _figuresStructure.AddLast(instance);
    }
}
