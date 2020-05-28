using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiNavigation : MonoBehaviour
{
    [SerializeField] private GameObject loveObject = null;
    private NavMeshAgent ThisNavAgent = null;
    [SerializeField] private float distanceSatisfaction = 2.0f;
    [SerializeField] private float distanceEager = 6.0f;
    [SerializeField] private bool random = true;
    [SerializeField] private float randomFrequency = 2.0f;
    [SerializeField] private Vector3 center = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] private float radius = 5.0f;

    public enum STATES { IDLE = 0, TRAVEL = 1 };
    private STATES currentState = STATES.IDLE;
    //[SerializeField] private STATES startState = STATES.IDLE;

    public STATES CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            StopAllCoroutines();
            currentState = value;
            switch (currentState)
            {
                case STATES.IDLE:
                    StartCoroutine(idleState());
                    break;
                case STATES.TRAVEL:
                    StartCoroutine(travelState());
                    break;
                default:
                    break;
            }
        }
    }

    public GameObject SetGameObject
    {
        set
        {
            loveObject = value;
        }
    }

    public Vector3 Center
    {
        set
        {
            center = value;
        }
    }

    public float Radius
    {
        set
        {
            Radius = value;
        }
    }

    public float RandomFrequenzy
    {
        set
        {
            RandomFrequenzy = value;
            StopCoroutine("AlterDesiredPosition");
            InvokeRepeating("AlterDesiredPosition", 0.0f, randomFrequency);
        }
    }

    void Awake()
    {
        ThisNavAgent = GetComponent<NavMeshAgent>();
        if (random == true)
        {
            loveObject = new GameObject();
            InvokeRepeating("AlterDesiredPosition", 0.0f, randomFrequency);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Vector3.Distance(transform.position, loveObject.transform.position) >= distanceEager)
        {
            CurrentState = STATES.TRAVEL;
        }
        else
        {
            CurrentState = STATES.IDLE;
        }
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(Vector3.Distance(transform.position, loveObject.transform.position));
        //ThisNavAgent.SetDestination(sexualDesire.transform.position);    
    }

    IEnumerator idleState()
    {
        while (currentState == STATES.IDLE)
        {
            ThisNavAgent.SetDestination(this.transform.position);
            if (Vector3.Distance(transform.position, loveObject.transform.position) >= distanceEager)
            {
                CurrentState = STATES.TRAVEL;
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator travelState()
    {
        while (CurrentState == STATES.TRAVEL)
        {
            ThisNavAgent.SetDestination(loveObject.transform.position);
            if (Vector3.Distance(transform.position, loveObject.transform.position) <= distanceSatisfaction)
            {
                CurrentState = STATES.IDLE;
                yield break;
            }
            yield return null;
        }
    }

    void AlterDesiredPosition()
    {
        //Vector3 desPos = center + new Vector3(Random.Range(-radius, radius), 0.0f, Random.Range(-radius, radius));
        //Debug.Log(desPos);
        loveObject.transform.position = center + new Vector3(Random.Range(-radius, radius), 0.0f, Random.Range(-radius, radius)); //desPos;
    }
}
