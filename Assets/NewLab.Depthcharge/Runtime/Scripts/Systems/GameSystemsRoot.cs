using Depthcharge.UI;
using UnityEngine;

namespace Depthcharge.GameManagement
{

    [DisallowMultipleComponent]
    public class GameSystemsRoot : MonoBehaviour
    {

        public static GameSystemsRoot Instance { get; private set; } = null;

        #region Systems

        [SerializeField]
        private UISystem _UISystem = null;
        public UISystem UISystem { get => _UISystem; }

        #endregion

        private void Awake()
        {
            SetSingleton();
            DontDestroyOnLoad(this.gameObject);
        }

        private void SetSingleton()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

    }

}