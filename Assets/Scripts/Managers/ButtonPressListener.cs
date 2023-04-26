using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressListener : MonoBehaviour
{
    [SerializeField] private CreateStacks _createStacks;
    
    private void OnEnable()
    {
        TestStackBtn.OnTestStackPressed+= HandleTestStackButtonPress;
       
    }
    private void OnDisable()
    {
        TestStackBtn.OnTestStackPressed-= HandleTestStackButtonPress;
       
    }
    void HandleTestStackButtonPress()
    {
        List<GameObject> glassBlocks = _createStacks.GetGlassBlocks();
        foreach (var glassBlock in glassBlocks)
        {
            Destroy(glassBlock);
        }
    }
}
