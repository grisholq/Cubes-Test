using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FiguresCollectionByColor : IFiguresCollection
{
    public Sprite Icon;
    public Figure Prefab;
    public List<Color> Colors = new List<Color>();

    public IEnumerable<FigureData> GetFiguresCollection()
    {
        List<FigureData> figures = new List<FigureData>();
        
        foreach (var color in Colors)
        {
            figures.Add(new FigureData(color, Prefab, Icon));
        }

		return figures;
    }
}