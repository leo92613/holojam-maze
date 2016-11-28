using UnityEngine;
using System.Collections;

public class ScoreUp : MonoBehaviour {

	public Score score;
	// Use this for initialization
	public void AddToScore(){
		score.AddToScore ();
	}
}
