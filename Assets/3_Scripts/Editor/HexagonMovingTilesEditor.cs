using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexagonMovingTiles))]

public class HexagonMovingTilesEditor : Editor {
    

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        HexagonMovingTiles hexagonMovingTiles = (HexagonMovingTiles) target;
        base.OnInspectorGUI();

        GUILayout.Label("");
        GUILayout.Label("Any submited(!) change will take its value from above");
        GUILayout.Label("and set it to all tiles of that platfrom.");

        if (GUILayout.Button("Copy Current Position To A"))
        {            
            hexagonMovingTiles.CopyCurrentPositionToA();
        }

        if (GUILayout.Button("Copy Current Position To B"))
        {            
            hexagonMovingTiles.CopyCurrentPositionToB();
        }

        if (GUILayout.Button("Save current position"))
        {            
            hexagonMovingTiles.SaveCurrentPosition();
        }

        if (GUILayout.Button("Go Back To Saved Position"))
        {            
            hexagonMovingTiles.GoBackToSavedPosition();
        }


    }
} // END OF CLASS