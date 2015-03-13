using UnityEngine;
using System.Collections;

public class FallingFloor : MonoBehaviour {
	public GameObject fallingfloor;

	// Use this for initialization
	void Start () {
		collider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {


	
	}
	void OnTriggerEnter(Collider other){
		// このコードを適用したgameObjectを3秒掛けて(12, -4, 7)の位置まで移動させる
		iTween.MoveTo(fallingfloor, new Vector3(13, -4, 7), 3.0f);
	}
}
