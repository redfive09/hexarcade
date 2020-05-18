using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Platform))]

public class PlatformEditor : Editor {
    

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        Platform platform = (Platform) target;
        base.OnInspectorGUI();

        if (GUILayout.Button("Change Color"))
        {            
            platform.SetColor();
        }
        
        if (GUILayout.Button("Set startingTiles"))
        {
            platform.SetStartingPlatform();
        }

        if (GUILayout.Button("Set winningTiles"))
        {
            platform.SetWinningPlatform();
        }
        
        if (GUILayout.Button("Delete Platform"))
        {
            platform.DestroyPlatform(true);
        }
    }
} // END OF CLASS