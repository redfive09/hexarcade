using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * https://www.youtube.com/watch?v=JKoBWBXVvKY
 */
public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Music");
        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        
        DontDestroyOnLoad(this.gameObject); //technically this would be enough
        //however, since the object is never destroyed, once you reload the scene 
        // you get a duplicate. To prevent that from happenening we destroy the Music object
        // everytime that the is more than one 
    }
}
