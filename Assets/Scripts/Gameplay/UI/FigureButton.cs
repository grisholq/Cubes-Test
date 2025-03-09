using UnityEngine;
using UnityEngine.UI;

public class FigureButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    public FigureData FigureData { get; private set; }
    
    public void SetFigureData(FigureData figureData)
    {
        FigureData = figureData;
        _image.sprite = figureData.Icon;
        _image.color = FigureData.Color;
    }
}