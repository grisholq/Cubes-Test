using UnityEngine;
using VContainer;
using VContainer.Unity;

public class FigureButtonsPool : Pool<FigureButton>
{
    private IObjectResolver _objectResolver;
    
    [Inject]
    private void Construct(IObjectResolver resolver)
    {
        _objectResolver = resolver;
    }
    
    protected override FigureButton Instantiate()
    {
        return _objectResolver.Instantiate(_prefab, _parent).GetComponent<FigureButton>();
    }
}