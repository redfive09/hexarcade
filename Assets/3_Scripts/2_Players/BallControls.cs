using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControls : MonoBehaviour
{
    [SerializeField] float speed = 1000.0f;
    
    void FixedUpdate()
    {
        // Be ready for moving
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");    
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().AddForce (movement * (speed * Time.fixedDeltaTime));
    }

}