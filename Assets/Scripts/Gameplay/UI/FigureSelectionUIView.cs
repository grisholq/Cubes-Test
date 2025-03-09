using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.UI;
using UnityEngine.UIElements;
using VContainer;

public class FiguresSelectionUIView : MonoBehaviour
{
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private FigureButtonsPool _figureButtonsPool;
    [SerializeField] private ScrollRect _scrollRect;
    
    private FigureSelectionUIPresenter _presenter;
    
    public void Initialize(FigureSelectionUIPresenter presenter)
    {
        _presenter = presenter;
    }

    public void AddFiguresButtons(IEnumerable<FigureData> figureDatas)
    {
        foreach (var figureData in figureDatas)
        {
            var button = _figureButtonsPool.Get(_buttonsParent);
            button.SetFigureData(figureData);
        }
    }

    public FigureButton FindFigureButton(FigureData figureData)
    {
        var buttons = _buttonsParent.GetComponentsInChildren<FigureButton>();

        foreach (var button in buttons)
        {
            if(button.FigureData == figureData) return button;
        }
        
        return null;
    }
    
    public void CancelScrolling()
    {
        StartCoroutine(CancelScrollingCoroutine());
    }

    private IEnumerator CancelScrollingCoroutine()
    {
        _scrollRect.enabled = false;
        yield return null;
        _scrollRect.enabled = true;
    }
}