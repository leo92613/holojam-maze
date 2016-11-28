﻿using UnityEngine;
using System.Collections;

public class SpriteAimerComplex : MonoBehaviour {

	public int X;
	public int Y;
	int amount;
	int frame;
	float angle;
	Mesh mesh;

	Vector2[] nUV3;

	int I = 1;
	int J = 0;

	Vector2[] initUV;
	Vector2[] positions;
	float prevAngle;

	float ID;

	public bool getAngleFromObject;
	public GameObject container;
	public float rotationOffet = 0;

	public UVLookup uvLookup;

//	void Start(){
//		Init ();
//	}
//
//	void Update(){
//		UpdatePosition ();
//	}
	// Use this for initialization
	public void Init () {

		angle = 0.5f;
		ID = Random.value;
		amount = X * Y;
		mesh = GetComponent<MeshFilter>().mesh;
		initUV = new Vector2[mesh.uv.Length];

		Vector2[] nUV2 = new Vector2[mesh.uv.Length];
		for (int i = 0; i < nUV2.Length; i++) {
			nUV2 [i] = mesh.uv[i];
		}
		mesh.uv2 = nUV2;

		nUV3 = new Vector2[mesh.uv.Length];

		for (int i = 0; i < nUV2.Length; i++) {
			nUV3 [i] = new Vector2 (0, 0);
		}
		mesh.uv3 = nUV3;

		for (int i = 0; i < initUV.Length; i++) {
			initUV [i] = mesh.uv [i];
		}
		setupPositions ();
		SetFrame (angle);
	}
		
	// Update is called once per frame
	public void UpdatePosition () {
		Profiler.BeginSample("SpriteAimerComplex.UpdatePosition");
		float angle = (this.transform.eulerAngles.y-container.transform.eulerAngles.y) + rotationOffet;
		SetFrame (angle);
		SetAngle (angle);
		Aim ();
		Profiler.EndSample();
	}

	void Aim(){
		Profiler.BeginSample("SpriteAimerComplex.Aim");
		if (Camera.main) {
			transform.LookAt (Camera.main.transform.position);
			transform.localEulerAngles = Vector3.Scale (transform.localEulerAngles, Vector3.up); // Don't use new unless you have to.
//			transform.Rotate (0, 180, 0);
		}
		Profiler.EndSample();
	}

	void setupPositions(){
		positions = new Vector2[X * Y];
		for (int i = 0; i < positions.Length; i++) {
			positions [i] = new Vector2 (I, J);
			I++;
			if(I>X){
				I=1;
				J++;
				if(J>=Y){
					J=0;
				}
			}
		}
	}

	public void SetAngle(float degree){
		degree += 270;
		if (degree < 0)
			degree += 360;
		float pos = ((degree%360 / 360f));
		for (int i = 0; i < nUV3.Length; i++) {
			nUV3 [i].Set(pos,ID);
		}
		mesh.uv3 = nUV3;
	}

	public void SetFrame(float degree){
//		degree += 180;
		if (degree < 0)
			degree += 360;

		float deg = 360 - degree;
		
		float pos = Mathf.Floor ((1f-(deg % 360 / 360f)) * X*Y);
//		Vector2 px = positions [(int)Mathf.Min(positions.Length-1,positions.Length-(int)pos)];
//		Vector2[] nUV = new Vector2[mesh.uv.Length];
//		for (int i = 0; i < nUV.Length; i++) {
//			float x = (initUV [i].x / (float)X) + (1f-(px.x/(float)X));
//			float y = (initUV [i].y / (float)Y) + px.y/(float)Y;
//			nUV [i] = new Vector2 (x,y);
//		}
//		mesh.uv = nUV;
//		Debug.Log(uvLookup.UVList);
		mesh.uv = uvLookup.UVList [(int)Mathf.Min (positions.Length - 1, positions.Length - (int)pos)];
	}
}
