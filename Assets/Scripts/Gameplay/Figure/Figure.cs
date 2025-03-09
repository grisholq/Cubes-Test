using UnityEngine;
using VContainer;

public class Figure : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _halfHeight;
    [SerializeField] private float _halfWidth;

    [Inject] private Camera _camera;
    
    public Vector3 AnimationTargetPosition { get; set; }
    public bool IsAnimated { get; set; } = false;
    public float HalfHeight => _halfHeight;
    public float HalfWidth => _halfWidth;
    public bool IsOffScreen
    {
        get
        {
             var screenPosition = _camera.WorldToScreenPoint(transform.position);
             return screenPosition.y > Screen.height - 40 || screenPosition.y < 0 || screenPosition.x > Screen.width || screenPosition.x < 0;
        }
    }
    
    public bool TakenFromConstruction => Construction != null && Construction.IsFigureTaken(this);
    public FigureData FigureData { get; private set; }
    public FiguresConstruction Construction { get; set; }
    
    public void SetData(FigureData data)
    {
        FigureData = data;
        _spriteRenderer.color = data.Color;
    }
}