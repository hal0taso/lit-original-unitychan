using UnityEngine;
using System.Collections;

public class CameraController1 : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	

		
	
		if(Input.GetKeyUp(KeyCode.K)) {
		//Debug.Log ("test");
		transform.Rotate( 0, 90, 0);}

		if(Input.GetKeyUp(KeyCode.L)) 	{
		//Debug.Log ("test");
		transform.Rotate(0, -90, 0);}

	}
}
