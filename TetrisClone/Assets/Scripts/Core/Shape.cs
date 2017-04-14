using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {

	public bool canRotate = true;

	void Move(Vector3 moveDirection){
		transform.position += moveDirection;
	}

	public void MoveLeft(){
		Move (new Vector3 (-1, 0, 0));
	}

	public void MoveRight(){
		Move (new Vector3(1, 0, 0));
	}

	public void MoveUp(){
		Move (new Vector3(0, 1, 0));
	}

	public void MoveDown(){
		Move (new Vector3(0, -1, 0));
	}

	public void RotateRight(){
		if (!canRotate) return;
		transform.Rotate (0, 0, -90);
	}

	public void RotateLeft(){
		if (!canRotate) return;

		transform.Rotate (0, 0, 90);
	}

	// Use this for initialization
	void Start () {
//		InvokeRepeating ("MoveDown", 0, 0.5f);
//		InvokeRepeating("RotateRight", 0, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
