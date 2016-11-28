using UnityEngine;
using System.Collections;

public class ForceFromKeyboard2 : MonoBehaviour {

	public float multiplier;
	Rigidbody body;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!body.useGravity) {
			if (Input.anyKey)
				TriggerStart ();
		}
		if (Input.GetKey (KeyCode.J)) {
			body.AddForce (Vector3.left*multiplier);
		}
		if (Input.GetKey (KeyCode.I)) {
			body.AddForce (Vector3.forward*multiplier);
		}
		if (Input.GetKey (KeyCode.L)) {
			body.AddForce (Vector3.right*multiplier);
		}
		if (Input.GetKey (KeyCode.K)) {
			body.AddForce (Vector3.back*multiplier);
		}
	}

	void TriggerStart(){
		if (!body.useGravity)
			body.useGravity = true;
	}
}
