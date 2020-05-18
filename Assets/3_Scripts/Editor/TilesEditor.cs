using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tiles))]

public class TilesEditor : Editor {

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Tiles tiles = (Tiles) target;
        
        if (GUILayout.Button("Print pathTiles"))
        {            
            tiles.PrintPathTiles(); // Just for testing purposes
        }
    }
} // END OF CLASS