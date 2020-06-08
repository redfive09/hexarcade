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
        Dictionary<int, List<Hexagon>>[] tiles = GetComponent<Tiles>().GetAllTiles();
        for(int h = 0; h < tiles.Length; h++)
        {            
            int sizeOfDictionary = tiles[h].Count;

            for(int i = 0; i < sizeOfDictionary; i++)
            {
                if(tiles[h].TryGetValue(i, out List<Hexagon> hexagonList))
                {
                    for(int k = 0; k < hexagonList.Count; k++)
                    {
                        if(setCrackables)
                        {
                            HexagonBehaviour hexagon = hexagonList[k].GetComponent<HexagonBehaviour>();
                            hexagon.SetCrackedTileBreaksInTime(crackedTileBreaksInSeconds);
                        }

                        if(setSpecialVelocity)
                        {
                            HexagonSpecial hexagon = hexagonList[k].GetComponent<HexagonSpecial>();
                            hexagon.SetVelocity(velocity);
                        }                        
                    }
                }
                else
                {
                    sizeOfDictionary++;
                }
            }
        }
    }
}