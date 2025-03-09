using System.Threading.Tasks;

public static class FromURLGameConfigLoader
{
        public static async Task<GameConfig> LoadFromURL(string url)
        {
            //getting some job done
            await Task.Delay(1000);
            return new GameConfig();
        }
}
