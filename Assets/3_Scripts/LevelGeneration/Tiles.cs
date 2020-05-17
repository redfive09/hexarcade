using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    private List<Platform> platforms = new List<Platform>(); // Holds all platforms of the current level
    private List<Hexagon> pathTiles = new List<Hexagon>(); // Holds all tiles of the current path
    private List<Hexagon> startingTiles;
    private List<Hexagon> winningTiles;


    /*
     *  Add a new platform
     */
    public void AddPlatform(Platform platform)
    {
        platforms.Add(platform);
    }

    /*
     *  Return the searched platform, otherwise return null
     */
    public Platform GetPlatform(string platformName)
    {
        for(int i = 0; i < platforms.Count; i++)
        {
            if(platforms[i].name == platformName)
            {
                return platforms[i];
            }
        }
        return null;
    }

    /*     
     * Remove a platform from this list of platforms, but it does not destroy it!
     * For destorying a platform, you have to tell the platform itself with DestroyHexagon()!
    */
    public void RemovePlatform(Platform platform)
    {
        for(int i = 0; i < platforms.Count; i++)
        {
            if(platforms[i] == platform)
            {                
                platforms.RemoveAt(i);
            }
        }
    }

    public int GetNumberOfPlatforms()
    {
        return platforms.Count;
    }

} // END OF CLASS
