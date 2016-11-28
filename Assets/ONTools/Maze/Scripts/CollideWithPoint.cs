using UnityEngine;
using System.Collections;

public class CollideWithPoint : MonoBehaviour {

	public GameObject OnCollideObject;
	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}

	void OnTriggerEnter (Collider c){
		Destroy (this.gameObject);
		GameObject g = Instantiate (OnCollideObject,this.transform.position,Quaternion.identity) as GameObject;
		g.GetComponent<AudioSource> ().pitch = Random.Range (.5f, 1.5f);
		c.GetComponent<ScoreUp> ().AddToScore ();
	}
}
