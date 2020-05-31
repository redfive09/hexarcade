using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   /*
    * Tutorial by Brackeys: https://youtu.be/MFQhpwc6cKE
    */

   [SerializeField] private bool stickBehindPlayer = false;
   [SerializeField] private  float smoothSpeed = 0.125f;
   [SerializeField] private  Vector3 offset = new Vector3(0, 15, 0);
   [SerializeField] private  Vector3 changePositionOffset = new Vector3(0, 15, 0);
   [SerializeField] private  Vector3 velocity = new Vector3(0, 0, 0);
   
   private Vector3 playerMoveDir, playerPrevPos;
   private float distance;
   private Transform target;
   private Rigidbody playerRB;
   private float changePosition;
      

   public void SetPosition(Transform player)
   {
      // no Vector3, needs to be a Quaternion instead:
      // Vector3 standardRotation = new Vector3(90, 0, 0); 
      // this.transform.rotation.x = standardRotation;
      this.transform.position = player.position + offset;
   }

   public void SetTarget(Transform player)
   {
     target = player;


     playerRB = player.GetComponent<Rigidbody>();     
     if(GameObject.Find("Map/UntaggedGameObjects/CameraChangePosition"))
     {
         GameObject cameraChanger = GameObject.Find("Map/UntaggedGameObjects/CameraChangePosition");
         changePosition = cameraChanger.transform.position.z;
     }
   }
   
   void LateUpdate()
   {
      if(!stickBehindPlayer)
      {
         /*smoothDamp works better than lerp apparently https://docs.unity3d.com/ScriptReference/Vector3.SmoothDamp.html*/
         var position = transform.position;
         // var smoothedPosition = Vector3.SmoothDamp(position, target.position + offset,  ref velocity, smoothSpeed);
         var smoothedPositionV2 = Vector3.Lerp(position, target.position + offset, Time.fixedDeltaTime);
         transform.position = smoothedPositionV2;
      }
      else
      {
         playerMoveDir = target.transform.position - playerPrevPos;
         if (playerMoveDir != Vector3.zero)
         {
            playerMoveDir.Normalize();
            transform.position = target.transform.position - playerMoveDir * distance;

            Vector3 height = transform.position;

            if(transform.position.z < changePosition)
            {
               height += offset;
            }
            else
            {
               height += changePositionOffset;
            }
            
            
            transform.position = height; 

            transform.LookAt(target.transform.position);

            playerPrevPos = target.transform.position;


            // Vector3 rotationVector = playerRB.velocity;
            // Quaternion rotation = Quaternion.Euler(rotationVector.x, rotationVector.y, rotationVector.z);
            // transform.rotation = rotation;
         }
      }
   }
}
