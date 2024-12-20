using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public bool IsSwitched = false;
    public Image background1;
    public Image background2;
    public Animator animator;

    public void SwitchImage(Sprite sprite)
    {
        if (!IsSwitched)
        {
            background2.sprite = sprite;
            animator.SetTrigger("SwitchFirst");
        }
        else
        {
            background1.sprite = sprite;
            animator.SetTrigger("SwitchSecond");
        }
        IsSwitched = !IsSwitched;
    }

    public void SetImage(Sprite sprite)
    {
        Debug.Log("SettingImage");
        if (!IsSwitched)
        {
            background2.sprite = sprite;
        }
        else
        {
            background1.sprite = sprite;
        }
    }
}
