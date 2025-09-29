using Depthcharge.SceneManagement;
using Depthcharge.UI;
using UnityEngine;

namespace Depthcharge.GameManagement
{

    [DisallowMultipleComponent]
    public class StartSceneController : MonoBehaviour
    {

        private GameSystemsRoot systemsRoot = null;
        private GameStateManager stateManager = null;
        private UI_StartController UI = null;

        private void Start()
        {
            systemsRoot = GameSystemsRoot.Instance;
            stateManager = GameStateManager.Instance;
            systemsRoot.UISystem.SetCampaignUIActiveness(false);
            systemsRoot.UISystem.SetStartUIActiveness(true);
            UI = systemsRoot.UISystem.StartUI;
            UI.EnableInput();
            UI.ResetSelection();
            UI.SubscribeToSceneButtons(OnClickButton);
        }

        private void OnDestroy()
        {
            UI.UnsubscribeFromSceneButtons(OnClickButton);
        }

        private void OnClickButton(SceneConfiguration configuration)
        {
            systemsRoot.UISystem.LoseUI.SetButtonArg(
                Depthcharge.UI.EndGame.EndGameButtonType.Reload, 
                configuration);
            systemsRoot.SceneSystem.ChangeScene(configuration);
        }

    }

}