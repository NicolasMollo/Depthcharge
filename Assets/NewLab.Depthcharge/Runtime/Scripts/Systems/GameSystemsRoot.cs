using Depthcharge.SceneManagement;
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

        [SerializeField]
        private SceneManagementSystem _sceneSystem = null;
        public SceneManagementSystem SceneSystem { get => _sceneSystem; }

        #endregion

        private void Awake()
        {
            SetSingleton();
            DontDestroyOnLoad(this.gameObject);
            SetUp();
        }

        private void SetSingleton()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        private void SetUp()
        {
            _sceneSystem.SetUp();
        }

    }

}