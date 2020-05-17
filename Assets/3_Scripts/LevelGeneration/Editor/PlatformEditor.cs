using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Platform))]

public class PlatformEditor : Editor {

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Platform platform = (Platform) target;
        
        if (GUILayout.Button("Delete Platform"))
        {
            platform.DestroyPlatform(true);
        }
    }
} // END OF CLASS