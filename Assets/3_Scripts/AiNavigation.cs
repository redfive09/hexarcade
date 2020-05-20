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

    void Awake()
    {
        ThisNavAgent = GetComponent<NavMeshAgent>();
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
        Debug.Log(Vector3.Distance(transform.position, loveObject.transform.position));
        //ThisNavAgent.SetDestination(sexualDesire.transform.position);    
    }

    public IEnumerator idleState()
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

    public IEnumerator travelState()
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
}
