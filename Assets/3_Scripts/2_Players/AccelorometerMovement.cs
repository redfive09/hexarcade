using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class AccelorometerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float multiplier = 75.5f;
    [SerializeField]
    private ForceMode inputApplyment = ForceMode.Acceleration;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /*
     * 
     * Takes in the gravity induced acceleration from sensors hopply build in in every handy this script will ever run on. Proper fuction is guaranteet only having the screen facing up.
     * The read out values are offset to work as intuitive control on a game object viewed form above as it is the case with LookAt(<GameObject>, Verctor3.forward) and if then applied as force on it .
     * To further have the option scale the magnitude of "force" applied a additional multiplier is added to the calculation.
     */
    void Update()
    {
        Vector3 tilt = Input.acceleration;
        tilt.z = 0.0f;
        tilt = Quaternion.Euler(90, 0, 0) * tilt;
        tilt *= multiplier;
        rb.AddForce(tilt, inputApplyment);
    }
}
