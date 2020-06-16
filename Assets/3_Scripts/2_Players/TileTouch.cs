// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TileTouch : MonoBehaviour
// {
//     private GameObject hitObject;

//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.touchCount > 0)
//         {
//             Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
//             RaycastHit hitInfo;
//             if (Physics.Raycast(ray, out hitInfo))
//             {
//                 hitObject = hitInfo.collider.GetComponent<Collider>().gameObject;
//                 hitObject.GetComponent<Hexagon>().SetMarkedColor();

//             }
//         }
//         if (Input.GetMouseButtonDown(0))
//         {
//             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//             RaycastHit hitInfo;
//             if (Physics.Raycast(ray, out hitInfo))
//             {
//                 hitObject = hitInfo.collider.GetComponent<Collider>().gameObject;
//                 //Debug.Log(hitObject.transform.parent.gameObject.GetComponent<Hexagon>());
//                 hitObject.transform.parent.gameObject.GetComponent<Hexagon>().SetMarkedColor();
//             }

//         }
//     }
// }
