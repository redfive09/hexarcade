using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Code from this tutorial: https://www.youtube.com/watch?v=fsEkZLBeTJ8
 * Check out the Glide Game Tutorial on his channel, it's a great game using the Accelorometer Mechanic
 * here: https://www.youtube.com/watch?v=sZhhfOH0Q3Y
 */
public class AccelorometerInput : MonoBehaviour
{
    private Rigidbody rb;
    public bool isFlat = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tilt = Input.acceleration;
        if (isFlat)
        {
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
        }
        rb.AddForce(tilt);
        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.cyan);
    }
}