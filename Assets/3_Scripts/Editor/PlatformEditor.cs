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
            tiles.ResetAllLists();
            platform.SetColor();
        }

        if (GUILayout.Button("Set to cracked tiles"))
        {
            tiles.ResetAllLists();
            platform.SetCrackedPlatform();
        }

        if (GUILayout.Button("Set to path tiles"))
        {
            tiles.ResetAllLists();
            platform.SetPathPlatform();
        }
        
        if (GUILayout.Button("Set to starting tiles"))
        {
            tiles.ResetAllLists();
            platform.SetStartingPlatform();
        }

        if (GUILayout.Button("Set to winning tiles"))
        {
            tiles.ResetAllLists();
            platform.SetWinningPlatform();
        }
        
        if (GUILayout.Button("Set to checkpoint tiles"))
        {
            tiles.ResetAllLists();
            platform.SetCheckpointPlatform();
        }

        if (GUILayout.Button("Set to distraction tiles"))
        {
            tiles.ResetAllLists();
            platform.SetDistractionPlatform();
        }

        if (GUILayout.Button("Set to special tiles"))
        {
            tiles.ResetAllLists();
            platform.SetSpecialPlatform();
        }

        if (GUILayout.Button("Untag all tiles"))
        {
            tiles.ResetAllLists();
            platform.UntagAllHexagons();
        }
        
        if (GUILayout.Button("Delete Platform"))
        {
            platform.DestroyPlatform(true);
        }
    }
} // END OF CLASS