using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour
{
    public bool IsSwitched = false;
    public Image image1;
    public Image image2;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Switchbackground(Sprite sprite)
    {
        if (!IsSwitched)
        {
            image2.sprite = sprite;
            animator.SetTrigger("SwitchFirst");
        }
        else
        {
            image1.sprite = sprite;
            animator.SetTrigger("SwitchSecond");
        }
        IsSwitched = !IsSwitched;
    }

    public void SwitchImage(Sprite sprite)
    {
        Debug.Log("Switching sprite. IsSwitched: " + IsSwitched);

        if (!IsSwitched)
        {
            Debug.Log("Switching to image2 with sprite: " + sprite.name);

            image2.sprite = sprite;
            image2.enabled = true;
            image1.enabled = false;

            Debug.Log("Triggering animation for SwitchFirst");
        }
        else
        {
            Debug.Log("Switching to image1 with sprite: " + sprite.name);

            image1.sprite = sprite;
            image1.enabled = true;
            image2.enabled = false;

            Debug.Log("Triggering animation for SwitchSecond");
        }

        Debug.Log("IsSwitched value after switch: " + IsSwitched);
        IsSwitched = !IsSwitched;
    }


    public void SetImage(Sprite sprite)
    {
        if (!IsSwitched)
        {
            image2.sprite = sprite;
            image1.enabled = false;
        }
        else
        {
            image1.sprite = sprite;
            image2.enabled = false;
        }
    }

    public Sprite GetImage()
    {
        if (!IsSwitched)
        {
            return image2.sprite;
        }
        else
        {
            return image1.sprite;
        }
    }

}
