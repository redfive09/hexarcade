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
        movingTilePosA = SetValuesForMovingHexagons(movingTilePosA);
        movingTilePosB = SetValuesForMovingHexagons(movingTilePosB);
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


    private Vector3 SetValuesForMovingHexagons(Vector3 vector)
    {
        // if(vector.x == 0)
        // {
        //     vector.x = this.transform.position.x;
        // }

        // if(vector.y == 0)
        // {
        //     vector.y = this.transform.position.y;
        // }

        // if(vector.z == 0)
        // {
        //     vector.z = this.transform.position.z;
        // }
        return vector;
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
    /* 
     * That's not the right way to do it, but it would be helpful     
     * Button therefore deactivated for right now
    */
    public void PrintCurrentWorldPosition()
    {
        Debug.Log(this.transform.parent.transform.position - this.transform.position);
    }

    public void CopyCurrentPositionToA()
    {
        movingTilePosA = transform.position;
    }

    public void CopyCurrentPositionToB()
    {
        movingTilePosB = transform.position;
    }

    private Vector3 savedCurrentPosition = new Vector3();
    public void SaveCurrentPosition()
    {
        savedCurrentPosition = transform.position;
    }

    public void GoBackToSavedPosition()
    {
        transform.position = savedCurrentPosition;
    }

}