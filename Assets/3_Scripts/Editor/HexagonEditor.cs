﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hexagon))]

public class HexagonEditor : Editor {

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Hexagon hexagon = (Hexagon) target;
        
        // if (GUILayout.Button("Print World Position"))
        // {            
        //     hexagon.PrintCurrentWorldPosition();
        // }

        if (GUILayout.Button("Change Color"))
        {            
            hexagon.SetColor();
        }

        if (GUILayout.Button("Update Additional Scripts"))
        {
            if(hexagon.IsMovingTile() && !hexagon.GetComponent<HexagonMovingTiles>()) hexagon.gameObject.AddComponent<HexagonMovingTiles>();
            if(!hexagon.IsMovingTile() && hexagon.GetComponent<HexagonMovingTiles>()) DestroyImmediate(hexagon.GetComponent<HexagonMovingTiles>());

            if(hexagon.IsDistractionTile() && !hexagon.GetComponent<HexagonDistraction>()) hexagon.gameObject.AddComponent<HexagonDistraction>();
            if(!hexagon.IsDistractionTile() && hexagon.GetComponent<HexagonDistraction>()) DestroyImmediate(hexagon.GetComponent<HexagonDistraction>());

            if(hexagon.IsSpecialTile() && !hexagon.GetComponent<HexagonSpecial>()) hexagon.gameObject.AddComponent<HexagonSpecial>();
            if(!hexagon.IsSpecialTile() && hexagon.GetComponent<HexagonSpecial>()) DestroyImmediate(hexagon.GetComponent<HexagonSpecial>());
        }

        // if (GUILayout.Button("Delete Hexagon"))
        // {
        //     // bool inEditor = !EditorApplication.isPlaying;
        //     hexagon.DestroyHexagon(true, 0);
        // }

    }
} // END OF CLASS