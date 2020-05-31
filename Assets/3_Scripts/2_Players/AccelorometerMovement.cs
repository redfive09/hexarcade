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
    [SerializeField]
    private float multiplier = 1.0f;
    [SerializeField]
    private ForceMode inputApplyment = ForceMode.Acceleration;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 tilt = Input.acceleration;
        tilt.z = 0.0f;
        if (isFlat)
        {
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
        }
        tilt *= multiplier;
        rb.AddForce(tilt, inputApplyment);
    }
}
