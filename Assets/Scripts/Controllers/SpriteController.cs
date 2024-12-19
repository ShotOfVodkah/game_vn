using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour
{
    private SpriteSwitcher switcher;
    private Animator animator;
    private RectTransform rect;

    private void Awake()
    {
        switcher = GetComponent<SpriteSwitcher>();
        animator = GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
        if (animator == null)
        {
            Debug.LogError("Animator is missing from SpriteController!");
        }
    }


    public void Setup(Sprite sprite)
    {
        switcher.SetImage(sprite);
    }

    public void Show(Vector2 coords)
    {
        Debug.Log("Showing sprite at position: " + coords);
        animator.SetTrigger("Show");
        rect.localPosition = coords;
    }

    public void Hide()
    {
        Debug.Log("Hiding sprite");
        animator.SetTrigger("Hide");
    }

    public void Move(Vector2 coords, float speed)
    {
        Debug.Log("Moving sprite at position: " + coords + " with speed: " + speed);
        StartCoroutine(MoveCoroutine(coords, speed));
    }

    private IEnumerator MoveCoroutine(Vector2 coords, float speed)
    {
        while (Vector2.Distance(rect.localPosition, coords) > 0.01f)
        {
            rect.localPosition = Vector2.MoveTowards(rect.localPosition, coords, speed * Time.deltaTime);
            yield return null;
        }
        rect.localPosition = coords;
    }


    public void SwitchSprite(Sprite sprite)
    {
        Debug.Log("Switching sprite");
        if (switcher.GetImage() != sprite)
        {
            switcher.SwitchImage(sprite);
        }
    }
}
