using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


// Thx to -> https://github.com/SebLague/Dreamlo-Highscores/blob/master/Episode%2002/Highscores.cs && https://www.youtube.com/watch?v=KZuqEyxYZCc
public class Highscores : MonoBehaviour {

	const string privateCode = "ZzS_SAddkkufuTI20AAeKA1IB4xtQMEEuT0Lb0MjZnUg";
	const string publicCode = "5ef12968377eda0b6c5ebd12";
	const string webURL = "http://dreamlo.com/lb/";
    
    // ABSOLUTELY PRIVAT LINK: http://dreamlo.com/lb/ZzS_SAddkkufuTI20AAeKA1IB4xtQMEEuT0Lb0MjZnUg

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
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(level + separatingStrings[0] + username) +  "/1337/" + time);
		Debug.Log(www.url);
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

	IEnumerator DownloadHighscoresFromDatabase() {
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;
		
		if (string.IsNullOrEmpty (www.error)) {
			FormatHighscores (www.text);
			if(highscoreDisplay) 
			{
				highscoreDisplay.SetError(false);
				highscoreDisplay.OnHighscoresDownloaded(levelBestTimes);
			}
		}
		else {
			print ("Error Downloading: " + www.error);
            if(highscoreDisplay) 
			{
				highscoreDisplay.SetError(true);
				highscoreDisplay.Error();
			}
		}
	}

	void FormatHighscores(string textStream) 
	{
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