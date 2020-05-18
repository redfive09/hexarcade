using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    Tiles tiles;
    List<Balls> players = new List<Balls>();

    
    void Start()
    {
        tiles = this.transform.GetComponentInChildren<Tiles>();
        tiles.GetStarted();
        SpawnPlayers(1);
    }

    public void AddTiles(Tiles tiles)
    {
        this.tiles = tiles;
    }

    private void SpawnPlayers(int numberOfPlayers)
    {
        var playersFolder = new GameObject();
        playersFolder.name = "Players";
        playersFolder.transform.parent = this.transform;
        
        for(int i = 0; i < numberOfPlayers; i++)
        {
            GameObject playerBall = Instantiate(ball);
            playerBall.name = "Player" + (i + 1);
            Balls player = playerBall.GetComponent<Balls>();            
            players.Add(player);
            player.GoToSpawnPosition(tiles.GetSpawnPosition(i));
            
        }
    }

}
