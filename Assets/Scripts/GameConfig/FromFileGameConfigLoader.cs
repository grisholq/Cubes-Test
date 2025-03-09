using System.Threading.Tasks;

public static class FromFileGameConfigLoader
{
    public static async Task<GameConfig> LoadFromFile(string path)
    {
        //getting some job done
        await Task.Delay(1000);
        return new GameConfig();
    }
}
