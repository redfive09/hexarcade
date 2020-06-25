using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionAudio : MonoBehaviour
{
    private static SceneTransitionAudio instance = null;
    public static SceneTransitionAudio Instance
    {
        get { return instance; }
    }
    
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;            
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
