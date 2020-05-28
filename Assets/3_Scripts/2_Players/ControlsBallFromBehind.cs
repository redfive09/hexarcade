using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsBallFromBehind : MonoBehaviour
{
    [SerializeField] private bool ballCanTurn;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 300f;

    private void FixedUpdate()
    {
        if(ballCanTurn)
        {
            float y = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
            
            transform.Rotate(0, y, 0);
            transform.Translate(0, 0, z);
        }
        else
        {
            float x = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
            transform.Translate(x, 0, 0);
        }
        

    }
}
