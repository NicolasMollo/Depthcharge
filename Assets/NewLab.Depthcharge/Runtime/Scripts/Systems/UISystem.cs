using UnityEngine;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class UISystem : MonoBehaviour
    {

        [SerializeField]
        private StartUIController _startUI = null;
        public StartUIController StartUI { get => _startUI; }

        [SerializeField]
        private GameUIController _gameUI = null;
        public GameUIController GameUI { get => _gameUI; }

        public void SetStartUIActiveness(bool activeness)
        {
            _startUI.gameObject.SetActive(activeness);
        }

        public void SetGameUIActiveness(bool activeness)
        {
            _gameUI.gameObject.SetActive(activeness);
        }

    }

}