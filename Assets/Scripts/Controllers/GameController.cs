using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
    public GameScene currentScene;
    public StoryScene prevScene;
    public BottomBarController bottomBar;
    public BackgroundController backgroundController;
    public ChooseController chooseController;

    private State state = State.IDLE;
    private enum State
    {
        IDLE,
        ANIMATE,
        CHOOSE
    }
    void Start()
    {
        bottomBar.PlayScene((currentScene as StoryScene));
        backgroundController.SetImage((currentScene as StoryScene).background);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (state == State.IDLE && bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    if ((currentScene as StoryScene).nextScene != null)
                    {

                        PlayScene((currentScene as StoryScene).nextScene);
                    }
                }
                else
                {
                    bottomBar.PlayNextSentence();
                }
            }
        }
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
