using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TouchCoords : MonoBehaviour
{
    
    [SerializeField] private Text touchInfo = null;
    [SerializeField] private Text touchInfoRaw = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
        Touch touch = Input.GetTouch(0);
        Vector3 touchCoords = touch.position;
        Vector3 touchCoordsRaw = touch.rawPosition;
        touchInfo.text = "x: " + touchCoords.x + " y: " + touchCoords.y;
        touchInfoRaw.text = "x: " + touchCoordsRaw.x + " y: " + touchCoordsRaw.y;


            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            { 
                hitInfo.collider.GetComponent<Collider>().gameObject.SetActive(false);
            }
        }
    }
}

