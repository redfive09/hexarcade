using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Tiles))]

public class TilesEditor : Editor {

    // Thank you Brackeys: https://www.youtube.com/watch?v=RInUu1_8aGw
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Tiles tiles = (Tiles) target;
        
        if (GUILayout.Button("Clearify Names for Non-Standard Tiles"))
        {
            tiles.ResetAllLists();
            AddNameSuffixToNonStandardHexagons(tiles.GetPlatforms());
        }

        if (GUILayout.Button("Enhance hexagon clicking experience (for old scenes)"))
        {
            tiles.ResetAllLists();
            AddScriptToAllHexagonChildren(tiles.GetPlatforms());
        }



        // if (GUILayout.Button("Print pathTiles"))
        // {            
        //     tiles.PrintPathTiles(); // Just for testing purposes
        // }

    }


    /*
     *  Add a script to the each hexagon child in order to auto select its parent (the hexagon gameobject) when it's clicked on at the scene view
     */    
    public void AddScriptToAllHexagonChildren(List<Platform> platforms)
    {
        for(int i = 0; i < platforms.Count; i++)
        {
            List<Hexagon> platformTiles = platforms[i].GetTilesList();
            for(int k = 0; k < platformTiles.Count; k++)
            {                
                Hexagon hexagon = platformTiles[k];
                if(!hexagon.transform.GetChild(0).gameObject.GetComponent<HexagonChild>())
                {
                    hexagon.transform.GetChild(0).gameObject.AddComponent<HexagonChild>();
                }
            }
        }
    }


    /*
     *  Rename all non-standard tiles, so it's easier to navigate in the hierarchy
     */
    private void AddNameSuffixToNonStandardHexagons(List<Platform> platforms)
    {
        for(int i = 0; i < platforms.Count; i++)
        {
            List<Hexagon> platformTiles = platforms[i].GetTilesList();
            for(int j = 0; j < platformTiles.Count; j++)
            {
                Hexagon hexagon = platformTiles[j];                
                
                string nameSeparator = " || ";
                int sizeOfNameSeparator =  nameSeparator.Length; 


                for(int k = 0; k + sizeOfNameSeparator < hexagon.name.Length; k++)
                {
                    string namePart = hexagon.name.Substring(k, sizeOfNameSeparator);
                    if(namePart == nameSeparator)
                    {
                        string standardName = hexagon.name.Substring(0, k) + nameSeparator;
                        hexagon.name = standardName;
                    }
                }

                string name = hexagon.name;
                
                if(hexagon.IsCrackedTile())
                {
                    int number = hexagon.GetCrackedNumber();
                    name += "Cracked " + number + nameSeparator;
                }

                if(hexagon.IsPathTile())
                {
                    int number = hexagon.GetPathNumber();
                    name += "Path " + number + nameSeparator;
                }

                if(hexagon.IsDistractionTile())
                {
                    int number = hexagon.GetDistractionNumber();
                    name += "Distraction " + number + nameSeparator;
                }

                if(hexagon.IsCheckpointTile())
                {
                    int number = hexagon.GetCheckpointNumber();
                    name += "Checkpoint " + number + nameSeparator;
                }

                if(hexagon.IsSpecialTile())
                {
                    HexagonSpecial specialHexagon = hexagon.GetComponent<HexagonSpecial>();
                    name += "Special " + specialHexagon.GetNameOfFunction() + nameSeparator;
                }

                if(hexagon.IsMovingTile())
                {
                    int number = hexagon.GetMovingNumber();
                    name += "Moving " + number + nameSeparator;
                }

                if(hexagon.IsStartingTile())
                {
                    int number = hexagon.GetStartingNumber();
                    name += "Starting " + number + nameSeparator;
                }

                if(hexagon.IsWinningTile())
                {
                    int number = hexagon.GetWinningNumber();
                    name += "Winning " + number + nameSeparator;
                }

                hexagon.name = name;
            }
        }
    }

} // END OF CLASS