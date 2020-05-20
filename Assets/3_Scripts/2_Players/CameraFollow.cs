using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   /*
    * Tutorial by Brackeys: https://youtu.be/MFQhpwc6cKE
    */
   [SerializeField] private Transform target;
   [SerializeField] private  float smoothSpeed;
   [SerializeField] private  Vector3 offset;
   [SerializeField] private  Vector3 velocity;

   public void GetStarted(Transform player)
   {
      smoothSpeed = 0.125f;
      offset = new Vector3(0,10,0);
      velocity = new Vector3(0,0,0);
      target = player;
   }
   
   void FixedUpdate() //better for non-physics related actions
   {
      /*smoothDamp works better than lerp apparently https://docs.unity3d.com/ScriptReference/Vector3.SmoothDamp.html*/
      var position = transform.position;
      var smoothedPosition = Vector3.SmoothDamp(position, target.position + offset,  ref velocity, smoothSpeed);
      var smoothedPositionV2 = Vector3.Lerp(position, target.position + offset, Time.fixedDeltaTime);
      transform.position = smoothedPositionV2;
   }
}
