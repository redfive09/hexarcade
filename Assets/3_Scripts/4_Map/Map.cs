using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{    

    private Tiles tiles;
    private Players players;
    
    
    void Start()
    {
        SceneTransitionValues.currentScene = SceneManager.GetActiveScene().buildIndex;
        CreateTiles();
        CreatePlayers();
    }

    private void CreateTiles()
    {
        tiles = this.transform.GetComponentInChildren<Tiles>();
        tiles.GetStarted(false);

        GameObject mapGenerator = GameObject.Find("/MapGenerator");
        if(mapGenerator) mapGenerator.SetActive(false);
    }

    private void CreatePlayers()
    {
        players = this.transform.GetComponentInChildren<Players>();
        players.GetStarted();
    }


/* ------------------------------ EDITOR MODE METHODS ------------------------------  */
    public void AddTiles(Tiles tiles)
    {
        this.tiles = tiles;
    }


} // END OF CLASS
