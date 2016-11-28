using UnityEngine;
using System.Collections;

public class bulletCollision : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D c){
		Destroy (this.gameObject);
//		GameObject g = Instantiate (OnCollideObject,this.transform.position,Quaternion.identity) as GameObject;
//		g.GetComponent<AudioSource> ().pitch = Random.Range (.5f, 1.5f);
//		c.GetComponent<ScoreUp> ().AddToScore ();
	}

}
