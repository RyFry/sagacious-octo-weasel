using UnityEngine;
using UnityEngine.VR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameSynthesis.VR
{
	struct Sample
	{
		public float timestamp;
		public Quaternion orientation;
		public Vector3 eulerAngles;
		
		public Sample(float timestamp, Quaternion orientation)
		{
			this.timestamp = timestamp;
			this.orientation = orientation;
			
			eulerAngles = orientation.eulerAngles;
			eulerAngles.x = RiftGesture.WrapAngle(eulerAngles.x);
			eulerAngles.y = RiftGesture.WrapAngle(eulerAngles.y);
		}
	}
	
	public class RiftGesture : MonoBehaviour
	{
		public int sensor = 0;
		public AudioSource spitStart;
		public AudioSource spitEnd;
		public GameObject[] list;
		
		LinkedList<Sample> samples;
		float waitTime = 0f;
		const float detectInterval = 0.5f;

		bool beginSpit = false;
		
		public RiftGesture()
		{
			samples = new LinkedList<Sample>();
		}
		
		public void Update()
		{
			// Recode orientation
			Quaternion q = InputTracking.GetLocalRotation(VRNode.Head);
			
			samples.AddFirst(new Sample(Time.time, q));
			if (samples.Count >= 120) {
				samples.RemoveLast();
			}
			
			// Detect gestures
			if (waitTime > 0) {
				waitTime -= Time.deltaTime;
			} else {
				DetectNod();
				DetectHeadshake();
				DetectSpit();

			}
		}
		
		public void GetGraphEntries(out float[] timestamps, out Quaternion[] orientations)
		{
			int size = samples.Count;
			timestamps = new float[size];
			orientations = new Quaternion[size];
			
			int index = 0;
			foreach (var sample in samples) {
				timestamps[index] = sample.timestamp;
				orientations[index] = sample.orientation;
				index++;
			}
		}
		
		IEnumerable<Sample> Range(float startTime, float endTime)
		{
			return samples.Where(sample => (sample.timestamp < Time.time - startTime &&
			                                sample.timestamp >= Time.time - endTime));
		}
		
		void DetectNod()
		{
			try {
				float basePos = Range(0.2f, 0.4f).Average(sample => sample.eulerAngles.x);
				float xMax = Range(0.01f, 0.2f).Max(sample => sample.eulerAngles.x);
				float current = samples.First().eulerAngles.x;
				
				if (xMax - basePos > 8f &&
				    Mathf.Abs(current - basePos) < 5f) {
					print("nodded");
					waitTime = detectInterval;
				}
			} catch (InvalidOperationException) {
				// Range contains no entry
			}
		}
		
		void DetectHeadshake()
		{
			try {
				float basePos = Range(0.2f, 0.4f).Average(sample => sample.eulerAngles.y);
				float yMax = Range(0.01f, 0.2f).Max(sample => sample.eulerAngles.y);
				float yMin = Range(0.01f, 0.2f).Min(sample => sample.eulerAngles.y);
				float current = samples.First().eulerAngles.y;
				
				if ((yMax - basePos > 10f || basePos - yMin > 10f) &&
				    Mathf.Abs(current - basePos) < 5f) {
					print("shook head");
					waitTime = detectInterval;
				}
			} catch (InvalidOperationException) {
				// Range contains no entry
			}
		}

		void DetectSpit() {
			try {
				float basePos = Range (0.5f, 0.7f).Average (sample => sample.eulerAngles.x);
				float raisedPos = Range (0.3f, 0.5f).Average (sample => sample.eulerAngles.x);
				float spitLenght = Range (0.01f, 0.1f).Max (Sample => Sample.eulerAngles.x);
				if(basePos - raisedPos > 20f && !beginSpit) {
					//print ("Achhh");
					//spitStart.Play();
					beginSpit = true;
				}
				if(spitLenght - raisedPos > 22f)
				{
					print ("ptugh");
					spitEnd.Play ();
					waitTime = detectInterval;
					beginSpit = false;
				}
			} catch (InvalidOperationException) {
			}
		}

		public static float LinearMap(float value, float s0, float s1, float d0, float d1)
		{
			return d0 + (value - s0) * (d1 - d0) / (s1 - s0);
		}
		
		public static float WrapAngle(float degree)
		{
			if (degree > 180f) {
				return degree - 360f;
			}
			return degree;
		}

	}
}
