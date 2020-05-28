using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Platform))]

public class PlatformEditor : Editor {
    

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        Platform platform = (Platform) target;
        base.OnInspectorGUI();

        GUILayout.Label("");
        GUILayout.Label("Any submited(!) change will take its value from above");
        GUILayout.Label("and set it to all tiles of that platfrom.");

        if (GUILayout.Button("Change Color"))
        {            
            platform.SetColor();
        }

        if (GUILayout.Button("Set to path tiles"))
        {
            platform.SetPathPlatform();
        }
        
        if (GUILayout.Button("Set to starting tiles"))
        {
            platform.SetStartingPlatform();
        }

        if (GUILayout.Button("Set to winning tiles"))
        {
            platform.SetWinningPlatform();
        }
        
        if (GUILayout.Button("Set to checkpoint tiles"))
        {
            platform.SetCheckpointPlatform();
        }

        if (GUILayout.Button("Set to distraction tiles"))
        {
            platform.SetDistractionPlatform();
        }

        if (GUILayout.Button("Set to special tiles"))
        {
            platform.SetSpecialPlatform();
        }

        if (GUILayout.Button("Untag all tiles"))
        {
            platform.UntagAllHexagons();
        }
        
        if (GUILayout.Button("Delete Platform"))
        {
            platform.DestroyPlatform(true);
        }
    }
} // END OF CLASS