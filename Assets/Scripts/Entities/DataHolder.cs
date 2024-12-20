using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "NewDataHolder", menuName = "Data/New Data Holder")]
[System.Serializable]

public class DataHolder : ScriptableObject
{
    public List<GameScene> scenes;

}
