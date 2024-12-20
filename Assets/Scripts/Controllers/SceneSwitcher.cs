using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToUnload;
    public string loaderScene;
    public string sceneToLoad;
    public Animator animator;

    void Start()
    {
        UnloadStartScene();
    }

    private void UnloadStartScene()
    {
        SceneManager.sceneUnloaded += OnStartUnloaded;
        SceneManager.UnloadSceneAsync(sceneToUnload);
    }

    private void OnStartUnloaded(Scene scene)
    {
        SceneManager.sceneUnloaded -= OnStartUnloaded;
        SceneManager.sceneLoaded += OnEndLoaded;
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }

    private void OnEndLoaded(Scene scene, LoadSceneMode mode)
    {
        animator.SetTrigger("Transition");
        WaitForTransitionAndUnload();
    }
    private System.Collections.IEnumerator WaitForTransitionAndUnload()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.sceneLoaded -= OnEndLoaded;
        UnloadLoader();
    }

    private void UnloadLoader()
    {
        SceneManager.UnloadSceneAsync(loaderScene);
    }
}