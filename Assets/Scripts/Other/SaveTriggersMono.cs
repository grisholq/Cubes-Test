using UnityEngine;
using VContainer;

public class SaveTriggersMono : MonoBehaviour
{
    [Inject]
    private GameSaveService _gameSaveService;
    
    private void OnApplicationFocus(bool hasFocus)
    {
        if(hasFocus == false) _gameSaveService.Save();
    }

    private void OnApplicationQuit()
    {
        _gameSaveService.Save();
    }
}