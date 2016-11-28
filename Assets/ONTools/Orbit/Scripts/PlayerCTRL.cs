using UnityEngine;
using System.Collections;

namespace orbit{
	public class PlayerCTRL : MonoBehaviour {

		Rigidbody2D body;
		public float multiplier;
		public float rotateSpeed;
		public float gravityAmount;
		public float squirtMultiply;
		Vector3 right;
		Vector3 left;
		public bool useCTRL;
		// Use this for initialization
		void Start () {
			body = GetComponent<Rigidbody2D> ();
			right = new Vector3 (0, 0, 1);
			left = new Vector3 (0, 0, -1);
		}
		
		// Update is called once per frame
		void Update () {
			if(useCTRL)
				CTRL ();
			ApplyGravity ();
		}

		void CTRL(){
			if (Input.GetKey (KeyCode.A)) {
				body.AddTorque (rotateSpeed);
			}
			if (Input.GetKey (KeyCode.W)) {
				body.AddForce (this.transform.up*multiplier);

			}
			if (Input.GetKey (KeyCode.D)) {
				body.AddTorque (-rotateSpeed);
//				this.transform.Rotate (right*rotateSpeed);

			}
			if (Input.GetKeyDown (KeyCode.S)) {
				GameObject g = Instantiate (this.gameObject, this.transform.position + this.transform.up*2, this.transform.rotation) as GameObject;
				g.transform.localScale = Vector3.one * .3f;
				g.GetComponent<Rigidbody2D> ().AddForce (this.transform.up * multiplier * squirtMultiply);
				g.GetComponent<PlayerCTRL> ().useCTRL = false;
				g.AddComponent<bulletCollision> ();

			}
		}

		void ApplyGravity(){

			Vector3 force = Vector3.zero - this.transform.position;
				body.AddForce ((force*gravityAmount)/Mathf.Max(.0001f,this.transform.position.magnitude));
		}
	}
}
