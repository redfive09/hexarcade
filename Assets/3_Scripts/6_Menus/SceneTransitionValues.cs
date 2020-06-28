using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionValues : MonoBehaviour
{
    public static int currentScene;
    public static string currentSceneName;
    public static string lastMenuName;
    public static float time;
    public static float record;
    public static bool newRecord;
    public static bool alreadyEnteredEndScreen;
    public static bool gameLoadedForTheFirstTime = true;
    public static string playerName;
    public static List<string> allLevels;
    public static Dictionary<string, List<string>> worlds;
    public static List<string> worldList;    
}
