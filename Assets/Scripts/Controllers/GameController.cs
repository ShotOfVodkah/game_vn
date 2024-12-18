using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottomBar;

    void Start()
    {
        bottomBar.PlayScene(currentScene);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (bottomBar.IsCompleted()) {
                bottomBar.PlayNextSentence();
            }
        }
    }
}
