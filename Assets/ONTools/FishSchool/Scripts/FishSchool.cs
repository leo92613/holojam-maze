using UnityEngine;
using System.Collections;
namespace FishSchool{

	public class FishSchool : MonoBehaviour {

		public GameObject fishPrefab;
		Fish[] fishes;

		public GameObject schoolAnimator;

		Vector3[] targets;
	//	Vector3[] prevPosition;

	//	GameObject fishParent;
		public float fishScale = 1;
		
		public float inMin;
		public float inMax;
		public float min;
		public float max;
			
		public bool activated = false;

		public UVLookup uvLookup;
	//	public FillWithRandomObjects fill;

		private bool initialized = false;

		public float spread;
		public int amount;

		public bool useNoiseBehavior;

		ImageTools.Core.PerlinNoise PNoise;

		public bool spawnFishes = true;

		float noiseCounter = 0;

		public float noiseSpeed = 1f;
		public float noiseScale = 2.2f;
		public float noiseWorldScale = .1f;

		//
		Vector3 nextPosition;
		Vector3 noisePosition;
		

		// Use this for initialization
		void Start () {

			PNoise = new ImageTools.Core.PerlinNoise (1);
			fishes = new Fish[amount];
			uvLookup.Init ();
	//		prevPosition = new Vector3[amount];
	//		fishParent = new GameObject ();
	//		fishParent.name = "FishSpriteParent";
			if(spawnFishes)
				FillFishes ();

		}

		//todo: make extension method
		Vector3 GetNoiseVec(Vector3 vec, float freq){
			vec *= freq;
			return new Vector3 (
				.5f-Mathf.PerlinNoise(vec.x,vec.z),
				.5f-Mathf.PerlinNoise(vec.y,vec.x),
				.5f-Mathf.PerlinNoise(vec.z,vec.y)
			);
		}

		void FillFishes() {
			for (int i = 0; i < amount; i++) {

				Vector3 fishPosition = Vector3.Scale( GetNoiseVec(Random.insideUnitSphere,2f),spread*this.transform.localScale);
				GameObject fishInstance = Instantiate (fishPrefab,fishPosition,Quaternion.identity,this.transform) as GameObject;
				fishInstance.name = "Fish_" + i;
				fishInstance.transform.GetChild(0).gameObject.GetComponent<SpriteAimerComplex>().enabled=false;
				Fish fishScript = fishInstance.GetComponent<Fish> ();
				fishScript.target = fishPosition;
				fishScript.origin = fishPosition;
				fishScript.spriteAimer.uvLookup = uvLookup;
				fishScript.spriteAimer.Init ();
				fishInstance.SetActive (false);
				fishes [i] = fishScript;
				fishes [i].enabled = false;

	//			yield return null;
			}
			initialized = true;
		}
		
		// Update is called once per frame
		void Update () {
		
			if (initialized) {
				if (activated && fishScale > 0) {
					for (int i = 0; i < amount; i++) {
						nextPosition = schoolAnimator.transform.localToWorldMatrix.MultiplyVector (fishes [i].origin) + schoolAnimator.transform.position;
						if (useNoiseBehavior) {
							float off = .5f;
							float scale = noiseScale*.01f;
							float wScale = noiseWorldScale;
							noiseCounter += noiseSpeed * Time.deltaTime;
							noisePosition.Set (
								scale * (float)PNoise.Noise (wScale * nextPosition.x + noiseCounter + off, noiseCounter + nextPosition.y * wScale, noiseCounter + wScale * nextPosition.z),
								scale * (float)PNoise.Noise (wScale * nextPosition.x + noiseCounter, wScale * nextPosition.y + noiseCounter + off, noiseCounter + wScale * nextPosition.z),
								scale * (float)PNoise.Noise (wScale * nextPosition.x + noiseCounter, wScale * nextPosition.y + noiseCounter, noiseCounter + wScale * nextPosition.z + off));
							
							fishes [i].target = nextPosition + noisePosition;
	//					fishes [i].transform.LookAt (prevPosition [i]);
	//					fishes [i].transform.Rotate (0, 90, 0);
	//					fishes [i].transform.localScale = new Vector3 (fishScale, fishScale, fishScale);
	//					fishes [i].spriteAimer.UpdatePosition ();
	//					prevPosition [i] = fishes[i].transform.position;
						} else
							fishes [i].target = nextPosition;
						
						fishes [i].UpdatePosition (fishScale);
					}
				} else if (fishScale <= 0 && activated) {
					for (int i = 0; i < this.transform.childCount; i++) {
						fishes [i].gameObject.SetActive (false);

						fishes [i].enabled = false;// .gameObject.SetActive (false);
						fishes[i].transform.GetChild(0).gameObject.GetComponent<SpriteAimerComplex>().enabled=false;
					}
					activated = false;
				} else if (!activated && fishScale > 0) {
					for (int i = 0; i < this.transform.childCount; i++) {
						fishes [i].gameObject.SetActive (true);
						fishes [i].enabled = true;// .gameObject.SetActive (false);
						fishes[i].transform.GetChild(0).gameObject.GetComponent<SpriteAimerComplex>().enabled=true;
						fishes [i].UpdatePosition (fishScale);
					}
					activated = true;
				}
			}
		}


		// Set Scale

		public void SetScale(float value){
			fishScale = Mathf.Max(0.0f,map (value, inMin, inMax, min, max));
		}

		float map(float s, float a1, float a2, float b1, float b2)
		{
			return b1 + (s-a1)*(b2-b1)/(a2-a1);
		}

	}
}
