using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    // [SerializeField] private Camera playerCamera;

    [SerializeField] private int numberOfPlayers = 1;
    [SerializeField] private int numberOfCheckpoints;
    [SerializeField] private Vector3 spawnPositionAtTile = new Vector3(0, 1, 0);

    private List<Ball> players = new List<Ball>();
    private Dictionary<int, List<Hexagon>> startingTiles;
    

    public void GetStarted(Dictionary<int, List<Hexagon>> startingTiles, Dictionary<int, List<Hexagon>> checkpointTiles, bool introductionScreen)
    {   
        this.startingTiles = startingTiles;
        SpawnPlayers(checkpointTiles, introductionScreen);
    }

    /*
     * All the players get added here
    **/
    void SpawnPlayers(Dictionary<int, List<Hexagon>> checkpointTiles, bool introductionScreen)
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            GameObject playerBall = Instantiate(ball);
            playerBall.name = "Player" + (i + 1);
            playerBall.transform.parent = this.transform;

            CameraFollow playerCam = GetComponentInChildren<CameraFollow>(); // For multiplayer: a prefab-camera has to be initiated
            playerCam.GetStarted(playerBall.transform);

            Ball player = playerBall.GetComponent<Ball>();
            players.Add(player);
            player.GoToSpawnPosition(GetSpawnPosition(i), spawnPositionAtTile);
            player.GetStarted(i, numberOfCheckpoints, checkpointTiles, introductionScreen);
            
        }
    }

    Hexagon GetSpawnPosition(int player)
    {
        return startingTiles[player][0]; // Instead of 0, it can be more flexible later, for example with a random number, in case there are more startingTiles for that player
    }

}
