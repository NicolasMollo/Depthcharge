using Depthcharge.SceneManagement;
using Depthcharge.UI;
using UnityEngine;

namespace Depthcharge.GameManagement
{

    [DisallowMultipleComponent]
    public class StartSceneController : MonoBehaviour
    {

        private GameSystemsRoot systemsRoot = null;
        private StartUIController UI = null;
        private SceneManagementSystem sceneSystem = null;

        private void Start()
        {
            systemsRoot = GameSystemsRoot.Instance;
            systemsRoot.UISystem.SetGameUIActiveness(false);
            systemsRoot.UISystem.SetStartUIActiveness(true);
            UI = systemsRoot.UISystem.StartUI;
            sceneSystem = systemsRoot.SceneSystem;
            UI.SetUp();
            UI.SubscribeToSceneButtons(OnClickButton);
        }

        private void OnDestroy()
        {
            UI.UnsubscribeToSceneButtons(OnClickButton);
        }

        private void OnClickButton(SceneConfiguration sceneConfiguration)
        {
            sceneSystem.ChangeScene(sceneConfiguration);
        }

    }

}