using Xunit;

namespace GameEngine.Tests
{
    [CollectionDefinition("GameState collection")]
    public class GameTestCollection: ICollectionFixture<GameStateFixture>
    {
    }
}
