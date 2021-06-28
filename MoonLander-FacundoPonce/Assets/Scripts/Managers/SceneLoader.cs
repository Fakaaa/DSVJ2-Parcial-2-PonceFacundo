using UnityEngine.SceneManagement;
using MonoBehaviourSingletonScript;

public class SceneLoader : MonoBehaviourSingleton<SceneLoader>
{
    void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}