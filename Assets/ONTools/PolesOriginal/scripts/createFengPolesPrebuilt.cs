using UnityEngine;
using System.Collections;

public class createFengPolesPrebuilt : MonoBehaviour {


	void Start () {

		GameObject[] lights = GameObject.FindGameObjectsWithTag ("glass");

		ArrayList polesWithLights = new ArrayList();

		for(int i = 0 ; i < lights.Length ; i++){
			if(lights[i].transform.parent.transform.localScale.x > .5)
				polesWithLights.Add(lights[i].transform.parent.transform.parent);
		}

//		Transform[] pwLights = new Transform[count];
//
//		int q = 0;
//
//		for(int i = 0 ; i < lights.Length ; i++){
//			if(lights[i].transform.parent.transform.localScale.x < .5){
//				pwLights[q] = lights[i].transform.parent.transform.parent;
//				q++;
//				print (lights[i].transform.parent.transform.parent);
//			}
//		}

		GameObject.Find("makeMoths").SendMessage("drawAnim",polesWithLights);
	
	}

}
