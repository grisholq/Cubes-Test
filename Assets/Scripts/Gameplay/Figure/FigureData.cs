using System;
using UnityEngine;

[Serializable]
public class FigureData
{
    public Sprite Icon;
    public Color Color;
    public Figure Prefab;

    public FigureData(Color color, Figure prefab, Sprite icon)
    {
        this.Icon = icon;
        this.Color = color;
        this.Prefab = prefab;
    }
    
    public static bool operator ==(FigureData a,FigureData b)
    {
        return a.Color == b.Color && a.Prefab == b.Prefab;
    }   
    
    public static bool operator !=(FigureData a,FigureData b)
    {
        return a.Color != b.Color || a.Prefab != b.Prefab;
    }
}