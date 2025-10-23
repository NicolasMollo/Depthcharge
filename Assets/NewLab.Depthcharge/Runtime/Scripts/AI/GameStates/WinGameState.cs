using Depthcharge.SceneManagement;
using Depthcharge.UI;
using Depthcharge.UI.EndGame;

namespace Depthcharge.GameManagement.AI
{

    public class WinGameState : EndGameState
    {

        private bool wasBossLevel = false;

        protected override UI_EndGameController ConfigureUI()
        {
            return UISystem.WinUI;
        }

        protected override void OnClickReloadButton(SceneConfiguration configuration)
        {
            SceneConfiguration selectedConfiguration = configuration;
            if (!wasBossLevel && gameLogic.IsBossLevel)
            {
                selectedConfiguration = bossSceneConfiguration;
                wasBossLevel = true;
            }
            else
            {
                gameLogic.IncreaseCurrentLevelNumber();
                wasBossLevel = false;
            }
            base.OnClickReloadButton(selectedConfiguration);
        }

    }

}