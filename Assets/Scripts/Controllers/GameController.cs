using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameScene currentScene;
    public StoryScene prevScene;
    public BottomBarController bottomBar;
    public BackgroundController backgroundController;
    public ChooseController chooseController;
    public Button menuButton;
    public Animator animator;

    public DataHolder data;
    public string menuScene;

    private State state = State.IDLE;

    private List<StoryScene> history = new List<StoryScene>();
    private enum State
    {
        IDLE,
        ANIMATE,
        CHOOSE
    }
    void Start()
    {
        if (menuButton != null)
        {
            menuButton.onClick.AddListener(ReturnToMenu);
        }
        if (SaveManager.IsGameSaved())
        {
            SaveData data = SaveManager.LoadGame();
            data.prevScenes.ForEach(scene =>
            {
                history.Add(this.data.scenes[scene] as StoryScene);
            });
            currentScene = history[history.Count - 1];
            history.RemoveAt(history.Count - 1);
            bottomBar.SetSentenceIndex(data.sentence - 1);
            history.Add((currentScene as StoryScene));
            bottomBar.PlayScene((currentScene as StoryScene));
            backgroundController.SetImage((currentScene as StoryScene).background);
            backgroundController.SwitchImage((currentScene as StoryScene).background);
        } else
        {
            history.Add((currentScene as StoryScene));
            bottomBar.PlayScene((currentScene as StoryScene));
            backgroundController.SetImage((currentScene as StoryScene).background);
        }
    }

    void Update()
    {
        float leftMargin = 0f;
        float rightMargin = 0f;
        float topMargin = 100f;
        float bottomMargin = 0f;

        Rect allowedArea = new Rect(
            leftMargin,                                 
            topMargin,                                 
            Screen.width - leftMargin - rightMargin,    
            Screen.height - topMargin - bottomMargin 
        );
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && IsMouseInAllowedArea(allowedArea))
        {
            if (state == State.IDLE && bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    if ((currentScene as StoryScene).nextScene != null)
                    {

                        PlayScene((currentScene as StoryScene).nextScene);
                    } else
                    {
                        ReturnToMenu();
                    }
                }
                else
                {
                    bottomBar.PlayNextSentence();
                }
            }
        }
    }

    private bool IsMouseInAllowedArea(Rect area)
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition.y = Screen.height - mousePosition.y;
        return area.Contains(mousePosition);
    }

    public void ReturnToMenu()
    {
        animator.SetTrigger("Transition");
        StartCoroutine(WaitForTransitionAndUnload());
    }

    private System.Collections.IEnumerator WaitForTransitionAndUnload()
    {
        yield return new WaitForSeconds(0.5f);
        List<int> historyIndicies = new List<int>();
        history.ForEach(scene =>
        {
            historyIndicies.Add(this.data.scenes.IndexOf(scene));
        });

        SaveData data = new SaveData
        {
            sentence = bottomBar.GetSentenceIndex(),
            prevScenes = historyIndicies
        };

        SaveManager.SaveGame(data);
        SceneManager.LoadScene(menuScene);
    }

    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(GameScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        if ((scene is StoryScene) && (prevScene == null || (scene as StoryScene).background != prevScene.background))
        {
            bottomBar.Hide();
        }
        if (scene is StoryScene)
        {
            StoryScene Storyscene = scene as StoryScene;
            history.Add(Storyscene);
            yield return new WaitForSeconds(1f);
            if ( prevScene == null || Storyscene.background != prevScene.background)
            {
                foreach (var spriteController in bottomBar.sprites.Values)
                {
                    if (spriteController != null)
                    {
                        spriteController.Hide();
                    }
                }
                bottomBar.sprites.Clear();
                backgroundController.SwitchImage(Storyscene.background);
                yield return new WaitForSeconds(1f);
            }
            if ((scene is StoryScene) && (prevScene == null || (scene as StoryScene).background != prevScene.background))
            {
                bottomBar.ClearText();
                bottomBar.animator.SetTrigger("Hidename");
                bottomBar.Show();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(0.25f);
            }
            prevScene = Storyscene;
            Globals.fromChoose = false;
            bottomBar.PlayScene(Storyscene);
            state = State.IDLE;
        } else if (scene is ChooseScene)
        {
            bottomBar.Hide();
            state = State.CHOOSE;
            chooseController.SetupChoose(scene as ChooseScene);
        }
    }

}
