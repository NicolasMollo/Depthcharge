using UnityEngine;

namespace Depthcharge.GameManagement
{

    [DisallowMultipleComponent]
    public class GameSystemsRoot : MonoBehaviour
    {

        public static GameSystemsRoot Instance { get; private set; } = null;

        #region Systems



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