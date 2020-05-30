using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tiles))]

public class TilesEditor : Editor {

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Tiles tiles = (Tiles) target;
        
        // GUILayout.Label("All elements of lists getting lost after leaving the playmode.");
        // GUILayout.Label("So if something doesn't work (e. g. changing colour), maybe");
        // GUILayout.Label("it's because of the empty lists. To solve that, press this");
        // GUILayout.Label("button and all the tiles are getting added to their lists again.");

        // if (GUILayout.Button("Load Lists for Editor access"))
        // {
        //     tiles.ResetAllLists();
        // }

        if (GUILayout.Button("Clearify Names for Non-Standard Tiles"))
        {
            tiles.ResetAllLists();
            tiles.AddNameSuffixToNonStandardHexagons();
        }

        if (GUILayout.Button("Enhance hexagon clicking experience (for old scenes)"))
        {
            tiles.ResetAllLists();
            tiles.AddScriptToAllHexagonChildren();
        }



        // if (GUILayout.Button("Print pathTiles"))
        // {            
        //     tiles.PrintPathTiles(); // Just for testing purposes
        // }
    }
} // END OF CLASS