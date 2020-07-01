using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private bool useLerp = false;
    [SerializeField] private float timeAlignment = 0.75f;
    [SerializeField] private bool focusTarget = false;
    [SerializeField] private Vector3 offset = new Vector3(0, 15, 0);
    [SerializeField] private bool experimentCamera = false;
    [SerializeField] private Vector3 changePositionOffset = new Vector3(0, 15, 0);
    
    private Vector3 velocity = Vector3.zero;
    private Transform target;
    private bool cameraReachedFinalPosition;
    private bool rememberLerp;
    private bool rememberFocus;
    private float rememberTimeAlignment;
    private Quaternion rememberRotation;    

    // experimental feature on    
    private Rigidbody playerRB;
    private float changePosition, distance;
    private Vector3 playerMoveDir, playerPrevPos;
    //experimental feature off

    public void GetReady(Transform player)
    {
        SetPosition(player);
        SetTarget(player);
        playerRB = player.GetComponent<Rigidbody>();

        rememberLerp = useLerp;
        rememberFocus = focusTarget;
        rememberTimeAlignment = timeAlignment;
        rememberRotation = transform.rotation;

        //experimental feature on 
        if (GameObject.Find("Map/UntaggedGameObjects/CameraChangePosition"))
        {
            GameObject cameraChanger = GameObject.Find("Map/UntaggedGameObjects/CameraChangePosition");
            changePosition = cameraChanger.transform.position.z;
            cameraReachedFinalPosition = true;
        }
        //experimental feature off
    }

    public void SetPosition(Transform player)
    {
        this.transform.position = player.position;
    }

    public void SetTarget(Transform objectTransform)
    {
        target = objectTransform;
    }

    public void SetFocusOnTarget(bool status)
    {
        focusTarget = status;
    }

    public bool GetCameraReachedFinalPosition()
    {
        return cameraReachedFinalPosition;
    }

    public bool IsExperimentCamera()
    {
        return experimentCamera;
    }

    public Vector3 GetOffset()
    {
        return offset;
    }

    public void GetBackInPosition()
    {
        if(experimentCamera)
        {
            cameraReachedFinalPosition = true;    
        }
        else
        {
            cameraReachedFinalPosition = false;
        }        
    }

    public void ChangeCameraSettings(bool lerp, bool focus, float alignment)
    {
        useLerp = lerp;
        focusTarget = focus;
        timeAlignment = alignment;        
    }

    public void ResetCameraSettings()
    {
        useLerp = rememberLerp;
        focusTarget = rememberFocus;
        timeAlignment = rememberTimeAlignment;
    }
    
    // Coroutine needed here
    public void ResetCameraRotation()
    {
        transform.rotation = rememberRotation;
    }

    public void GoToTargetInstantly()
    {        
        Vector3 position = target.position + offset;
        // if(!useLerp)
        // {

        // }
        transform.position = position;
    }
    
    /*
    *  The camera hovers offseted over a given GameObject with a slight delay. The goal to center the targeted GameObject is always set.
    *  The "strictness" of the Camera to focus the GameObject can be determined by a conditional call of the LookAt function, if prefered over just a shorter value of the timeAlignment variable. 
    */
    void LateUpdate()
    {
        if (experimentCamera)
        {
            //experimantel feature on
            playerMoveDir = target.transform.position - playerPrevPos;
            if (playerMoveDir != Vector3.zero)
            {
                playerMoveDir.Normalize();
                transform.position = target.transform.position - playerMoveDir * distance;

                Vector3 height = transform.position;

                if (transform.position.z < changePosition)
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
                //experimental feature off
            }
        }
        else
        {
            if (!useLerp)
            {
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

            // Debug.Log(Vector3.Distance(transform.position, (target.position + offset) - new Vector3(0,1,0)));
            if (Vector3.Distance(transform.position, (target.position + offset) - new Vector3(0,1,0)) < 1.5f)
            {
                cameraReachedFinalPosition = true;
            }
        }
    }   
}