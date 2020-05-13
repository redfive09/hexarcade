using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

/*
 * Code from this tutorial: https://www.youtube.com/watch?v=fsEkZLBeTJ8
 * Check out the Glide Game Tutorial on his channel, it's a great game using the AccelorometerMovement Mechanic
 * here: https://www.youtube.com/watch?v=sZhhfOH0Q3Y
 */
public class AccelorometerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public bool isFlat = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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
