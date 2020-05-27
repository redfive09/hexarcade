using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManipulation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                hitInfo.collider.GetComponent<Collider>().gameObject.SetActive(false);
            }
        }
    }
}
