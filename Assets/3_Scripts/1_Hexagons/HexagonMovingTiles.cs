using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonMovingTiles : MonoBehaviour
{

    [SerializeField] private Vector3 movingTilePosA;
    [SerializeField] private Vector3 movingTilePosB;
    [SerializeField] private float speedOfMovingTiles;

    [SerializeField] private float startingDelay;
    [SerializeField] private float waitBeforeTurningBack;

    // [SerializeField] private bool isRoundTrip;

    [SerializeField] private bool isLift;
    [SerializeField] private int needsNumberOfPlayersForLifting;


    // private
    private Hexagon thisHexagon;
    private int currentPlayersOnTile = 0;


    // Setup standard values for editor mode
    public void Setup()
    {
        speedOfMovingTiles = 2f;
        // isRoundTrip = true;
        needsNumberOfPlayersForLifting = 1;
        isLift = false;
    }

    void Start()
    {
        thisHexagon = this.transform.GetComponentInParent<Hexagon>();
        StartCoroutine(PrepareMoving());
    }

    public void MovingTileTouched()
    {
        currentPlayersOnTile++;
        StartCoroutine(PrepareMoving());

    }

    public void MovingTileLeft()
    {
        currentPlayersOnTile--;
    }


    private bool ConditionsMet()
    {
        if(thisHexagon.IsMovingTile() && !isLift || 
            isLift && needsNumberOfPlayersForLifting <= currentPlayersOnTile)
        {
            return true;
        }
        return false;
    }


    private IEnumerator PrepareMoving()
    {
        while (ConditionsMet())
        {
            yield return new WaitForSeconds(startingDelay);
            yield return StartCoroutine(MoveObject(this.transform, movingTilePosA, movingTilePosB, speedOfMovingTiles));
            yield return new WaitForSeconds(waitBeforeTurningBack);
            // if(isRoundTrip)
            // {
                yield return StartCoroutine(MoveObject(this.transform, movingTilePosB, movingTilePosA, speedOfMovingTiles));
            // }
        }
    }


    /*
    *  Method gets called to move the tile up and down.
    */
    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f * time/5;
        while (i < 1.0f) 
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
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
        transform.parent.transform.position = movingTilePosA;
    }

    public void GoToB()
    {
        transform.parent.transform.position = movingTilePosB;
    }

    public void SaveCurrentPosition()
    {
        savedCurrentPosition = transform.position;
    }

    public void GoBackToSavedPosition()
    {
        transform.parent.transform.position = savedCurrentPosition;
    }

    // Not working yet
    // public void ResetTransformToParent()
    // {
    //     transform.position = transform.parent.transform.position;
    // }

}