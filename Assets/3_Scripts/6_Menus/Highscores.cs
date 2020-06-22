using UnityEngine;
using System.Collections;

// Thx to -> https://github.com/SebLague/Dreamlo-Highscores/blob/master/Episode%2002/Highscores.cs && https://www.youtube.com/watch?v=KZuqEyxYZCc
public class Highscores : MonoBehaviour {

	const string privateCode = "ZzS_SAddkkufuTI20AAeKA1IB4xtQMEEuT0Lb0MjZnUg";
	const string publicCode = "5ef12968377eda0b6c5ebd12";
	const string webURL = "http://dreamlo.com/lb/";
    
    // ABSOLUTELY PRIVAT LINK: http://dreamlo.com/lb/ZzS_SAddkkufuTI20AAeKA1IB4xtQMEEuT0Lb0MjZnUg

	HighscoresDisplay highscoreDisplay;
	public Highscore[] highscoresList;
	static Highscores instance;
    static bool uploaded = false;
	
	void Awake() {
		highscoreDisplay = GetComponent<HighscoresDisplay> ();
		instance = this;
	}

	public static void AddNewHighscore(string level, string username, float time) {
        uploaded = false;
        Highscores newHighscore = new Highscores();
		newHighscore.StartCoroutine(newHighscore.UploadNewHighscore(level,username,time));
	}

	IEnumerator UploadNewHighscore(string level, string username, float time) {
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(level) +  "/" + username + "/" + time);
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
			highscoreDisplay.OnHighscoresDownloaded(highscoresList);
		}
		else {
			print ("Error Downloading: " + www.error);
            GetComponent<HighscoresDisplay>().Error();
		}
	}

	void FormatHighscores(string textStream) {
		string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];

		for (int i = 0; i <entries.Length; i++) {
			string[] entryInfo = entries[i].Split(new char[] {'|'});
			string level = entryInfo[0];
            string username = entryInfo[1];
			float time = float.Parse(entryInfo[2]);
			highscoresList[i] = new Highscore(level,username,time);
            print (highscoresList[i].level + ": " + highscoresList[i].username + ": " + highscoresList[i].time);
		}
	}

}

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

