using UnityEngine;
using System.Collections;

public class CameraController1 : MonoBehaviour {

	public Camera camera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButtonUp(KeyCode.A)) {
			Debug.Log ("test");
		}

	}
}
