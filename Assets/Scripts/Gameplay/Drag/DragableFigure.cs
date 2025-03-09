using UnityEngine;

public class DragableFigure
{
    public Figure Current { get; set; }

    public void Hide()
    {
        if (Current != null)
        {
            Current.gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        if (Current != null)
        {
            Current.gameObject.SetActive(true);
        }
    }

    public void SetPosition(Vector3 position)
    {
        Current.transform.position = position;
    }

    public void Destroy()
    {
        GameObject.Destroy(Current.gameObject);
    }  
    
    public void Deselect()
    {
        Current = null;
    }
}