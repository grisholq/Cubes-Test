using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using VContainer.Unity;

public class Pool<T> : MonoBehaviour where T : Component
{    
    public  GameObject _prefab;
    public  Transform _parent;
    
    private readonly Stack<T> pool = new Stack<T>();
    
    public T Get(Transform parent = null)
    {
        T obj = null;
            
        if (pool.Count > 0)
        {
            obj = pool.Pop();
            obj.gameObject.SetActive(true); 
        }
        else
        {
            obj = CreateObject(true);
        }
        
        if(parent != null) obj.transform.SetParent(parent);
        
        return obj;
    }
    
    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(_parent);
        pool.Push(obj);
    }
    
    private T CreateObject(bool active = false)
    {
        T newObj = Instantiate();
        newObj.gameObject.SetActive(active);
        return newObj;
    }

    protected virtual T Instantiate()
    {
        return Object.Instantiate(_prefab, _parent).GetComponentInChildren<T>();
    }
}