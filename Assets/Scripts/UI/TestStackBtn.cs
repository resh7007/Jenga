using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStackBtn : MonoBehaviour
{
   public static Action OnTestStackPressed;
   public void TestStackWasPressed()
   {
      OnTestStackPressed?.Invoke();
      
   }
}
