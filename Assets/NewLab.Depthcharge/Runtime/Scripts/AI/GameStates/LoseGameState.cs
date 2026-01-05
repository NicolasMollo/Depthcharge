using Depthcharge.SceneManagement;
using Depthcharge.UI;
using Depthcharge.UI.EndGame;

namespace Depthcharge.GameManagement.AI
{
    public class LoseGameState : EndGameState
    {

        protected override UI_EndGameController ConfigureUI()
        {
            return UISystem.LoseUI;
        }

        protected override void OnClickReloadButton(SceneConfiguration configuration)
        {
            SceneConfiguration selectedConfiguration = configuration;
            if (sceneSystem.CurrentScene.Configuration.SceneName != selectedConfiguration.SceneName)
            {
                selectedConfiguration = bossSceneConfiguration;
            }
            base.OnClickReloadButton(selectedConfiguration);
        }

    }
}