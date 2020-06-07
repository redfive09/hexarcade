using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ball))]

public class BallEditor : Editor {

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Ball ball = (Ball) target;

        // if (GUILayout.Button("Go to spawn position"))
        // {
        //     MapSettings map = GameObject.Find("Map").GetComponent<MapSettings>();
        //     Tiles tiles = GameObject.Find("Map/Tiles").GetComponent<Tiles>();
        //     tiles.CollectTiles();
        //     Hexagon startTile = tiles.GetSpawnPosition(ball.GetPlayerNumber());
        //     ball.GoToSpawnPosition(startTile, map.GetSpawnPositionOffset());
        //     ball.GetComponentInParent<Players>().GetCamera().SetPosition(ball.transform);
        // }
    }
} // END OF CLASS