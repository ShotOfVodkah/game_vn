using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class Globals
{
    public static bool fromChoose = false; // Глобальная переменная
}


public class ChooseController : MonoBehaviour
{
    public ChooseLabelController label;
    public GameController gameController;
    private RectTransform rectTransform;
    private Animator animator;
    private float labelHeight = -1;

    void Start()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();

    }

    public void SetupChoose(ChooseScene scene)
    {
        Destroylabels();
        animator.SetTrigger("Show");

        for (int index = 0; index < scene.labels.Count; index++)
        {
            ChooseLabelController newLabel = Instantiate(label.gameObject, transform).GetComponent<ChooseLabelController>();
            if (labelHeight == -1)
            {
                labelHeight = newLabel.GetHeight();
            }
            newLabel.Setup(scene.labels[index], this, CalculateLabelPosition(index, scene.labels.Count));
        }

    }


    private float CalculateLabelPosition(int labelIndex, int labelCount)
    {
        // Вычисляем начальную точку для всех элементов
        float spacingFactor = 0.6f; // Фактор для расстояний между вариантами
        float totalHeight = labelHeight * spacingFactor * labelCount;
        float startOffset = -totalHeight / 2 + (labelHeight * spacingFactor) / 2;

        return startOffset + labelIndex * labelHeight * spacingFactor;
    }


    public void PerformChoose(StoryScene scene)
    {
        Globals.fromChoose = true;
        gameController.PlayScene(scene);
        animator.SetTrigger("Hide");
    }



    private void Destroylabels()
    {
        foreach(Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
    }
}
