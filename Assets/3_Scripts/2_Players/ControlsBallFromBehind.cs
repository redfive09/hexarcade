using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsBallFromBehind : MonoBehaviour
{
    [SerializeField] private bool ballCanTurn;
    [SerializeField] private float moveSpeedX = 10f;
    [SerializeField] private float moveSpeedZ = 1f;
    [SerializeField] private float rotationSpeed = 300f;

    private void FixedUpdate()
    {
        if(ballCanTurn)
        {
            float y = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeedZ;
            
            transform.Rotate(0, y, 0);
            transform.Translate(0, 0, z);
        }
        else
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeedX;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeedZ;
            transform.Translate(x, 0, z);
        }
    }
}
