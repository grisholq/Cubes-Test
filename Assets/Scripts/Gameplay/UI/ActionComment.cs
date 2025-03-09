using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ActionComment : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _comment;

    private List<Tween> _commentAnimations = new List<Tween>(2);
    
    public void ShowComment(string comment)
    {
        Reset();
        
        _comment.text = comment;
        
        _commentAnimations.Add(_comment.DOFade(0,2).SetDelay(1f)); 
    }

    private void Reset()
    {
        _comment.alpha = 1;
        _comment.transform.localScale = Vector3.one;

        foreach (var animation in _commentAnimations)
        {
            animation.Kill();
        }
        
        _commentAnimations.Clear();
    }
}