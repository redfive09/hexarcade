using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WriteTouchCoords : MonoBehaviour
{
    
    [SerializeField] private Text touchInfo = null;
    [SerializeField] private Text touchInfoRaw = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchCoords = touch.position; // .position changes if the touch moves on scree
            Vector3 touchCoordsRaw = touch.rawPosition; // .rawposition does not change but remains the initial touch position
            touchInfo.text = "___ x: " + touchCoords.x + " y: " + touchCoords.y;
            touchInfoRaw.text = "Raw: x: " + touchCoordsRaw.x + " y: " + touchCoordsRaw.y;
        }
    }
}

