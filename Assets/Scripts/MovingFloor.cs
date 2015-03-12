using UnityEngine;
using System.Collections;

public class MovingFloor : MonoBehaviour {

	public Vector3 speed    = Vector3.zero; //1フレームで動く距離(マイナスは逆方向)
	public Vector3 distance = Vector3.zero; //この距離まで動く
	
	//distanceまで動いた後に反対方向へ折り返して動くか？
	//falseだとdistanceまで動いたらそこで止る
	public bool turn = true;
	
	private Vector3 moved = Vector3.zero; //移動した距離を保持
	private List ride = new List();//床に乗ってるオブジェクト
	
	void Update() 
	{
		//床を動かす
		float x = speed.x;
		float y = speed.y;
		float z = speed.z;
		if (moved.x >= distance.x) x = 0;
		else if (moved.x + speed.x > distance.x) x = distance.x - moved.x;
		if (moved.y >= distance.y) y = 0;
		else if (moved.y + speed.y > distance.y) y = distance.y - moved.y;
		if (moved.z >= distance.z) z = 0;
		else if (moved.z + speed.z > distance.z) z = distance.z - moved.z;
		transform.Translate(x, y, z);
		//動いた距離を保存
		moved.x += Mathf.Abs(speed.x);
		moved.y += Mathf.Abs(speed.y);
		moved.z += Mathf.Abs(speed.z);
		
		//床の上のオブジェクトを床と連動して動かす
		foreach (GameObject g in ride) 
		{
			Vector2 v = g.transform.position;
			g.transform.position = new Vector3(v.x + x, v.y + y);	//yの移動は不要////////////
		}
		//折り返すか？
		if (moved.x >= distance.x && moved.y >= distance.y && moved.z >= distance.z && turn) {
			speed *= -1; //逆方向へ動かす
			moved = Vector3.zero;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		//床の上に乗ったオブジェクトを保存
		ride.Add(other.gameObject);
		//Debug.LogError ("ride");
	}
	
	void OnTriggerExit2D(Collider2D other) {
		//床から離れたので削除
		ride.Remove(other.gameObject);
	}
}
