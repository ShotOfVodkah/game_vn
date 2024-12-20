using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewChooseScene", menuName = "Data/New Choose Scene")]
[System.Serializable]

public class ChooseScene : GameScene
{
    public List<ChooseLabel> labels;
    [System.Serializable]

    public struct ChooseLabel
    {
        public string text;
        public StoryScene nextScene;
    }
}
