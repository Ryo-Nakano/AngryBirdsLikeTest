using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour {

	float power = 0.05f;//ボールに加える速度ベクトルがあまりにも大きくなりすぎないように調整

	Rigidbody r;//GetComponentしたRigidbodyを入れておく為の変数
	Vector3 ballVec;//ボールが飛んで方向

	Vector3 clickPosDown;//マウス押し込んだ位置
	Vector3 clickPosUp;//マウス離した位置
	float distance;//2点間の距離
	float distanceMin = 50;//飛ばす最小値


	// Use this for initialization
	void Start () {
		r = this.gameObject.GetComponent<Rigidbody> ();
		r.useGravity = false;//ゲーム開始と同時に重力無効化
	}
	
	// Update is called once per frame
	void Update () {
		Test ();//引っ張った距離に応じてballを飛ばす
	}


	//引っ張ってボール飛ばす為の関数
	void Test()
	{
		//マウス押し込んだ時
		if(Input.GetMouseButtonDown(0) == true)
		{
			clickPosDown = Input.mousePosition;//マウス押し込んだ瞬間のマウスの位置座標を変数"clickPosDown"に格納
		}

		//マウス離した時
		if(Input.GetMouseButtonUp(0) == true)
		{
			clickPosUp = Input.mousePosition;//マウス離した瞬間のマウスの位置座標を変数"clickPosUp"に格納

			distance = (clickPosUp - clickPosDown).magnitude;//2点間距離を求める
			Debug.Log(distance);

			//マウス押し込んだ座標と離した座標があまりにも近すぎる場合→処理抜ける(先に進まない)
			if (distance < distanceMin) {
				return;
			}
				
			//ボールを飛ばす方向を計算！
			ballVec = (clickPosDown - clickPosUp);//ボールが飛んで行く方向ベクトルを出す→変数"ballVec"に格納

			//実際に飛ばす
			r.velocity = ballVec.normalized * power * distance;//ballのvelocityに直接速度ベクトル代入する！
			r.useGravity = true;//重力をONに
		}
	}
}
