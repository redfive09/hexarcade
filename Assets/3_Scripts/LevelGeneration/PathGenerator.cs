using System.Collections.Generic;
using UnityEngine;

// namespace _3_Scripts
// {

    /*  
    *  Class purpose: Creating a path and returning its tiles in a list
    **/ 
    public class PathGenerator : MonoBehaviour
    {
        /* Method receives tiles (of a map) and the coordinates of the path
        *  Returns: List of hexagon tiles, which are part of the path, only
        **/     
        public List<Hexagon> GetPathTiles(List<Hexagon> tiles, int[,] pathCoordinates)
        {
            List<Hexagon> path = new List<Hexagon>(); // All tiles of the path get added here

            for(int k = 0; k < pathCoordinates.GetLength(0); k++) // Going through each path coordinate
            {
                
                for(int i = 0; i < tiles.Count; i++) // Loop goes through each tile (of a map)
                {
                // Getting map coordinates of the current tile
                float x = tiles[i].GetPositionX();
                float z = tiles[i].GetPositionZ();

                    // If the path coordinate is identical with the tile coordinate, then add it to "path"
                    if(pathCoordinates[k, 0] == x && pathCoordinates[k, 1] == z) 
                    {                        
                        path.Add(tiles[i]);
                        tiles[i].SetIsPath(0); // Tell the current tile, that it is part of the path                        
                    }
                }
            }
            return path;
        }



        // ----------------- FROM HERE THE OLD RANDOM GENERATOR, WHICH IS NOT IN USE RIGHT NOW -----------------

        public List<int[]>  generatedPathCoordinates = new List<int[]>();

        public PathGenerator()
        {
            // GetStartPoint(Random.Range(1, 3));
            // GetNextCoordinate();
        }


        //Next Step of the path
        void GetNextCoordinate()
        {
            int[] currentPath = generatedPathCoordinates[0];
            while (currentPath[0] < 3)
            {
                int randomNumber = Random.Range(1, 4);
                bool odd = (currentPath[1] % 2) != 0;
                int[] nextPath = new int[] {currentPath[0] + GetNextPath(odd, randomNumber)[0], currentPath[1] + GetNextPath(odd, randomNumber)[1]};
                if (nextPath[1] > 2 || nextPath[1] < -3) {
                    break;
                }
                generatedPathCoordinates.Add(nextPath);
                currentPath = nextPath;
                // print(currentPath[0] + ", " + currentPath[1]);
            }
        }

        
        int[] GetNextPath(bool odd, int randomNumber)
        {
            if (odd)
            {    
                switch(randomNumber)
                {
                    case 1:
                        return new int[] {1, 1};
                    case 2:
                        return new int[] {1, 0};
                    case 3:
                        return new int[] {1, -1};
                }
            }
            else
            {
                switch(randomNumber)
                {
                    case 1:
                        return new int[] {0, 1};
                    case 2:
                        return new int[] {1, 0};
                    case 3:
                        return new int[] {0, -1};
                }
            }
            

            return new int[] {0, 0};
        }

        //der furthest punkt auf der linke seite soll der start sein.
        void GetStartPoint(int randomNumber)
        {
            switch (randomNumber)
            {
                case 1:
                    generatedPathCoordinates.Add(new int[] {-4, 1});
                    // print("-4, 1");
                    break;
                case 2:
                    generatedPathCoordinates.Add(new int[] {-4, -1});
                    // print("-4, -1");
                    break;
            }
        }
    }
// }