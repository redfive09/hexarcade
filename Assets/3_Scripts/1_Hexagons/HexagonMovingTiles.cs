using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonMovingTiles : MonoBehaviour
{
    [SerializeField] private Vector3[] movingPositions;
    [SerializeField] private float speedOfMovingTiles = 1;
    [SerializeField] private float startingDelay;
    [SerializeField] private float waitBeforeTurningBack;

    // [SerializeField] private bool isRoundTrip;
    [SerializeField] private bool startMovingBeforeGameStarted;
    [SerializeField] private bool liftFinishMoving = true;
    [SerializeField] private bool isLift;
    [SerializeField] private int needsNumberOfPlayersForLifting = 1;
    [SerializeField] private int elementChangePosition;
    

    private Hexagon thisHexagon;
    private HashSet<Ball> balls = new HashSet<Ball>();
    private Vector3 destinationCoordinates;
    private int currentDestination = 1; // index counter of movingPositions
    private int biggestVectorComponent = -1;
    private float distanceTolerance;
    private bool startedMoving = false;
    private float leftWaitingTime;    
    private bool hexagonHasToWait = false;
    private float[] distances = new float[3];
    private float[] speedRegulator = new float[3];


    void Start()
    {
        thisHexagon = this.transform.GetComponentInParent<Hexagon>();
        destinationCoordinates = movingPositions[currentDestination];
        distanceTolerance = speedOfMovingTiles * 0.01f; // constant is a result of many tests
        SetupMovingSpeedRegulator(transform.position, destinationCoordinates);
        WaitingCheck(startingDelay);
    }
   

    // Well, at the end not used at all, but thx anyway to -> https://low-scope.com/unity-quick-the-most-common-ways-to-move-a-object
    private void FixedUpdate()
    {        
        if(!Game.isPaused && (Game.hasStarted || startMovingBeforeGameStarted))
        {
            if(!isLift || balls.Count >= needsNumberOfPlayersForLifting || (liftFinishMoving && startedMoving))
            {                
                if(hexagonHasToWait && leftWaitingTime > 0)
                {
                    leftWaitingTime -= Time.fixedDeltaTime;
                }
                else
                {
                    startedMoving = true;
                    Vector3 calcNewPosition = new Vector3();

                    for(int i = 0; i < 3; i++)
                    {
                        calcNewPosition[i] = GetSign(destinationCoordinates[i] - transform.position[i]) * speedOfMovingTiles * speedRegulator[i] * Time.deltaTime + transform.position[i];
                    }

                    // Moves the object to target position
                    transform.position = calcNewPosition;


                    // Setup the next destination
                    float difference = GetPositiveNumber(destinationCoordinates[biggestVectorComponent] - transform.position[biggestVectorComponent]);
                    if (difference < distanceTolerance) 
                    {
                        currentDestination++;
                        if(movingPositions.Length <= currentDestination /* + 1 */)
                        {                            
                            currentDestination = 0;                         
                        }

                        destinationCoordinates = movingPositions[currentDestination];
                            
                        if(currentDestination == 1) // when returned to the starting position, lift will wait there if set to finish moving
                        {
                            startedMoving = false;
                        }

                        SetupMovingSpeedRegulator(transform.position, destinationCoordinates);
                        WaitingCheck(waitBeforeTurningBack);                    
                    }
                }
            }
        }
    }

    private void WaitingCheck(float waitingTime)
    {
        if(waitingTime > 0)
        {
            hexagonHasToWait = true;
            leftWaitingTime = waitingTime;
        }
        else
        {
            hexagonHasToWait = false;
            leftWaitingTime = 0;
        }
    }
    
    private float GetSign(float number)
    {
        if(number < 0)
        {
            return -1f;
        }
        else
        {
            return 1f;
        }
    }

    private void SetupMovingSpeedRegulator(Vector3 position1, Vector3 position2)
    {

        // Get all the distances between each component (e. g.: x - x, y - y, z - z)
        for(int i = 0; i < 3; i++)
        {
            distances[i] = GetPositiveNumber(position1[i] - position2[i]);
        }
        
        // Remember the component with the highest distance and save it
        float tempHighestDistance = -1;
        for(int i = 0; i < 3; i++)
        {
            int nextPoint = (i+1) % distances.Length;
            if(distances[i] >= distances[nextPoint] && distances[i] > tempHighestDistance)
            {                
                biggestVectorComponent = i;
                tempHighestDistance = distances[i];                
            }
        }

        // Vector components with smaller distances have to be translated proportionally less
        for(int i = 0; i < 3; i++)
        {
            speedRegulator[i] = distances[i] / distances[biggestVectorComponent];
        }
    }

    private float GetPositiveNumber(float number)
    {
        if(number < 0)
        {
            return (-1) * number;
        }
        return number;
    }


    public void MovingTileTouched(HashSet<Ball> balls)
    {
        this.balls = balls;
    }

    public void MovingTileLeft(HashSet<Ball> balls)
    {
        this.balls = balls;
    }


    /* ------------------------------ METHODS FOR EDITOR MODE ------------------------------  */

    // Field, which is just used in editor mode
    private Vector3 savedCurrentPosition = new Vector3();
    private int countUsedPositions = 0;
    private int movingPosition = 0;

    private void checkUsedPositions()
    {
        if(movingPositions == null)
        {
            movingPositions = new Vector3[1];
        }

        countUsedPositions = 0;

        for(int i = 0; i < movingPositions.Length; i++)
        {
            if(movingPositions[i].sqrMagnitude != 0)
            {                
                countUsedPositions++;
            }
        }
    }
    

    public void AddNewDestination()
    {
        checkUsedPositions();

        if(movingPositions.Length <= countUsedPositions)
        {
            Vector3[] tempArray = new Vector3[countUsedPositions + 1];

            for(int i = 0; i < countUsedPositions; i++)
            {
                tempArray[i] = movingPositions[i];
            }
        	movingPositions = tempArray;
            movingPositions[countUsedPositions] = transform.position;
        }

        movingPositions[countUsedPositions] = transform.position;
    }

    public void ElementGoToNextPosition()
    {
        if(boundaryCheckOkay())
        {
            int nextPosition = (elementChangePosition + 1) % movingPositions.Length;
            Vector3 tempValue = movingPositions[nextPosition];
            movingPositions[nextPosition] = movingPositions[elementChangePosition];
            movingPositions[elementChangePosition] = tempValue;
            elementChangePosition = nextPosition;
        }
    }

    public void GoToNextPosition()
    {
        transform.position = movingPositions[movingPosition];
        movingPosition = (movingPosition + 1) % movingPositions.Length;
    }

    private bool boundaryCheckOkay()
    {
        if(elementChangePosition >= movingPositions.Length)
        {
            Debug.Log("There are only " + movingPositions.Length + " elements in the array.");
            return false;
        }
        return true;
    }

    public void SaveCurrentPosition()
    {
        savedCurrentPosition = transform.position;
    }

    public void GoBackToSavedPosition()
    {
        transform.position = savedCurrentPosition;
    }
}