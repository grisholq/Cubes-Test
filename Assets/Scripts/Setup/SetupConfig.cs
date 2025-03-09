using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSetupConfig", menuName = "MyAssets/GameSetupConfig")]
public class SetupConfig : ScriptableObject
{
    [Header("Game Config")]
    public GameConfigSource GameConfigSource;
    public string ConfigFilePath;
    public string ConfigUrl;
    public LocalGameConfig LocalGameConfig;

    [Space] public int GameSceneIndex = 1;
}