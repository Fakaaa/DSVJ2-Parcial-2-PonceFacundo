using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] AsyncOperation sceneState;

    public void LoadAsyncScene(string nameScene)
    {
        StartCoroutine(AsynchronousLoad(nameScene));
    }
    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public IEnumerator AsynchronousLoad(string scene)
    {
        yield return null;

        sceneState = SceneManager.LoadSceneAsync(scene);
        sceneState.allowSceneActivation = false;

        while (!sceneState.isDone)
        {
            if (sceneState.progress >= 0.9f)
                sceneState.allowSceneActivation = true;

            yield return null;
        }
    }
    public AsyncOperation GetSceneState()
    {
        return sceneState;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}