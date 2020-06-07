using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Platform))]

public class PlatformEditor : Editor {
    

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        Platform platform = (Platform) target;
        Tiles tiles = GameObject.Find("Map/Tiles").GetComponent<Tiles>();

        base.OnInspectorGUI();        

        GUILayout.Label("");
        GUILayout.Label("Any submited(!) change will take its value from above");
        GUILayout.Label("and set it to all tiles of that platfrom.");

        if (GUILayout.Button("Change Color"))
        {
            tiles.CollectTiles();
            platform.SetColor();
        }

        if (GUILayout.Button("Set to cracked tiles"))
        {
            tiles.CollectTiles();
            platform.SetCrackedPlatform();
        }

        if (GUILayout.Button("Set to path tiles"))
        {
            tiles.CollectTiles();
            platform.SetPathPlatform();
        }
        
        if (GUILayout.Button("Set to starting tiles"))
        {
            tiles.CollectTiles();
            platform.SetStartingPlatform();
        }

        if (GUILayout.Button("Set to winning tiles"))
        {
            tiles.CollectTiles();
            platform.SetWinningPlatform();
        }
        
        if (GUILayout.Button("Set to checkpoint tiles"))
        {
            tiles.CollectTiles();
            platform.SetCheckpointPlatform();
        }

        if (GUILayout.Button("Set to distraction tiles"))
        {
            tiles.CollectTiles();
            platform.SetDistractionPlatform();
        }

        if (GUILayout.Button("Set to special tiles"))
        {
            tiles.CollectTiles();
            platform.SetSpecialPlatform();
        }

        if (GUILayout.Button("Untag all tiles"))
        {
            tiles.CollectTiles();
            platform.UntagAllHexagons();
        }
        
        if (GUILayout.Button("Delete Platform"))
        {
            platform.DestroyPlatform(true);
        }
    }
} // END OF CLASS