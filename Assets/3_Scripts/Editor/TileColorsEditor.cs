using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileColors))]

public class TileColorsEditor : Editor {
    

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        TileColors tileColors = (TileColors) target;
        base.OnInspectorGUI();

        GUILayout.Label("");
        GUILayout.Label("Buttons for constant colour changes. If they don't work, click once");
        GUILayout.Label("(not more!) at 'Activate Editor Access' in the 'Map' instructor.");

        if (GUILayout.Button("Change Color of CrackedTiles"))
        {            
            tileColors.ChangeCrackedTilesColor();
        }

        if (GUILayout.Button("Change Color of PathTiles"))
        {            
            tileColors.ChangePathTilesColor();
        }

        if (GUILayout.Button("Change Color of DistractionTiles"))
        {            
            tileColors.ChangeDistractionTilesColor();
        }

        if (GUILayout.Button("Change Color of CheckpointTiles"))
        {            
            tileColors.ChangeCheckpointTilesColor();
        }

        if (GUILayout.Button("Change Color of SpecialTiles"))
        {            
            tileColors.ChangeSpecialTilesColor();
        }

        if (GUILayout.Button("Change Color of MovingTiles"))
        {            
            tileColors.ChangeMovingTilesColor();
        }
        
        if (GUILayout.Button("Change Color of StartingTiles"))
        {            
            tileColors.ChangeStartingTilesColor();
        }

        if (GUILayout.Button("Change Color of WinningTiles"))
        {            
            tileColors.ChangeWinningTilesColor();
        }

        if (GUILayout.Button("Change Color of standardTiles"))
        {            
            tileColors.ChangeColorOfStandardTiles();
        }        
        
        if (GUILayout.Button("Change Color of every Tile"))
        {            
            tileColors.ChangeColorOfAllTiles();
        }

    }
} // END OF CLASS