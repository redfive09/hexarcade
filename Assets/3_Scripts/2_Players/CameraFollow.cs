using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private bool useLerp = false;
    [SerializeField]
    private float timeAlignment = 2.5f;
    [SerializeField]
    private bool focusTarget = false;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 15, 0);

    private Vector3 velocity = Vector3.zero;
    private Transform target;

    public void SetPosition(Transform player)
    {
        this.transform.position = player.position;
    }

    public void SetTraget(Transform player)
    {
        target = player;
    }

    /*
     *  The camera hovers offseted over a given GameObject with a slight delay. The goal to center the targeted GameObject is always set.
     *  The "strictness" of the Camera to focus the GameObject can be determined by a conditional call of the LookAt function, if prefered over just a shorter value of the timeAlignment variable. 
     */
    void LateUpdate()
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
    }
}

