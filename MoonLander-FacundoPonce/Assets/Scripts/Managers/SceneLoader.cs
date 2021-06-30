using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
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