using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraFollow))]

public class CameraFollowEditor : Editor {

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CameraFollow cameraFollow = (CameraFollow) target;
        
        // if (GUILayout.Button("Print World Position"))
        // {            
        //     hexagon.PrintCurrentWorldPosition();
        // }

        if (GUILayout.Button(""))
        {
            
        }
    }

} // END OF CLASS


