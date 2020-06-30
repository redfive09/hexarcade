using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;


// Thx to -> https://github.com/SebLague/Dreamlo-Highscores/blob/master/Episode%2002/Highscores.cs && https://www.youtube.com/watch?v=KZuqEyxYZCc
public class Highscores : MonoBehaviour {

	const string privateCode = "z0wJr1tbBEiv_bVC-eXGyQCT3HuOeCeE25FS9VUjlnWQ";
	const string publicCode = "5ef90b7e377eda0b6c89efae";
	const string webURL = "http://dreamlo.com/lb/";
    
    // ABSOLUTELY PRIVAT LINKS: 
	// 2020-06-26 http://dreamlo.com/lb/ZzS_SAddkkufuTI20AAeKA1IB4xtQMEEuT0Lb0MjZnUg
	// 2020-07-xx http://dreamlo.com/lb/z0wJr1tbBEiv_bVC-eXGyQCT3HuOeCeE25FS9VUjlnWQ

	private HighscoresDisplay highscoreDisplay;
	static Highscores instance;
	static bool uploaded = false;	
	string[] separatingStrings = { "___" };
	private Dictionary<string, LinkedList<Highscore>> levelBestTimes = new Dictionary<string, LinkedList<Highscore>>();
	
	
	void Awake() {
		highscoreDisplay = GetComponent<HighscoresDisplay> ();
		instance = this;
	}

	public static void AddNewHighscore(string level, string username, float time) {		
        uploaded = false;
		instance.StartCoroutine(instance.UploadNewHighscore(
			level,
			username,
			Timer.ConvertToInt(time)));
	}

	IEnumerator UploadNewHighscore(string level, string username, int time) {
		UnityWebRequest www = new UnityWebRequest(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(level + separatingStrings[0] + username) +  "/1337/" + time);
		// Debug.Log(www.url);
		yield return www;

		if (string.IsNullOrEmpty(www.error)) {
			print ("Upload Successful");
            uploaded = true;
			DownloadHighscores();
		}
		else {
			print ("Error uploading: " + www.error);
		}
	}

	public void DownloadHighscores() {
		StartCoroutine(DownloadHighscoresFromDatabase());
	}

	// IEnumerator DownloadHighscoresFromDatabase() 
	// {
	// 	UnityWebRequest www = new UnityWebRequest(webURL + publicCode + "/pipe/");
	// 	yield return www;

	// 	if (string.IsNullOrEmpty (www.error)) {
	// 		FormatHighscores (www.downloadHandler.text);
	// 		if(highscoreDisplay) 
	// 		{
	// 			highscoreDisplay.SetError(false);
	// 			highscoreDisplay.OnHighscoresDownloaded(levelBestTimes);
	// 		}
	// 	}
	// 	else {
	// 		print ("Error Downloading: " + www.error);
    //         if(highscoreDisplay) 
	// 		{
	// 			highscoreDisplay.SetError(true);
	// 			highscoreDisplay.Error();
	// 		}
	// 	}


	// For this method, thx to -> https://forum.unity.com/threads/api-web-request-error-help-nullreferenceexception.549760/
	IEnumerator DownloadHighscoresFromDatabase()
    {		
        UnityWebRequest request = new UnityWebRequest();
 
        request = UnityWebRequest.Get(webURL + publicCode + "/pipe/");
 
        yield return request.SendWebRequest();
 
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
			if(highscoreDisplay) 
			{
				highscoreDisplay.SetError(true);
				highscoreDisplay.Error();
			}
        }
        else
        {            
            // Debug.Log(request.downloadHandler.text); // Show results as text
            
			FormatHighscores(request.downloadHandler.text);
			if(highscoreDisplay) 
			{
				highscoreDisplay.SetError(false);
				highscoreDisplay.OnHighscoresDownloaded(levelBestTimes);
			}
        }        
    }

	void FormatHighscores(string textStream)
	{
		levelBestTimes.Clear();
		string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);

		for (int i = 0; i <entries.Length; i++) 
		{
			string[] entryInfo = entries[i].Split(new char[] {'|'});			
			string[] levelUsername = entryInfo[0].Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
			int intTime = int.Parse(entryInfo[2]);			
			float time = Timer.ConvertToFloat(intTime);			
			SetPosition(levelUsername[0], levelUsername[1], time);
			// print (levelUsername[0] + ": " + levelUsername[1] + ": " + time);
		}		
	}

	private void SetPosition(string level, string username, float time)
	{		
		LinkedList<Highscore> levelHighscores;	
		if(levelBestTimes.TryGetValue(level, out LinkedList<Highscore> levelHighscoreListExists))
		{
			levelHighscores = levelHighscoreListExists;
		}
		else
		{
			levelHighscores = new LinkedList<Highscore>();
			levelBestTimes[level] = levelHighscores;
		}

		LinkedListNode<Highscore> currentEntry = levelHighscores.First;		
		while(currentEntry != null )
		{
			if(currentEntry.Value.time > time)
			{
				Highscore newEntry = new Highscore(username, time);
				levelHighscores.AddBefore(currentEntry, newEntry);
				currentEntry = new LinkedListNode<Highscore>(newEntry);
				break;
			}
			else
			{
				currentEntry = currentEntry.Next;
			}			
		}

		if(currentEntry == null)
		{
			levelHighscores.AddLast(new Highscore(username, time));
		}
	}
} // END OF CLASS



public struct Highscore {
	public string username;
	public float time;

	public Highscore(string _username, float _time) {
        username = _username;
		time = _time;		
	}
} // END OF CLASS