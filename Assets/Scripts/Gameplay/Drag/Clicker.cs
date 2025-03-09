using UnityEngine;

public class Clicker
{
    private RaycastUtility2d _raycastUtility;

    public Clicker(RaycastUtility2d raycastUtility)
    {
        _raycastUtility = raycastUtility;
    }
    
    public ClickData Click(Vector2 screenPosition)
    {
        var worldHierarchy = _raycastUtility.RaycastAllPhysicsAtPosition(screenPosition);
        var uiHierarchy = _raycastUtility.RaycastAllUIAtPosition(screenPosition);
        return new ClickData(worldHierarchy, uiHierarchy);
    }
}
