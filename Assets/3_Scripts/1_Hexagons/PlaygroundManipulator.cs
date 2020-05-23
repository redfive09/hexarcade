using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaygroundManipulator : MonoBehaviour
{

    private Transform Playground = null;

    void Awake()
    {
        // Playground = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Playground.LookAt(Vector3.forward);
        // Playground.Rotate(90.0f * Input.acceleration.y, 0.0f, -90.0f * Input.acceleration.x, Space.World);
        //Playground.Rotate(90.0f * 0.1f, 90.0f * 0, 0.0f, Space.World);
    }
}
