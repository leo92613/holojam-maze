using UnityEngine;
using System.Collections;
	
public class verticesToGameObjects : MonoBehaviour {

	public Transform pole;

	void Start() {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
		Transform[] poles = new Transform[vertices.Length];
		int i = 0;
		while (i < vertices.Length) {
			Transform p = Instantiate(pole,vertices[i]*20f,new Quaternion(Random.Range (-10,10),Random.Range(-180,180),0,Random.Range(-180,180))) as Transform;
			poles[i] = p;
			if(i>0)
				poles[i].LookAt(poles[i-1].position);
//			vertices[i] += normals[i] * Mathf.Sin(Time.time);
			i++;
		}
		mesh.vertices = vertices;
	}
}