using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

// Loads, saves and updates high score
public static class SaveLoadManager
{
    private static string nameOfSaveFile = "/time_records.lox";
    private static string versionController = "2020-06-28";

    public static TimeKeeper Load()
    {
        if (File.Exists(Application.persistentDataPath + nameOfSaveFile))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + nameOfSaveFile, FileMode.Open);

            TimeKeeper data = binaryFormatter.Deserialize(fileStream) as TimeKeeper;

            fileStream.Close();

            if(data.IsRightVersion(versionController))
            {
                return data;
            }
            else
            {
                return Reset();
            }
        }
        return Reset();
    }

    public static TimeKeeper Reset()
    {
        Dictionary<string, float> dictionary = new Dictionary<string, float>();
        TimeKeeper newKeep = new TimeKeeper(dictionary, versionController);
        Save(newKeep);
        Debug.Log("Loadings Reseted");
        return newKeep;
    }  

    public static void Save(TimeKeeper newKeep)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + nameOfSaveFile, FileMode.Create);

        TimeKeeper data = newKeep;
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();        
    }


    // Update high score in file
    public static void SaveTimes(Dictionary<string, float> newBestTimes)
    {
        TimeKeeper data = Load();
        data.SetBestTimes(newBestTimes);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + nameOfSaveFile, FileMode.Create);

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
            TimeKeeper data;
            
            try
            {
                data = binaryFormatter.Deserialize(fileStream) as TimeKeeper;
                fileStream.Close();
                if(!data.IsRightVersion(versionController))
                {                    
                    Reset();
                    return LoadTimes();
                }                
                return data.GetTimes();
            }
            catch
            {
                fileStream.Close();
                Reset();
                return LoadTimes();
            }
            

            // In case someone has still the old version with another datatype, replace it with the new one
            // if(!typeof(IDictionary<string, float>).IsAssignableFrom(data.GetTimes().GetType())) // thx to: https://stackoverflow.com/questions/16956903/determine-if-type-is-dictionary
            // {
            //     return ResetTimes();
            // }
            
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
    private string versionControl = "";
    private string playerName = "hexanymous";

    public TimeKeeper(Dictionary<string, float> newBestTimes, string versionControl)
    {
        this.versionControl = versionControl;
        bestTimes = newBestTimes;
    }

    public Dictionary<string, float> GetTimes()
    {
        return bestTimes;
    }

    public void SetBestTimes(Dictionary<string, float> newBestTimes)
    {
        bestTimes = newBestTimes;
    }

    public bool IsRightVersion(string versionController)
    {
        return versionControl == versionController;
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

}
