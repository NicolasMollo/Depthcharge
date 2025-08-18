using UnityEngine;

namespace Depthcharge.SceneManagement
{

    public class SceneContainer : MonoBehaviour
    {

        [SerializeField]
        private SceneConfiguration _configuration = null;
        public SceneConfiguration Configuration { get => _configuration; }

        private bool _isLoaded = false;
        public bool IsLoaded { get => _isLoaded; set => _isLoaded = value; }

    }

}