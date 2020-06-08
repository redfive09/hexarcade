using System.Collections.Generic;
using UnityEngine;

/* 
 * This class serves its purpose for the editor in order to change several tiles to a type at once
 */

public class TilesApplyForAll : MonoBehaviour
{
    [SerializeField] private float crackedTileBreaksInSeconds;
    [SerializeField] private float velocity;


    public void SetTiles(bool setCrackables, bool setSpecialVelocity)
    {
        Hexagon[] tiles = GetComponent<Tiles>().GetAllTiles();

        for(int i = 0; i < tiles.Length; i++)
        {
            if(setCrackables)
            {
                HexagonBehaviour hexagon = tiles[i].GetComponent<HexagonBehaviour>();
                hexagon.SetCrackedTileBreaksInTime(crackedTileBreaksInSeconds);
            }

            if(setSpecialVelocity)
            {
                HexagonSpecial hexagon = tiles[i].GetComponent<HexagonSpecial>();
                hexagon.SetVelocity(velocity);
            }
        }
    }
}