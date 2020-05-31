using System.Collections.Generic;
using UnityEngine;

public class TilesApplyForAll : MonoBehaviour
{
    [SerializeField] private bool applyForAllCrackableTiles = false;
    [SerializeField] private float crackedTileBreaksInSeconds;

    [SerializeField] private bool applyForAllVelocityTiles = false;
    [SerializeField] private float velocity;


   public void GetStarted(Dictionary<int, List<Hexagon>>[] tiles)
    {        
        for(int h = 0; h < tiles.Length; h++)
        {            
            int sizeOfDictionary = tiles[h].Count;

            for(int i = 0; i < sizeOfDictionary; i++)
            {
                if(tiles[h].TryGetValue(i, out List<Hexagon> hexagonList))
                {
                    for(int k = 0; k < hexagonList.Count; k++)
                    {
                        if(applyForAllCrackableTiles)
                        {
                            HexagonBehaviour hexagon = hexagonList[k].GetComponent<HexagonBehaviour>();
                            hexagon.SetCrackedTileBreaksInTime(crackedTileBreaksInSeconds);
                        }

                        if(applyForAllVelocityTiles)
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