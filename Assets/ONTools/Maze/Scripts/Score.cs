using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public int score = 0;
	public int wins = 0;
	public TextMesh txt;
	public int MaxScore;
	public Maze.MazeBuilder maze;

	//crappy hacky
	public Score otherScore;

	public void ResetScore(){
		score = 0;
		MaxScore = (int)((maze.divisions.x * maze.divisions.x) / 2);
		txt.text = "0";
	}
	public void AddToScore(){
		score++;
		txt.text = score+"\n"+wins;
		if (score > MaxScore) {
			maze.RebuildMaze ();
			MaxScore = (int)((maze.divisions.x * maze.divisions.x) / 2);
			wins++;
			otherScore.ResetScore ();
			ResetScore ();

		}
	}

}
