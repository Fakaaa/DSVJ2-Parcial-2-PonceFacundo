using UnityEngine.SceneManagement;
using MonoBehaviourSingletonScript;
using System.Collections;
using UnityEngine;

public class SceneLoader : MonoBehaviourSingleton<SceneLoader>
{
    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public IEnumerator AsynchronousLoad(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
            yield return null;

        ao.allowSceneActivation = true;
    }

    public IEnumerator AsynchronousLoad2(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            if (ao.progress >= 0.9f)
                ao.allowSceneActivation = true;

            yield return null;
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}