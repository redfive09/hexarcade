/*using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
     // Tutorial by Brackeys: https://youtu.be/MFQhpwc6cKE
     

    [SerializeField] private bool stickBehindPlayer = false;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset = new Vector3(0, 15, 0);


    [SerializeField] private Vector3 velocity = new Vector3(0, 0, 0);

    private Vector3 playerMoveDir, playerPrevPos;
    private float distance;


    private Transform target;

    public void SetPosition(Transform player)
    {
        // no Vector3, needs to be a Quaternion instead:
        // Vector3 standardRotation = new Vector3(90, 0, 0); 
        // this.transform.rotation.x = standardRotation;
        this.transform.position = player.position;
    }

    public void SetTraget(Transform player)
    {
        target = player;
    }

    void LateUpdate()
    {
        if (!stickBehindPlayer)
        {
            //smoothDamp works better than lerp apparently https://docs.unity3d.com/ScriptReference/Vector3.SmoothDamp.html
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
                height += offset;
                transform.position = height;

                transform.LookAt(target.transform.position);

                playerPrevPos = target.transform.position;
            }
        }
    }
}*/

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   
    // Tutorial by Brackeys: https://youtu.be/MFQhpwc6cKE
    

[SerializeField]
    private bool useLerp = false;
    [SerializeField]
    private  float timeAlignment = 2.5f;
    [SerializeField]
    private bool focusTarget = false;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 15, 0);


   [SerializeField] private  Vector3 velocity = Vector3.zero;
   
   private Vector3 playerMoveDir, playerPrevPos;
   //private float distance;


   private Transform target;

    public void Start()
    {
        //if (useLerp == true && timeAlignment > 1.0f)
        //{
        //    timeAlignment = 1.0f;
        //}
    }
   public void SetPosition(Transform player)
   {
      // no Vector3, needs to be a Quaternion instead:
      // Vector3 standardRotation = new Vector3(90, 0, 0); 
      // this.transform.rotation.x = standardRotation;
      this.transform.position = player.position; 
   }

   public void SetTraget(Transform player)
   {
     target = player;
   }
   
   void LateUpdate()
   {
      if(!useLerp)
      {
         //smoothDamp works better than lerp apparently https://docs.unity3d.com/ScriptReference/Vector3.SmoothDamp.html
            //Vector3 positionCurrent = transform.position;
            // var smoothedPosition = Vector3.SmoothDamp(position, target.position + offset,  ref velocity, smoothSpeed);
            // var smoothedPositionV2 = Vector3.Lerp(position, target.position + offset, Time.fixedDeltaTime);
            //transform.position = smoothedPositionV2;

            
            transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, timeAlignment);
            
       }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, timeAlignment);  
        }
        if (focusTarget)
        {
            transform.LookAt(target, Vector3.forward);
        }
    }
}

