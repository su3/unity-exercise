using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Shape[] allShapes;

	Shape GetRandomShape(){
		int i = Random.Range (0, allShapes.Length);

		if (allShapes [i]) {
			return allShapes [i];
		} else {
			Debug.Log ("WARNING! Invalid shape!");
			return null;
		}
	}

	public Shape SpawnShape(){
		Shape shape = null;
		shape = Instantiate (GetRandomShape(), transform.position, Quaternion.identity) as Shape;
		if (shape) {
			return shape;
		} else {
			Debug.Log ("WARNING! Invalid shape  in spawner!");
			return null;
		}
	}
}
