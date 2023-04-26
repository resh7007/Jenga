using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GradeLabels : MonoBehaviour
{
    public GameObject LabelPrefab;
    private void OnEnable()
    {
        CreateStacks.OnStacksCreationFinished+=HandleStacksCreationFinished;
    }

    private void OnDisable()
    {
        CreateStacks.OnStacksCreationFinished-=HandleStacksCreationFinished;

    }

    public void HandleStacksCreationFinished(CreateStacks createStacks)
    {
        for (int i = 0; i < 3; i++)
        {
             GameObject label=  Instantiate(LabelPrefab, createStacks.GetGradeLabelPosition(i), Quaternion.identity);
             label.GetComponent<TextMeshPro>().text = createStacks.GetGradeLabelText(i);
        }
    }
}
