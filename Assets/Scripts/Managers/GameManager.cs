using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    IEnumerator Start()
    {
        GetDataFromJson json = GetComponent<GetDataFromJson>();
 
        while (!json.isDone) 
            yield return null;
        
        RootObject rootObject = json.GetRootObject();
        
        CreateStacks createStacks = GetComponent<CreateStacks>(); 
        createStacks.SetRootObject(rootObject);

        CameraController cameraController =FindObjectOfType<CameraController>();
        cameraController.SetTarget(createStacks.stacksGO[0].transform);
 
    }
  
}
