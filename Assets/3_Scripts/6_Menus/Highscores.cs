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
	// public Highscore[] highscoresList;
	static Highscores instance;
	static bool uploaded = false;	
	string[] separatingStrings = { "___" };
	// private const string levelNameSeparator = "___";
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
		// highscoresList = new Highscore[entries.Length];
		HashSet<string> levelList = new HashSet<string>();

		for (int i = 0; i <entries.Length; i++) 
		{
			string[] entryInfo = entries[i].Split(new char[] {'|'});			
			string[] levelUsername = entryInfo[0].Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
			int intTime = int.Parse(entryInfo[2]);			
			float time = Timer.ConvertToFloat(intTime);			
			SetPosition(levelUsername[0], levelUsername[1], time);
			levelList.Add(levelUsername[0]);
			// print (levelUsername[0] + ": " + levelUsername[1] + ": " + time);
            // print (highscoresList[i].level + ": " + highscoresList[i].username + ": " + highscoresList[i].time);
		}

		// foreach (string level in levelList)
		// {
		// LinkedList<Highscore> levelHighscores = levelBestTimes["Level_1"];
		// LinkedListNode<Highscore> currentEntry = levelHighscores.First;		
		// while(currentEntry != null )
		// {
		// 	Debug.Log(currentEntry.Value.level + ": " + currentEntry.Value.username + ": " + currentEntry.Value.time);
		// 	currentEntry = currentEntry.Next;
		// 		// Debug.Log(levelHighscores.ElementAt(i).level + ": " + levelHighscores[i].username + ": " + levelHighscores[i].time);
			
		// }


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
		
		// Debug.Log(level + ": " + username + " - " + Timer.GetTimeAsString(time, 3));
		
		LinkedListNode<Highscore> currentEntry = levelHighscores.First;		
		while(currentEntry != null )
		{
			if(currentEntry.Value.time > time)
			{
				Highscore newEntry = new Highscore(level, username, time);
				levelHighscores.AddBefore(currentEntry, newEntry);
				currentEntry = levelHighscores.Find(newEntry);
				break;
			}
			else
			{
				currentEntry = currentEntry.Next;
			}			
		}

		if(currentEntry == null)
		{
			levelHighscores.AddLast(new Highscore(level, username, time));
		}
		
		
		
		// for(int i = 0; i < highscoresList.Length; i++)
		// {
		// 	if(string.IsNullOrEmpty(highscoresList[i].username))
		// 	{
		// 		levelHighscores.AddBefore(i, new Highscore(level, username, time))
				// highscoresList[i] = new Highscore(level, username, time);
				// levelHighscores.Insert(i, new Highscore(level, username, time));
				// Debug.Log(i + " || " + level + ": " + highscoresList[i].username + " - " + Timer.GetTimeAsString(highscoresList[i].time, 3));
			// 	break;
			// }

			// else if(highscoresList[i].time > time)
			// {
				// Highscore tempScore;
				// Highscore tempScore2;

				// tempScore = highscoresList[i];
				// highscoresList[i] = new Highscore(level, username, time);
				// levelHighscores.Insert(i, new Highscore(level, username, time));
				// Debug.Log(i + 1 + ". " + level + " || " + highscoresList[i].username + " - " + Timer.GetTimeAsString(highscoresList[i].time, 3));

				// for(int j = i + 1; j < highscoresList.Length; j++)
				// {
				// 	if(string.IsNullOrEmpty(highscoresList[j].username))
				// 	{
				// 		highscoresList[j] = tempScore;
				// 		// Debug.Log(j + 1 + ". " + level + " || " + highscoresList[j].username + " - " + Timer.GetTimeAsString(highscoresList[j].time, 3));
				// 		break;
				// 	}
				// 	else
				// 	{
				// 		tempScore2 = highscoresList[j];
				// 		highscoresList[j] = tempScore;
				// 		tempScore = tempScore2;
				// 	}
				// }
			// 	break;
			// }
			// else
			// {
				// Debug.Log(i + 1 + ". " + highscoresList[i].username + " - " + Timer.GetTimeAsString(highscoresList[i].time, 3));
			// }
			// Debug.Log(i + 1 + ". " + highscoresList[i].username + " - " + Timer.GetTimeAsString(highscoresList[i].time, 3));
		// }
	}
} // End of class



public struct Highscore {
    public string level;
	public string username;
	public float time;

	public Highscore(string _level, string _username, float _time) {
		level = _level;
        username = _username;
		time = _time;		
	}
}



// using UnityEngine.Networking;
// using UnityEngine;
// using System.Collections;

// public class Highscores : MonoBehaviour {

// 	const string privateCode = "4fcZoIDMO02emPvesFklzgGn_thjIN8EKfi4LNMXMcdw";
// 	const string publicCode = "548c04c96e51b60c740bcdec";
// 	const string webURL = "http://dreamlo.com/lb/";

// 	public Highscore[] highscoresList;

// 	void Awake() {

// 		AddNewHighscore("Sebastian", 50);
// 		AddNewHighscore("Mary", 85);
// 		AddNewHighscore("Bob", 92);

// 		DownloadHighscores();
// 	}

// 	public void AddNewHighscore(string username, int score) {
// 		StartCoroutine(UploadNewHighscore(username,score));
// 	}

// 	IEnumerator UploadNewHighscore(string username, int score) {
// 		UnityWebRequest www = new UnityWebRequest(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
// 		yield return www;

// 		if (string.IsNullOrEmpty(www.error))
// 			print ("Upload Successful");
// 		else {
// 			print ("Error uploading: " + www.error);
// 		}
// 	}

// 	public void DownloadHighscores() {
// 		StartCoroutine("DownloadHighscoresFromDatabase");
// 	}

// 	IEnumerator DownloadHighscoresFromDatabase() {
// 		UnityWebRequest www = new UnityWebRequest(webURL + publicCode + "/pipe/");
// 		yield return www;
		
// 		if (string.IsNullOrEmpty(www.error))
// 		{
//             Debug.Log(www.downloadHandler.text);
//         	FormatHighscores(www.downloadHandler.text);
            
//         }
// 		else {
// 			print ("Error Downloading: " + www.error);
// 		}
// 	}

// 	void FormatHighscores(string textStream) {
// 		string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
// 		highscoresList = new Highscore[entries.Length];

// 		for (int i = 0; i <entries.Length; i ++) {
// 			string[] entryInfo = entries[i].Split(new char[] {'|'});
// 			string username = entryInfo[0];
// 			int score = int.Parse(entryInfo[1]);
// 			highscoresList[i] = new Highscore(username,score);
// 			print (highscoresList[i].username + ": " + highscoresList[i].score);
// 		}
// 	}

// }

//     public struct Highscore {
// 	public string username;
// 	public int score;

// 	public Highscore(string _username, int _score) {
// 		username = _username;
// 		score = _score;
// 	}
// }

