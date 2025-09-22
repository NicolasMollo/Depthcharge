using Depthcharge.SceneManagement;
using UnityEngine;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class LoseUIController : MonoBehaviour
    {

        [SerializeField]
        private UI_PostGameMenu menu = null;
        [SerializeField]
        private SceneConfiguration campaignSceneConfig = null;
        [SerializeField]
        private SceneConfiguration survivalSceneConfig = null;

        public void AddListeners(StartUIController startUI)
        {
            startUI.SubscribeToSceneButtons(OnClickSceneButtons);
        }
        public void RemoveListeners(StartUIController startUI)
        {
            startUI.UnsubscribeFromSceneButtons(OnClickSceneButtons);
        }

        private void OnClickSceneButtons(SceneConfiguration configuration)
        {
            if (configuration.SceneName == survivalSceneConfig.SceneName)
                menu.SetReloadButtonArg(survivalSceneConfig);
            else
                menu.SetReloadButtonArg(campaignSceneConfig);
        }

        public void ActivateMenu()
        {
            menu.gameObject.SetActive(true);
        }
        public void DeactivateMenu()
        {
            menu.gameObject.SetActive(false);
        }

    }

}