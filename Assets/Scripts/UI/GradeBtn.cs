using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradeBtn : MonoBehaviour
{
    public int index;
    [SerializeField] private Text buttonText;
    private CameraController _cameraController;
    private CreateStacks _createStacks;
  private void OnEnable()
  {
      CreateStacks.OnStacksCreationFinished+=HandleStacksCreationFinished;
      _cameraController = FindObjectOfType<CameraController>();

  }

  private void OnDisable()
  {
        CreateStacks.OnStacksCreationFinished-=HandleStacksCreationFinished;

  }

  public void HandleStacksCreationFinished(CreateStacks createStacks)
  {
      _createStacks = createStacks;
      buttonText.text = createStacks.GetGradeLabelText(index);

  }

  public void GradeBtnWasPressed()
  {
      _cameraController.SetTarget(_createStacks.stacksGO[index].transform);
  }

}
