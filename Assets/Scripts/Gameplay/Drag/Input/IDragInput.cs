using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragInput
{
    public bool DragStarted { get; }
    public Vector2 DragPosition { get; }
    public bool DragEnded { get; }
}