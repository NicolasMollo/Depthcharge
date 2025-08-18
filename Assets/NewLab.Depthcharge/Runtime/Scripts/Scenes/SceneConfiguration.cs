using UnityEngine;

namespace Depthcharge.SceneManagement
{

    [CreateAssetMenu(menuName = "Scriptable Objects/SceneManagement/SceneConfiguration")]
    public class SceneConfiguration : ScriptableObject
    {

        [SerializeField]
        private int _sceneID = 0;
        public int SceneID { get => _sceneID; }

        [SerializeField]
        private string _sceneName = string.Empty;
        public string SceneName { get => _sceneName; }

    }

}