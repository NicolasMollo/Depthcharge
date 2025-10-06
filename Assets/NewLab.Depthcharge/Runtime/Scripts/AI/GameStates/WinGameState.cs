using Depthcharge.SceneManagement;
using Depthcharge.UI;
using Depthcharge.UI.EndGame;

namespace Depthcharge.GameManagement.AI
{

    public class WinGameState : EndGameState
    {
        public override UI_EndGameController ConfigureUI()
        {
            return UISystem.WinUI;
        }

        protected override void OnClickReloadButton(SceneConfiguration configuration)
        {
            base.OnClickReloadButton(configuration); //  gameLogic.LoadGameTransitionsState = false;
            gameLogic.IncreaseCurrentLevelNumber();
        }

    }

}