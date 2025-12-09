using Depthcharge.Audio;
using Depthcharge.SceneManagement;
using Depthcharge.UI;
using UnityEngine;

namespace Depthcharge.GameManagement
{

    [DisallowMultipleComponent]
    public class GameSystemsRoot : MonoBehaviour
    {

        #region Systems

        [Header("SYSTEMS")]
        [SerializeField]
        private UISystem _UISystem = null;
        [SerializeField]
        private AudioSystem _audioSystem = null;
        [SerializeField]
        private SceneManagementSystem _sceneSystem = null;

        #endregion

        public static GameSystemsRoot Instance { get; private set; } = null;

        public UISystem UISystem { get => _UISystem; }
        public AudioSystem AudioSystem { get => _audioSystem; }
        public SceneManagementSystem SceneSystem { get => _sceneSystem; }


        private void Awake()
        {
            SetSingleton();
            DontDestroyOnLoad(this.gameObject);
        }
        private void SetSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            _sceneSystem.SetUp();
            _UISystem.SetUp();
        }

    }

}