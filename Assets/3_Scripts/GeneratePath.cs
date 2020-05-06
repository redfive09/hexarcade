using System.Collections.Generic;
using UnityEngine;

namespace _3_Scripts
{
    public class GeneratePath : MonoBehaviour
    {
        private static List<int[]>  pathCoordinates = new List<int[]>();

        void Start()
        {
            getStartPoint(Random.Range(1, 3));
            getNextCoordinate();
            print("loaded");
        }

        //Next Step of the path
        void getNextCoordinate()
        {
            int[] currentPath = pathCoordinates[0];
            while (currentPath[0] < 3)
            {
                int randomNumber = Random.Range(1, 4);
                bool odd = (currentPath[1] % 2) != 0;
                int[] nextPath = new int[] {currentPath[0] + getNextPath(odd, randomNumber)[0], currentPath[1] + getNextPath(odd, randomNumber)[1]};
                if (nextPath[1] > 2 || nextPath[1] < -3) break;
                pathCoordinates.Add(nextPath);
                currentPath = nextPath;
                print(currentPath[0] + ", " + currentPath[1]);
            }
        }

        
        int[] getNextPath(bool odd, int randomNumber)
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
        void getStartPoint(int randomNumber)
        {
            switch (randomNumber)
            {
                case 1:
                    pathCoordinates.Add(new int[] {-4, 1});
                    print("-4, 1");
                    break;
                case 2:
                    pathCoordinates.Add(new int[] {-4, -1});
                    print("-4, -1");
                    break;
            }

        }

        public List<int[]> getPathList()
        {
            return pathCoordinates;
        }

        public int[] getStartTile() 
        {
            return pathCoordinates[0];
        }
    }
}