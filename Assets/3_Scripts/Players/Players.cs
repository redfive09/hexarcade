using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject stateMachine;
    List<Ball> players = new List<Ball>();
    Dictionary<int, List<Hexagon>> startingTiles;

    public void GetStarted(int numberOfPlayers, Dictionary<int, List<Hexagon>> startingTiles)
    {   
        this.startingTiles = startingTiles;
        SpawnPlayers(numberOfPlayers);        
    }

    /*
     * All the players get added here
    **/
    void SpawnPlayers(int numberOfPlayers)
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            GameObject playerBall = Instantiate(ball);
            playerBall.name = "Player" + (i + 1);
            playerBall.transform.parent = this.transform;

            GameObject SM = Instantiate(stateMachine);
            SM.name = "StateMachine";
            SM.transform.parent = playerBall.transform;

            Ball player = playerBall.GetComponent<Ball>();
            players.Add(player);
            player.GoToSpawnPosition(GetSpawnPosition(i));
            player.GetStarted();

            // Change place later
            // DeactivatePlayerControls(playerBall);
            // Invoke("ActivatePlayerControls", tiles.GetNumberOfPathTiles(i) * tileColorTime);
        }
    }

    Hexagon GetSpawnPosition(int player)
    {
        return startingTiles[player][0]; // Instead of 0, it can be more flexible later, for example with a random number, in case there are more startingTiles for that player
    }

}
