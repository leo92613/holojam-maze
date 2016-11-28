using UnityEngine;
using System.Collections;

public class polesMaterialAssign : MonoBehaviour {

	private ArrayList woodParts = new ArrayList();
	private ArrayList poles = new ArrayList();

	public Material mat;
	// Use this for initialization
	void Start () {

		Traverse (transform,"wood",woodParts);
		Traverse (transform, "poleGeo", poles);

		foreach (Transform item in woodParts){applyMat (item);}
		foreach (Transform item in poles){item.gameObject.GetComponent<Renderer>().material = mat;}
	}

	void Traverse(Transform obj, string name, ArrayList arr) { 
		foreach (Transform child in obj) {
			if(child.name.Contains (name)){
				arr.Add(child);
			}
			Traverse(child,name,arr); 
		}
		
	}

	void applyMat(Transform obj) { 
		foreach (Transform child in obj) {
			child.gameObject.GetComponent<Renderer>().material = mat;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
