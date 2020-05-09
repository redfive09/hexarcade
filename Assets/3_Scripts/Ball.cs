﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
 *  Class purpose: Giving each ball (respectively player) values and behaviour 
**/
public class Ball : MonoBehaviour
{
    [SerializeField] float speed = 1000.0f;
    private Hexagon occupiedTile;
    private Vector3 pos;


    /* ------------------------------ STANDARD METHODS BEGINN ------------------------------  */

    /*  
     *  This is the place, where the player gets controlled 
    **/
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        pos = transform.position;

        GetComponent<Rigidbody>().AddForce (movement * (speed * Time.fixedDeltaTime));
    }

    /*  
     *  The player checks, if it is standing on a tile; if true, then save the currentTile and tell the current and former tile
    **/
    void OnCollisionEnter(Collision collision)    
    {        
        GameObject tile = collision.gameObject;

        if(tile.tag == "Tile")
        {
            Hexagon currentTile = tile.GetComponent<Hexagon>();
        
            if(occupiedTile != currentTile)         // Check if the former occupiedTile has changed   
            {
                if(occupiedTile != null)            // Prevent a NullReferenceException
                {
                    occupiedTile.GotUnoccupied();   // Tell the former occupiedTile, that it's not occupied anymore
                }                    
                currentTile.GotOccupied();          // Tell the currentTile, that a player stands on it
                occupiedTile = currentTile;         // Save the current tile
            }            
        }
    }


    /* ------------------------------ GETTER METHODS BEGINN ------------------------------  */
    public Hexagon GetoccupiedTile()
    {        
        return occupiedTile;
    }
    
    public Vector3 GetPos()
    {
        return pos; // current position in world
    }


    /* ------------------------------ BEHAVIOUR METHODS BEGINN ------------------------------  */

    /*  
     *  Let the player spawn above the desired tile
    **/
    public void GoToSpawnPosition(Hexagon spawnTile)
    {
        float distanceAboveTile = 1f; // Should go later to a central place for all settings
        gameObject.transform.position = new Vector3(spawnTile.transform.position.x, spawnTile.transform.position.y + distanceAboveTile, spawnTile.transform.position.z);
    }
    
} // CLASS END