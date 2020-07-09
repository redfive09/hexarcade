using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexagonMovingTiles))]

public class HexagonMovingTilesEditor : Editor {
    

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        HexagonMovingTiles hexagonMovingTiles = (HexagonMovingTiles) target;
        base.OnInspectorGUI();

 
        if (GUILayout.Button("Add current position"))
        {            
            hexagonMovingTiles.AddNewDestination();
        }

        if (GUILayout.Button("Change element's position in array"))
        {            
            hexagonMovingTiles.ElementGoToNextPosition();
        }
        

        if (GUILayout.Button("Move Hexagon to next position"))
        {            
            hexagonMovingTiles.GoToNextPosition();
        }

        // if (GUILayout.Button("Go to A"))
        // {            
        //     hexagonMovingTiles.GoToA();
        // }

        // if (GUILayout.Button("Go to B"))
        // {            
        //     hexagonMovingTiles.GoToB();
        // }

        if (GUILayout.Button("Save current position"))
        {            
            hexagonMovingTiles.SaveCurrentPosition();
        }

        if (GUILayout.Button("Go Back To Saved Position"))
        {            
            hexagonMovingTiles.GoBackToSavedPosition();
        }

        // Not working yet
        // if (GUILayout.Button("Relocate transform to its parent"))
        // {            
        //     hexagonMovingTiles.ResetTransformToParent();
        // }


    }
} // END OF CLASS