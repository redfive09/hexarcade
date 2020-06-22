using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    // [SerializeField] private Camera playerCamera;

    [SerializeField] private int numberOfPlayers = 1;
    int childCount;

    private List<Ball> players = new List<Ball>();


    public void GetStarted()
    {        
        InstantiatePlayers(false);
    }

    /*
     * All the players get added here
    **/
    public void InstantiatePlayers(bool inEditor)
    {
        MapSettings settings = GetComponentInParent<MapSettings>();
        Hexagon[] getSpawnPositions = GetSpawnPositions(numberOfPlayers);
    
        childCount = transform.childCount;
        for(int i = 0; i < numberOfPlayers; i++)
        {
            if(childCount - 1 >= i)
            {
                CheckForCompatibilityWithCurrentVersion(i, inEditor);
            }

            if(childCount - 1 < i)
            {
                GameObject playerBall = Instantiate(ball);
                playerBall.name = "Player" + (i + 1);
                playerBall.transform.parent = this.transform;                
            }
            
            Ball player = transform.GetChild(i).GetComponent<Ball>();
            players.Add(player);
            player.GoToSpawnPosition(getSpawnPositions[i], settings.GetSpawnPositionOffset(), true);

            CameraFollow camera = GetCameraScript(); 
            camera.GetReady(player.transform);

            player.GetStarted(i);
        }
    }

    private Hexagon[] GetSpawnPositions(int numberOfPlayers)
    {
        Tiles tiles = GameObject.Find("Map/Tiles").GetComponent<Tiles>();
        Hexagon[] spawnPositions = new Hexagon[numberOfPlayers];
        int spawnTileNumber = -1;

        // In order to prevent the situation, that the level designer didn't think about, that for example player1 is basically
        // the player with the playerID = 0 and is supposed to have spawnTileNumber = 0, but instead chose to have startingTileNumbers from 1 or even higher

        for(int i = 0; i < numberOfPlayers; i++)
        {
            Hexagon spawnPosition = null;

            while(spawnPosition == null)
            {
                if(spawnTileNumber < i)
                {
                    spawnTileNumber = i;
                }
                else
                {
                    spawnTileNumber++;
                }

                // if spawnPosition is null again, then it goes another round and spawnTileNumber will increase by one and try again to get a spawn position in the list
                spawnPosition = tiles.GetSpawnPosition(spawnTileNumber);

                if(spawnTileNumber >= int.MaxValue)
                {
                    Debug.Log("Not enough startingTiles on the map!!!");
                    return spawnPositions;
                }
            }
            spawnPositions[i] = spawnPosition;
        }
        return spawnPositions; 
    }
    
    public CameraFollow GetCameraScript()
    {
        CameraFollow camera = Camera.main.GetComponent<CameraFollow>(); // TO-DO for multiplayer: a prefab-camera has to be initiated
        if(camera == null) 
        {
            GameObject.FindWithTag("MainCamera").AddComponent<CameraFollow>();
            camera = Camera.main.GetComponent<CameraFollow>();
            camera.SetFocusOnTarget(false);
        }
        return camera;
 
    }

    private void CheckForCompatibilityWithCurrentVersion(int playerID, bool inEditor)
    {
        GameObject player = transform.GetChild(playerID).gameObject;
        if(!player.transform.GetComponentInChildren<SkipButton>().HasSkipButtonReference())
        {
            if(inEditor)
            {
                DestroyImmediate(player);
            }
            else
            {
                Destroy(player);
                
            }
            childCount--;
            Debug.Log("Update player" + (playerID + 1) + " please. The current one in the scene is missing some new elements.");
        }        
    }
}