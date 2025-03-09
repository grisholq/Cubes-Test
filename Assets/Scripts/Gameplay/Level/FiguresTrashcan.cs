using DG.Tweening;
using UnityEngine;
using VContainer;

public class FiguresTrashcan : MonoBehaviour
{
    [SerializeField] private Transform _centre;
    
    [Inject] private IGameConfig _gameConfig;
    
    public void Utilize(Figure figure)
    {
        figure.transform.DOMove(_centre.position, _gameConfig.TrashcanAnimationDuration)
            .OnComplete(() => Destroy(figure.gameObject));

        figure.transform.DOScale(Vector3.zero, _gameConfig.TrashcanAnimationDuration);
        
        figure.transform.DORotate(_gameConfig.TrashcanObjectRotation, _gameConfig.TrashcanAnimationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }
}
