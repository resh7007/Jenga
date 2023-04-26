using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetBtn : MonoBehaviour
{
    public void ResetWasPressed()
    {
        SceneManager.LoadScene(sceneBuildIndex:0);
    }
}
