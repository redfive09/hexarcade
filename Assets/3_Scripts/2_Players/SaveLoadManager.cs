using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

// Loads, saves and updates high score
public static class SaveLoadManager
{
    private static string nameOfSaveFile = "/time_records.lox";

    // Update high score in file
    public static void SaveTimes(Dictionary<string, float> newBestTimes)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + nameOfSaveFile, FileMode.Create);

        TimeKeeper data = new TimeKeeper(newBestTimes);
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    // Retrieve high score from file
    public static Dictionary<string, float> LoadTimes()
    {
        if (File.Exists(Application.persistentDataPath + nameOfSaveFile))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + nameOfSaveFile, FileMode.Open);

            TimeKeeper data = binaryFormatter.Deserialize(fileStream) as TimeKeeper;

            fileStream.Close();

            // In case someone has still the old version with another datatype, replace it with the new one
            if(!typeof(IDictionary<string, float>).IsAssignableFrom(data.GetTimes().GetType())) // thx to: https://stackoverflow.com/questions/16956903/determine-if-type-is-dictionary
            {
                return ResetTimes();
            }
            return data.GetTimes();
        }
        else
        {
            return ResetTimes();
        }
    }

    // Overwrite/reset high score to negative values
    // Negative values equal to non-existent time record
    public static Dictionary<string, float> ResetTimes()
    {
        Dictionary<string, float> dictionary = new Dictionary<string, float>();
        
        Debug.Log("TimesReseted");
        SaveTimes(dictionary);
        return dictionary;
    }
}

[Serializable]
public class TimeKeeper
{
    private Dictionary<string, float> bestTimes;

    public TimeKeeper(Dictionary<string, float> newBestTimes)
    {
        bestTimes = newBestTimes;
    }

    public Dictionary<string, float> GetTimes()
    {
        return bestTimes;
    }
}
