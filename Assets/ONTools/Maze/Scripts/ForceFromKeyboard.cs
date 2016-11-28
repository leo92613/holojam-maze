using UnityEngine;
using System.Collections;

public class ForceFromKeyboard : MonoBehaviour {

	public float multiplier;
	public float jumpMultiplier;
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
		if (Input.GetKey (KeyCode.A)) {
			body.AddForce (Vector3.left*multiplier);
		}
		if (Input.GetKey (KeyCode.W)) {
			body.AddForce (Vector3.forward*multiplier);
		}
		if (Input.GetKey (KeyCode.D)) {
			body.AddForce (Vector3.right*multiplier);
		}
		if (Input.GetKey (KeyCode.S)) {
			body.AddForce (Vector3.back*multiplier);
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			body.AddForce (Vector3.up*multiplier*jumpMultiplier);
		}
	}

	void TriggerStart(){
		if (!body.useGravity)
			body.useGravity = true;
	}
}
