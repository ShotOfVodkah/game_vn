using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public string loaderScene;
    public Button loadButton;
    public Animator transitionAnimator;

    public void Start()
    {
        loadButton.interactable = SaveManager.IsGameSaved();
    }

    public void NewGame()
    {
        SaveManager.ClearSavedGame();
        StartCoroutine(PlayTransitionAndLoad());
    }

    public void Load()
    {
        StartCoroutine(PlayTransitionAndLoad());
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator PlayTransitionAndLoad()
    {
        transitionAnimator.SetTrigger("Transition");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(loaderScene, LoadSceneMode.Additive);
    }
}
