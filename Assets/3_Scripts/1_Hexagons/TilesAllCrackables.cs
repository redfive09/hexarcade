using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesAllCrackables : MonoBehaviour
{
    [SerializeField] private bool applyForAllCrackableTiles = false;
    [SerializeField] private float crackedTileBreaksInSeconds;

   private Dictionary<int, List<Hexagon>> crackableTiles;

   public void GetStarted(Dictionary<int, List<Hexagon>> crackableTiles)
    {        
        this.crackableTiles = crackableTiles;

        if(applyForAllCrackableTiles)
        {
            SetCrackableTiles();
        }
    }

    private void SetCrackableTiles()
    {
        for(int i = 0; i < crackableTiles.Count; i++)
        {
            List<Hexagon> tilesList = crackableTiles[i];

            for(int k = 0; k < tilesList.Count; k++)
            {
                HexagonBehaviour hexagon = tilesList[k].GetComponent<HexagonBehaviour>();
                hexagon.SetCrackedTileBreaksInTime(crackedTileBreaksInSeconds);
            }            
        }
    }
}