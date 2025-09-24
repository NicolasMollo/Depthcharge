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
        private StartUIController UI = null;
        private SceneManagementSystem sceneSystem = null;

        private void Start()
        {
            systemsRoot = GameSystemsRoot.Instance;
            stateManager = GameStateManager.Instance;
            systemsRoot.UISystem.SetCampaignUIActiveness(false);
            systemsRoot.UISystem.SetStartUIActiveness(true);
            UI = systemsRoot.UISystem.StartUI;
            sceneSystem = systemsRoot.SceneSystem;
            UI.SetUp();
            UI.SubscribeToSceneButtons(OnClickButton);
            systemsRoot.UISystem.LoseUI.AddListeners(UI);
            // systemsRoot.UISystem.LoseUI.SetUp();
        }

        private void OnDestroy()
        {
            systemsRoot.UISystem.LoseUI.RemoveListeners(UI);
            UI.UnsubscribeFromSceneButtons(OnClickButton);
            UI.CleanUp();
        }

        private void OnClickButton(SceneConfiguration sceneConfiguration)
        {
            sceneSystem.ChangeScene(sceneConfiguration);
        }

    }

}