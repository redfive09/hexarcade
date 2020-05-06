using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
[SerializeField] GameObject Ball;
[SerializeField] GameObject MapGenerator;
int [] startPoint;
private GeneratePath gP;
private List<GameObject> allTiles = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        allTiles = MapGenerator.GetComponent<HexTileMapGenerator>().GenerateMap(12, 6, 7);
        // allTiles = MapGenerator.GenerateMap(12, 6, 7);
        Debug.Log(allTiles.Count);

        gP = new GeneratePath(allTiles);
        // startPoint = gP.getStartTile();
        // Debug.Log(startPoint);
        // Ball.transform.position = new Vector3(startPoint[0], 1, startPoint[1]);

    }

   
}
