using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonMovingTiles : MonoBehaviour
{

    // [SerializeField] private Vector3[] movingPositions;
    [SerializeField] private Vector3 movingTilePosA;
    [SerializeField] private Vector3 movingTilePosB;
    [SerializeField] private float speedOfMovingTiles = 2f;
    [SerializeField] private float startingDelay;
    [SerializeField] private float waitBeforeTurningBack;

    // [SerializeField] private bool isRoundTrip;
    [SerializeField] private bool startMovingBeforeGameStarted;
    [SerializeField] private bool liftFinishMoving = true;
    [SerializeField] private bool isLift;
    [SerializeField] private int needsNumberOfPlayersForLifting = 1;
    

    private Hexagon thisHexagon;
    private HashSet<Ball> balls = new HashSet<Ball>();
    private bool startedMoving = false;
    private float leftWaitingTime;
    private bool hexagonHasToWait = false;
    private float[] distances = new float[3];
    private float[] speedRegulator = new float[3];


    void Start()
    {
        thisHexagon = this.transform.GetComponentInParent<Hexagon>();
        SetupMovingSpeedRegulator(transform.position, movingTilePosB);
        WaitingCheck(startingDelay);
    }
   

    // Well, at the end not used, but thx anyway to -> https://low-scope.com/unity-quick-the-most-common-ways-to-move-a-object
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

                    float x = GetSign(movingTilePosB.x-transform.position.x) * speedOfMovingTiles * speedRegulator[0] * Time.deltaTime + transform.position.x;
                    float y = GetSign(movingTilePosB.y-transform.position.y) * speedOfMovingTiles * speedRegulator[1] * Time.deltaTime + transform.position.y;
                    float z = GetSign(movingTilePosB.z-transform.position.z) * speedOfMovingTiles * speedRegulator[2] * Time.deltaTime + transform.position.z;

                    transform.position = new Vector3(x, y, z); // Moves the object to target position
                    
                    if (Vector3.Distance(transform.position, movingTilePosB) <= 0.0001f) // Flip the points once it has reached the target
                    {
                        startedMoving = false;
                        var b = movingTilePosB;
                        var a = movingTilePosA;
                        movingTilePosA = b;
                        movingTilePosB = a;
                        SetupMovingSpeedRegulator(transform.position, movingTilePosB);

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
        int biggest = -1;
        distances[0] = GetPositiveNumber(position1.x - position2.x);
        distances[1] = GetPositiveNumber(position1.y - position2.y);
        distances[2] = GetPositiveNumber(position1.z - position2.z);

        for(int i = 0; i < distances.Length; i++)
        {
            if(distances[i] >= distances[(i+1) % distances.Length])
            {                
                biggest = i;                
            }
        }

        for(int i = 0; i < speedRegulator.Length; i++)
        {
            speedRegulator[i] = distances[i] / distances[biggest];
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
    

    public void CopyCurrentPositionToA()
    {
        movingTilePosA = transform.position;
    }

    public void CopyCurrentPositionToB()
    {
        movingTilePosB = transform.position;
    }
    
    public void GoToA()
    {        
        transform.position = movingTilePosA;
    }

    public void GoToB()
    {
        transform.position = movingTilePosB;
    }

    public void SaveCurrentPosition()
    {
        savedCurrentPosition = transform.position;
    }

    public void GoBackToSavedPosition()
    {
        transform.position = savedCurrentPosition;
    }

    // Not working yet
    // public void ResetTransformToParent()
    // {
    //     transform.position = transform.parent.transform.position;
    // }

}