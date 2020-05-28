using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSettings : MonoBehaviour
{
    
    [SerializeField] private bool introductionScreen;
    [SerializeField] private int numberOfCheckpoints;
    [SerializeField] private float stoptimeForCheckpoints; //  smaller/equal to zero means, no stopwatch for choosing the checkpoints
    [SerializeField] private bool standardTilesMeansLosing;
    [SerializeField] private Vector3 spawnPositionOffset = new Vector3(0, 1, 0);


    public bool IsIntroductionScreen()
    {
        return introductionScreen;
    }

    public int GetNumberOfCheckpoints()
    {
        return numberOfCheckpoints;
    }

    public float GetStoptimeForCheckpoints()
    {
        return stoptimeForCheckpoints;
    }

    public bool DoesStandardTilesMeansLosing()
    {
        return standardTilesMeansLosing;
    }

    public Vector3 GetSpawnPositionOffset()
    {
        return spawnPositionOffset;
    }
    

}
