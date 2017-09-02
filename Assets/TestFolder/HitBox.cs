using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Basic Shield"){
			Debug.Log (other.name);
		}
	}
}
