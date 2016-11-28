using UnityEngine;
using System.Collections;

public class createPoles : MonoBehaviour {

	public Transform light;
	public GameObject lampLight;
	public Transform pole;
	public int poleAmount;
	public Transform polePositions;
	public float lightPercent = 30;
	Transform[] poles; 
	public Transform line;
	Transform[] lines;

	int volumeUp = 0;

	// Use this for initialization
	void Start () {

//		audio.volume = 0;

		float posX = 0f;
		float posY = 0f;
		float rotY = 0f;

		int i = 1;
		Transform[] poleRoots;
		poleRoots = polePositions.GetComponentsInChildren<Transform>();

		if (poleRoots.Length > 1)
			poleAmount = poleRoots.Length;

		poles = new Transform[poleAmount];
		lines = new Transform[poleAmount*4];
		ArrayList polesWithLights = new ArrayList();

		while (i < poleAmount) {

			if(poleRoots.Length>1)
				poles[i] = Instantiate(pole, poleRoots[i].position, poleRoots[i].rotation) as Transform;
			else
				poles[i] = Instantiate(pole, new Vector3(posX, 0, posY), new Quaternion(Random.Range (-10,10),rotY,0,Random.Range(-180,180))) as Transform;

//			poles[i].GetComponent<switchLevel>().fader = this.fader;


			posX-=Random.Range (-50,50);
			posY+=Random.Range (-17,30);
			rotY+=Random.Range (-180,180);

			GameObject lamp = poles[i].Find("lamp").gameObject;
			GameObject bulb = poles[i].Find("lamp/bulb").gameObject;

			bool makeLight = false;

			if(poleRoots.Length>1){
				if(poleRoots[i].localScale.y > 20)
					makeLight = true;
			}
			else{
				if(Random.Range(0,100)>lightPercent){
					makeLight=true;
				}
			}

			if(!makeLight){
				lamp.transform.localScale = new Vector3(.01f,.01f,.01f);
				bulb.GetComponent<AudioSource>().volume = 0;
			}
			else{
				polesWithLights.Add(poles[i]);
				bulb.GetComponent<AudioSource>().pitch = Random.Range(.5f,2f);
				bulb.GetComponent<AudioSource>().volume = 0;
				Transform lgt = Instantiate(light, bulb.transform.position, Quaternion.identity) as Transform;
				lgt.transform.Rotate(new Vector3(90,0,0));
				GameObject lampLgt = Instantiate(lampLight, bulb.transform.position, Quaternion.identity) as GameObject;
//				lampLgt.transform.Rotate(new Vector3(180f,0f,0f));
//				lampLgt.transform.Translate(new Vector3(0,-.21f,0));

			}

			if(i>1){

				Transform[] tLines = new Transform[4];

				for(int j = 0 ; j < 4 ; j++){

					int which = j+1;

					GameObject hook = poles[i].Find("decor/hooks/hook_"+which).gameObject;
					Vector3 hookPos = hook.transform.position;
//					print (hookPos);

					tLines[j] = Instantiate(line, new Vector3(0,0,0), Quaternion.identity) as Transform;

					GameObject lineRoot = tLines[j].Find("wire/line_1/aim").gameObject;
					GameObject lineTarget = tLines[j].Find("wire/line_1/aim/target").gameObject;
					
					lineRoot.transform.position = hook.transform.position;


					string dir = "decor/hooks/hook_"+which;
//					print (dir);
					GameObject prevHook = poles[i-1].Find(dir).gameObject;
					lineRoot.transform.LookAt(prevHook.transform.position);
					float dist = Vector3.Distance(lineRoot.transform.position,prevHook.transform.position);
//					print(dist);
					lineTarget.transform.localPosition = new Vector3(0,0,dist);
				}

			}

			i++;
		}

		GameObject.Find("makeMoths").SendMessage("drawAnim",polesWithLights);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (volumeUp < 100) {
			for(int i = 1 ; i < poles.Length ;i++){

				GameObject bulb = poles [i].Find ("lamp/bulb").gameObject;

				if (bulb.GetComponent<AudioSource>().volume < 1f)
					bulb.GetComponent<AudioSource> ().volume += .01f;

				volumeUp++;
			}
		}
	
	}
}
