using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMouseInput : IDragInput
{
    public bool DragStarted => Input.GetMouseButtonDown(0);
    public Vector2 DragPosition => Input.mousePosition;
    public bool DragEnded => Input.GetMouseButtonUp(0);
}
