using System.Collections;
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
                Debug.LogError($"=== SceneManagementSystem.ChangeScene() === Scene to load is null!");
                return;
            }
            _currentScene.IsLoaded = false;
            SceneManager.LoadScene(sceneToLoad.Configuration.SceneName, LoadSceneMode.Single);
            _currentScene = sceneToLoad;
            StartCoroutine(AttendUntilSceneIsLoaded(_currentScene));
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
        private IEnumerator AttendUntilSceneIsLoaded(SceneContainer scene)
        {
            Scene sceneToLoad = SceneManager.GetSceneByName(scene.Configuration.SceneName);
            if (sceneToLoad == null)
            {
                Debug.LogError($"=== SceneManagementSystem.AttendUntilSceneIsLoaded() === Scene is null!");
                yield break;
            }
            yield return new WaitUntil(() => sceneToLoad.isLoaded);
            scene.IsLoaded = true;
        }

    }

}