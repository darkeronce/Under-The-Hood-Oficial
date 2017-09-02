using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public class Corners{
		public float minX, maxX;
		public float minY, maxY;
	}

	public Transform target;
	public float speed;
	public Corners corners;
	public Vector2 distance;
	private float smoothSpeed;


	private void Start(){
	}

	private void LateUpdate(){
		Vector3 dir = new Vector3 (target.position.x - distance.x, target.position.y - distance.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, dir, speed * Time.deltaTime);
	}


}
