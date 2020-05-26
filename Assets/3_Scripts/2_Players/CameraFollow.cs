using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   /*
    * Tutorial by Brackeys: https://youtu.be/MFQhpwc6cKE
    */
   [SerializeField] private  float smoothSpeed = 0.125f;
   [SerializeField] private  Vector3 offset = new Vector3(-5, 10, -10);
   [SerializeField] private  Vector3 velocity = new Vector3(0, 0, 0);

   private Transform target;

   public void GetStarted(Transform player)
   {
      this.transform.position = player.position;
      target = player;
   }
   
   void LateUpdate()
   {
      /*smoothDamp works better than lerp apparently https://docs.unity3d.com/ScriptReference/Vector3.SmoothDamp.html*/
      var position = transform.position;
      // var smoothedPosition = Vector3.SmoothDamp(position, target.position + offset,  ref velocity, smoothSpeed);
      var smoothedPositionV2 = Vector3.Lerp(position, target.position + offset, Time.fixedDeltaTime);
      transform.position = smoothedPositionV2;
   }
}
