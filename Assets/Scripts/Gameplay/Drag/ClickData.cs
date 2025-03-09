using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickData
{
    public List<GameObject> WorldHierarchy => _worldHierarchy;
    public List<GameObject> UIHierarchy => _uiHierarchy;
     
    private List<GameObject> _worldHierarchy;
    private List<GameObject> _uiHierarchy;

    public ClickData(List<GameObject> worldHierarchy, List<GameObject> uiHierarchy)
    {
        _worldHierarchy = worldHierarchy;
        _uiHierarchy = uiHierarchy;
    }

    public bool HasInAllHierarchy<T>(out T component) where T : MonoBehaviour
    {
        if (HasInUIHierarchy(out component)) return true;
        else if (HasInWorldHierarchy(out component)) return true;
        
        return false;
    }    
    
    public bool HasInUIHierarchy<T>(out T component) where T : MonoBehaviour
    {
        component = null;
        
        foreach (var go in UIHierarchy)
        {
            if (go.TryGetComponent<T>(out T comp))
            {
                component = comp;
                return true;
            }
        }
        
        return false;
    }    
    
    public bool HasInWorldHierarchy<T>(out T component) where T : MonoBehaviour
    {
        component = null;
        
        foreach (var go in WorldHierarchy)
        {
            if (go.TryGetComponent<T>(out T comp))
            {
                component = comp;
                return true;
            }
        }
        
        return false;
    }   
}
