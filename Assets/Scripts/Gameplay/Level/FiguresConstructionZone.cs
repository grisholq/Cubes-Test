using UnityEngine;
using UnityEngine.Serialization;

public class FiguresConstructionZone : MonoBehaviour
{ 
    [SerializeField] private FiguresConstruction construction;
    
    public FiguresConstruction Construction => construction; 
}