using UnityEngine;
using System.Collections;

public class makeMoths : MonoBehaviour {

	public GameObject moth;			

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 100; i++) {
			GameObject m = Instantiate(moth,new Vector3(Random.Range(-100f,100f),Random.Range(0f,10f),Random.Range(-100f,100f)),Quaternion.identity) as GameObject;
			m.transform.parent = GameObject.Find("scene_01/poles").transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
