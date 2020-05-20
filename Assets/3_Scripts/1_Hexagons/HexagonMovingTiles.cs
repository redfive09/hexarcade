using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonMovingTiles : MonoBehaviour
{

    [SerializeField] private Vector3 movingTilePosA;
    [SerializeField] private Vector3 movingTilePosB;
    [SerializeField] private float speedOfMovingTiles;

    private Hexagon thisHexagon;

    // Setup standard values for editor mode
    public void Setup()
    {
        speedOfMovingTiles = 2f;
    }

    IEnumerator Start()
    {   
        thisHexagon = this.transform.GetComponentInParent<Hexagon>();
        if(thisHexagon.IsMovingTile())
        {
            movingTilePosA = SetValuesForMovingHexagons(movingTilePosA);
            movingTilePosB = SetValuesForMovingHexagons(movingTilePosB);
            while (thisHexagon.IsMovingTile()) 
            {
                yield return StartCoroutine(MoveObject(this.transform, movingTilePosA, movingTilePosB, speedOfMovingTiles));
                yield return StartCoroutine(MoveObject(this.transform, movingTilePosB, movingTilePosA, speedOfMovingTiles));                        
            }
        }
    }

        Vector3 SetValuesForMovingHexagons(Vector3 vector)
        {
        if(vector.x == 0)
        {
            vector.x = this.transform.position.x;
        }

        if(vector.y == 0)
        {
            vector.y = this.transform.position.y;
        }

        if(vector.z == 0)
        {
            vector.z = this.transform.position.z;
        }
        return vector;
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

}


