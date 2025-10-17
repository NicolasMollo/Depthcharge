using Depthcharge.SceneManagement;
using Depthcharge.UI;
using Depthcharge.UI.EndGame;

namespace Depthcharge.GameManagement.AI
{

    public class WinGameState : EndGameState
    {

        protected override UI_EndGameController ConfigureUI()
        {
            return UISystem.WinUI;
        }

        protected override void OnClickReloadButton(SceneConfiguration configuration)
        {
            SceneConfiguration selectedConfiguration = configuration;
            if (gameLogic.IsBossLevel)
            {
                selectedConfiguration = bossSceneConfiguration;
            }
            if (!gameLogic.IsLastLevel && sceneSystem.CurrentScene.Configuration.SceneName == configuration.SceneName)
            {
                gameLogic.IncreaseCurrentLevelNumber();
            }
            base.OnClickReloadButton(selectedConfiguration);
        }

    }

}