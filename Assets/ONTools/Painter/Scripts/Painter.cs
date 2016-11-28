using UnityEngine;
using System.Collections;

public class Painter : MonoBehaviour {

	public Texture2D tex;
	public GameObject spriteParent;
	public bool building;
	public int amount = 100;
	public float minSize = 0;
	public float maxSize = .1f;
	public float scale;

	public Color colorRotation;
	public Color colorScale;
	public Color reColor;

	public float spriteAlpha = 1f;
	public bool invertScale = false;

	// Use this for initialization
	void Start () {

	}

	void SetupTex(){

		float aspect = (float)tex.width / (float)tex.height;

		Debug.Log (aspect);
		for (int i = 0; i < amount; i++) {
			Vector2 coord = new Vector2 (Random.Range (0, tex.width), (int)Random.Range (0, tex.height));

			Color col = tex.GetPixel ((int)coord.x, (int)coord.y);
			GameObject thisSprite = spriteParent.transform.GetChild ((int)Random.Range (0, spriteParent.transform.childCount-1)).gameObject;
			GameObject sp = Instantiate (thisSprite);
			sp.transform.localPosition = new Vector3 ((coord.x/tex.width) * scale * aspect, (coord.y/tex.height) * scale, i * .0001f);

			float realScale = 1;
//
			if (invertScale) {
				realScale = Random.Range (minSize, maxSize) * (colorScale.r * col.r + colorScale.g * col.g + colorScale.b * col.b);
			}
			else{
				realScale = Random.Range (minSize, maxSize) * (((colorScale.r * (1-col.r))) + ((colorScale.g * (1-col.g))) + ((colorScale.b * (1-col.b))));
			}
			sp.transform.localScale = new Vector3 (realScale, realScale, realScale);

			sp.transform.localEulerAngles = new Vector3 (0, 0,((colorRotation.r*col.r+colorRotation.g*col.g+colorRotation.b*col.b)* -360));

			sp.GetComponent<SpriteRenderer> ().color = Color.Lerp( 
				new Color(col.r,col.g,col.b,spriteAlpha), 
				new Color(reColor.r,reColor.g,reColor.b,spriteAlpha), reColor.a);
			
			sp.GetComponent<SpriteRenderer>().sortingOrder = i;

		}
	}

	void Update () {
		if (building) {
			SetupTex ();
			building = false;
		}
	}
}
