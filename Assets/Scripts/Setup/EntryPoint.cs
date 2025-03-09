using UnityEngine;
using VContainer.Unity;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;

public class EntryPoint : MonoBehaviour
{
    [Inject] private  SetupConfig _setupConfig;
    [Inject] private readonly GameConfigHolder _gameConfigHolder;

    private async Task LoadAndSetGameConfig()
    {
        _gameConfigHolder.SetGameConfig(_setupConfig.LocalGameConfig.GameConfig);
        
        IGameConfig config = null;

        switch (_setupConfig.GameConfigSource)
        {
            case GameConfigSource.File:
                config = await FromFileGameConfigLoader.LoadFromFile(_setupConfig.ConfigFilePath);
                break;
            case GameConfigSource.Url:
                config = await FromURLGameConfigLoader.LoadFromURL(_setupConfig.ConfigUrl);
                break;
            case GameConfigSource.Local:
                config = _setupConfig.LocalGameConfig.GameConfig;
                break;
        }

        if (config != null)
        {
            _gameConfigHolder.SetGameConfig(config);
        }
    }

    public void Start()
    {
        LoadAndSetGameConfig();
        LoadGameScene();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(_setupConfig.GameSceneIndex, LoadSceneMode.Additive);
    }
}