using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastUtility2d
{
    private Camera _camera;
    private GraphicRaycaster _graphicRaycaster;
    private List<RaycastResult> _results = new List<RaycastResult>();
    private EventSystem _eventSystem;
    private PointerEventData _pointerEventData;

    public RaycastUtility2d(Camera camera, GraphicRaycaster graphicRaycaster, EventSystem eventSystem)
    {
        _camera = camera;
        _graphicRaycaster = graphicRaycaster;
        _eventSystem = eventSystem;

        _pointerEventData = new PointerEventData(_eventSystem);
    }
    
    public List<GameObject> RaycastAllPhysicsAtPosition(Vector3 screenPosition)
    {
        Vector3 worldPosition = _camera.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 1;

        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPosition, Vector2.zero);

        var hitObjects = new List<GameObject>();

        foreach (RaycastHit2D hit in hits)
        {
            hitObjects.Add(hit.collider.gameObject);
        }

        return hitObjects;
    }

    public List<GameObject> RaycastAllUIAtPosition(Vector3 screenPosition)
    {
        _pointerEventData.position = screenPosition;

        _results.Clear();

        _graphicRaycaster.Raycast(_pointerEventData, _results);

        List<GameObject> hitUIObjects = new List<GameObject>();
        foreach (RaycastResult result in _results)
        {
            hitUIObjects.Add(result.gameObject);
        }

        return hitUIObjects;
    }
}