using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tiles))]

public class TilesEditor : Editor {

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Tiles tiles = (Tiles) target;
        
        GUILayout.Label("Values in editor getting lost after entering the playmode (e. g. lists).");
        GUILayout.Label("So if something doesn't work (e. g. changing colour), maybe");
        GUILayout.Label("it's because of the missing values. To solve that, press this");
        GUILayout.Label("button and all the tiles are getting added to their lists again.");

        if (GUILayout.Button("Load Lists for Editor access"))
        {            
            tiles.CollectTiles();
        }

        // if (GUILayout.Button("Print pathTiles"))
        // {            
        //     tiles.PrintPathTiles(); // Just for testing purposes
        // }
    }
} // END OF CLASS