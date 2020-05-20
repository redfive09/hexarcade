using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

// Loads, saves and updates high score
public static class SaveLoadManager
{
    private static string nameOfSaveFile = "/time_records.lox";

    // Update high score in file
    public static void SaveTimes(float[] newBestTimes)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + nameOfSaveFile, FileMode.Create);

        TimeKeeper data = new TimeKeeper(newBestTimes);
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    // Retrieve high score from file
    public static float[] LoadTimes()
    {
        if (File.Exists(Application.persistentDataPath + nameOfSaveFile))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + nameOfSaveFile, FileMode.Open);

            TimeKeeper data = binaryFormatter.Deserialize(fileStream) as TimeKeeper;

            fileStream.Close();
            return data.GetTimes();
        }
        else
        {
            return ResetTimes();
        }
    }

    // Overwrite/reset high score to negative values
    // Negative values equal to non-existent time record
    public static float[] ResetTimes()
    {
        float[] newArray = new float[SceneManager.sceneCountInBuildSettings];

        for (int i = 0; i < newArray.Length; i++)
        {
            newArray[i] = float.MinValue;
        }
        SaveTimes(newArray);
        return newArray;
    }
}

[Serializable]
public class TimeKeeper
{
    private float[] bestTimes;

    public TimeKeeper(float[] newBestTimes)
    {
        bestTimes = newBestTimes;
    }

    public float[] GetTimes()
    {
        return bestTimes;
    }
}
