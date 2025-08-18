using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Depthcharge.SceneManagement
{

    [DisallowMultipleComponent]
    public class SceneManagementSystem : MonoBehaviour
    {

        private SceneContainer _currentScene = null;
        public SceneContainer CurrentScene { get => _currentScene; }
        [SerializeField]
        private List<SceneContainer> scenes = null;

        public void SetUp()
        {
            string startSceneName = SceneManager.GetActiveScene().name;
            _currentScene = GetSceneByName(startSceneName);
            _currentScene.IsLoaded = true;
        }

        public void ChangeScene(SceneConfiguration configuration)
        {
            SceneContainer sceneToLoad = GetSceneByName(configuration.SceneName);
            if (sceneToLoad == null)
            {
                Debug.LogError($"=== {this.gameObject.name} === Scene to load is null!");
                return;
            }
            _currentScene.IsLoaded = false;
            SceneManager.LoadScene(sceneToLoad.Configuration.SceneName, LoadSceneMode.Single);
            _currentScene = sceneToLoad;
            _currentScene.IsLoaded = true;
        }

        private SceneContainer GetSceneByName(string name)
        {
            SceneContainer selectedScene = null;
            foreach (SceneContainer scene in scenes)
            {
                if (scene.Configuration.SceneName == name)
                {
                    selectedScene = scene;
                    break;
                }
            }
            return selectedScene;
        }

    }

}