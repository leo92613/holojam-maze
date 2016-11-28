using UnityEngine;
using System.Collections;

namespace FishSchool{
	public class Fish : MonoBehaviour {

		public SpriteAimerComplex spriteAimer;
		private Vector3 prevPosition;
		public Vector3 target;
		public Vector3 origin;
		public bool lerpRotation = false;
		private Vector3 scale;
		
		Quaternion prev = Quaternion.identity;

		public void UpdatePosition(float fishScale){
		
			Profiler.BeginSample("Fish.UpdatePosition");
		
			this.transform.position = target;

			if(lerpRotation)
				prev = this.transform.rotation;
			
			this.transform.LookAt (prevPosition);

			if(lerpRotation){
				Quaternion next = this.transform.rotation;
				this.transform.rotation = Quaternion.Lerp(prev,next,.1f);
			}

			scale.Set(fishScale, fishScale, fishScale);
			this.transform.localScale = scale;
			this.spriteAimer.UpdatePosition ();
			prevPosition = target;
			
			Profiler.EndSample();
		}

	}
}
