using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public int totalscore = 0;
	public Text scoretext;
	public Text countdowntext;
	public Text resulttext;
	public Text timelimit;
	public InputField playernameinput;

	public GameObject titlepanel;
	public GameObject resultpanel;
	//public GameObject anothercontroller;

	public BulletShooter launcher;
	public BulletShooter launcher2;
	public BulletShooter launcher3;

	public BulletShooter[] launcherall; 
	public GameObject[] shooterall;

	public bool isIkuraMode = false;
	public int ikuramodecount = 0;
	bool gamestarted = false;
	int gamestartcount = 3;
	int shooternumber = 0;
	public int gametime = 5;
	public string playername;

	public void AddScore(int score){
		if (!gamestarted)
			return;

		totalscore += score;
		scoretext.text = "Score " + totalscore.ToString ();
	}

	public void AddSquidTime(){
		if(shooternumber + 1 < shooterall.Length)
			shooternumber++;
		shooterall [shooternumber].SetActive (true);
	}

	public void AddIkuraTime(){
		Debug.Log ("hogehoge");
		if (ikuramodecount >= 0) {
			ikuramodecount += 3;
		} else {
			ikuramodecount = 3;
		}
	}

	IEnumerator GetScore(){
		const string url = "http://toghurt.net/cgi-bin/tkch-shooter-ranking.cgi";
		WWW www = new WWW(url);
		yield return www;

		if (www.error == null) {
			//Debug.Log(www.text);
			resulttext.text = www.text;
		}
	}

	IEnumerator PostScore(string name, string score){
		const string url = "http://toghurt.net/cgi-bin/tkch-shooter-ranking.cgi";
		WWWForm form = new WWWForm ();
		form.AddField ("name", name);
		form.AddField ("score", score);
		WWW www = new WWW(url, form);
		yield return www;
		if (www.error == null) {
			resulttext.text = www.text;
			Debug.Log(www.text);
		}
	}

	public void GameStart(){
		playername = playernameinput.text;
		Debug.Log (playername);
		titlepanel.SetActive (false);
		countdowntext.text = "3";
		scoretext.text = "Score";
		StartCoroutine (ContinueGame ());
	}

	public void PostTwitter(){
		Application.OpenURL("http://twitter.com/intent/tweet?text=" +
		                    WWW.EscapeURL( playername + "=サンは寿司とか投げて" + totalscore + "点のスコアを獲得しました。おつかれ  " + "#寿司輪廻転生編"));
	}

	IEnumerator ContinueGame(){
		while(true){
			if(!gamestarted){
				if(gamestartcount > 0){
					countdowntext.text = gamestartcount.ToString();
					gamestartcount--;
				}else{
					gamestarted = true;
					countdowntext.text = string.Empty;
					for(int i=0;i < launcherall.Length ;i++)
					{
						launcherall[i].shootable = true;
					}
				}
			}else{
			//game shori
				if(ikuramodecount > 0){
					isIkuraMode = true;
				}else{
					isIkuraMode = false;
				}

				gametime--;
				ikuramodecount--;
				timelimit.text = gametime.ToString();

				if(gametime <= 0){
					//owari
					for(int i=0;i < launcherall.Length ;i++)
					{
						launcherall[i].shootable = false;
					}
					gamestarted = false;
					timelimit.text = string.Empty;
					StartCoroutine(ShowResult());
					yield break;
				}
			}
			yield return new WaitForSeconds (1.0f);
		}
	}

	IEnumerator ShowResult(){
		StartCoroutine(PostScore(playername,totalscore.ToString()));
		//StartCoroutine(GetScore());
		resultpanel.SetActive(true);

		yield break;
	}
}
