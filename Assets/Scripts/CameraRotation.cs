using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {
	
	public GameObject cube;
	int Camera_Controller = 90;
	
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown("a")) {
			Camera_Controller += 90;
			Camera_Controller %= 180;
			Debug.Log(Camera_Controller);
			iTween.RotateTo(cube, iTween.Hash("y", Camera_Controller, "time", 0.5f));
		}
		if (Input.GetKeyDown("d")) {
			Camera_Controller -= 90;
			Camera_Controller %= 180;
			Debug.Log(Camera_Controller);
			iTween.RotateTo(cube, iTween.Hash("y", Camera_Controller, "time", 0.5f));
		}
		
	}
}
