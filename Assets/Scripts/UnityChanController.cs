using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]


public class UnityChanController : MonoBehaviour {
	
	private Animator animator;// キャラにアタッチされるアニメーターへの参照
	private AnimatorStateInfo currentBaseState;			// base layerで使われる、アニメーターの現在の状態の参照
	private int doWalkId;
	private int doWalkJump;
	public float forwardSpeed = 2.0f;
	public float backwardSpeed = 2.0f;
	public float jumpPower = 2.0f; 
	private Vector3 velocity;

	public GameObject Cube;

	public bool useCurves = true;				// Mecanimでカーブ調整を使うか設定する
	// このスイッチが入っていないとカーブは使われない
	public float useCurvesHeight = 0.5f;		// カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）
	
	private Rigidbody rb;
	private CapsuleCollider col;

	private float orgColHight;
	private Vector3 orgVectColCenter;
	static int jumpState = Animator.StringToHash("Base Layer.Jump");
	private int JumpCounter = 0;
	
	// Use this for initialization
	void Start () {


		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();

		animator = GetComponent<Animator> ();
		doWalkId = Animator.StringToHash ("Do Walk");

		animator = GetComponent<Animator> ();
		doWalkJump = Animator.StringToHash ("Do Jump");

		orgColHight = col.height;
		orgVectColCenter = col.center;
	}

	void OnCollidionExit(Collider Cube){
		JumpCounter += 1;
	}

	void OnCollidionEnter(Collider Cube){
		JumpCounter = 0;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			animator.SetBool (doWalkId, true);

		}else if (Input.GetKey(KeyCode.LeftArrow)) {
				animator.SetBool(doWalkId, true);
		}else{
			animator.SetBool(doWalkId, false);
		}

		

		if (Input.GetKey(KeyCode.Space)) {
			animator.SetBool(doWalkJump, true);
			//rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
			if (JumpCounter == 0) {
				rb.AddForce (Vector3.up * jumpPower, ForceMode.VelocityChange);
				//iTween.MoveTo(gameObject, iTween.Hash("y", 3, "time", 1.0f, "easetype", iTween.EaseType.easeOutCubic));
				
			}
		}else{
			animator.SetBool(doWalkJump, false);
		}


	}

	void FixedUpdate () {
		// 以下、キャラクターの移動処理
		velocity = new Vector3(0, 0, (Input.GetAxis("Horizontal")));		// 上下のキー入力からZ軸方向の移動量を取得
		// キャラクターのローカル空間での方向に変換
		velocity = transform.TransformDirection(velocity);
		//以下のvの閾値は、Mecanim側のトランジションと一緒に調整する
		if (Input.GetAxis("Horizontal") > 0.1) {
			velocity *= forwardSpeed;		// 移動速度を掛ける
		} else if (Input.GetAxis("Horizontal") < -0.1) {
			velocity *= backwardSpeed;	// 移動速度を掛ける
		}

		transform.localPosition += velocity * Time.fixedDeltaTime;


		rb.useGravity = true;

		if(currentBaseState.nameHash == jumpState)
		{
			// ステートがトランジション中でない場合
			if(!animator.IsInTransition(0))
			{
				
				// 以下、カーブ調整をする場合の処理
				if(useCurves){
					// 以下JUMP00アニメーションについているカーブJumpHeightとGravityControl
					// JumpHeight:JUMP00でのジャンプの高さ（0〜1）
					// GravityControl:1⇒ジャンプ中（重力無効）、0⇒重力有効
					float jumpHeight = animator.GetFloat("JumpHeight");
					//float gravityControl = animator.GetFloat("GravityControl"); 
					//if(gravityControl > 0)
						//rb.useGravity = false;	//ジャンプ中の重力の影響を切る
					
					// レイキャストをキャラクターのセンターから落とす
					Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
					RaycastHit hitInfo = new RaycastHit();
					// 高さが useCurvesHeight 以上ある時のみ、コライダーの高さと中心をJUMP00アニメーションについているカーブで調整する
					if (Physics.Raycast(ray, out hitInfo))
					{
						if (hitInfo.distance > useCurvesHeight)
						{
							col.height = orgColHight - jumpHeight;			// 調整されたコライダーの高さ
							float adjCenterY = orgVectColCenter.y + jumpHeight;
							col.center = new Vector3(0, adjCenterY, 0);	// 調整されたコライダーのセンター
						}
						else{
							// 閾値よりも低い時には初期値に戻す（念のため）					
							resetCollider();
						}
					}
				}
				// Jump bool値をリセットする（ループしないようにする）				
				animator.SetBool("Jump", false);
			}
		}

}
	void resetCollider()
	{
		// コンポーネントのHeight、Centerの初期値を戻す
		col.height = orgColHight;
		col.center = orgVectColCenter;
	}

}