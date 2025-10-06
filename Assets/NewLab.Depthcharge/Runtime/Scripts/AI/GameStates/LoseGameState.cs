using Depthcharge.UI;
using Depthcharge.UI.EndGame;

namespace Depthcharge.GameManagement.AI
{
    public class LoseGameState : EndGameState
    {
        public override UI_EndGameController ConfigureUI()
        {
            return UISystem.LoseUI;
        }

    }
}