using UnityEngine;

namespace Depthcharge.UI
{
    [DisallowMultipleComponent]
    public class WinUIController : MonoBehaviour
    {

        [SerializeField]
        private UI_PostGameMenu menu;

        public void ActivateMenu()
        {
            menu.gameObject.SetActive(false);
        }
        public void DeactivateMenu()
        {
            menu.gameObject.SetActive(true);
        }

    }

}