using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class GetDataFromJson : MonoBehaviour
{
    private RootObject rootObject;
    public bool isDone = false;

    private void Awake()
    {
        rootObject = new RootObject();
        GetData(); 
    }

    public async void GetData()
    {
        var url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
        using var www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Content-Type", "application/json");
        var operation = www.SendWebRequest();
        
        while (!operation.isDone)
            await Task.Yield();

        if (www.result == UnityWebRequest.Result.Success)
        {  
            rootObject = JsonUtility.FromJson<RootObject>("{\"users\":" + www.downloadHandler.text+ "}");
            isDone = true;
        }
        else
            Debug.LogError($"Failed: {www.error}");
        
 
    }

    public RootObject GetRootObject()
    {
        return rootObject;
    }


}
