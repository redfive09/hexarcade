using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    // [SerializeField] private Camera playerCamera;

    [SerializeField] private int numberOfPlayers = 1;

    private List<Ball> players = new List<Ball>();


    public void GetStarted()
    {
        InstantiatePlayers(false);
        CollectPlayersAndGetThemStarted();
    }

    /*
     * All the players get added here
    **/
    public void InstantiatePlayers(bool editorMode)
    {
        if(transform.childCount < numberOfPlayers)
        {
            Tiles tiles = GameObject.Find("Map/Tiles").GetComponent<Tiles>();
            MapSettings settings = GetComponentInParent<MapSettings>();
        
            for(int i = transform.childCount; i < numberOfPlayers; i++)
            {
                GameObject playerBall = Instantiate(ball);
                playerBall.name = "Player" + (i + 1);
                playerBall.transform.parent = this.transform;
                
                Ball player = playerBall.GetComponent<Ball>();

                if(editorMode)
                {
                    tiles.ResetAllLists();
                }

                player.GoToSpawnPosition(tiles.GetSpawnPosition(i), settings.GetSpawnPositionOffset());
                
                GetCamera().SetPosition(player.transform);
            }
        }
    }    

    private void CollectPlayersAndGetThemStarted()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            Ball player = transform.GetChild(i).GetComponent<Ball>();
            players.Add(player);
            player.GetStarted(i);            

            GetCamera().SetTraget(player.transform);
        }
    }

    private CameraFollow GetCamera()
    {
        return Camera.main.GetComponent<CameraFollow>(); // TO-DO for multiplayer: a prefab-camera has to be initiated
    }
}
