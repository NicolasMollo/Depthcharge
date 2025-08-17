using UnityEngine;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class UISystem : MonoBehaviour
    {

        [SerializeField]
        private GameUIController _gameUI = null;
        public GameUIController GameUI { get => _gameUI; }

        public void SetGameUIActiveness(bool activeness)
        {
            _gameUI.gameObject.SetActive(activeness);
        }

    }

}