using UnityEngine;
using System.Collections;

public class Charcter : MonoBehaviour {
	
	public	float	MoveSpeed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// 自分のオブジェクトだけ操作できるようにする
		if(true == networkView.isMine)
		{
			Vector3 pos = gameObject.transform.position;
			if(Input.GetKey(KeyCode.UpArrow))
			{
				this.gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z + (MoveSpeed * Time.deltaTime));
			}
			else if(Input.GetKey(KeyCode.DownArrow))
			{
				this.gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z - (MoveSpeed * Time.deltaTime));
			}
			else if(Input.GetKey(KeyCode.RightArrow))
			{
				this.gameObject.transform.position = new Vector3(pos.x + (MoveSpeed * Time.deltaTime), pos.y, pos.z);
			}
			else if(Input.GetKey(KeyCode.LeftArrow))
			{
				this.gameObject.transform.position = new Vector3(pos.x - (MoveSpeed * Time.deltaTime), pos.y, pos.z);
			}

			if (Input.touchCount > 0)
			{
				networkView.RPC("Touch", RPCMode.All);
			}

			if (Input.GetMouseButtonDown(0))
			{
				networkView.RPC("Touch", RPCMode.All);
			}
		}
	}

	[RPC]
	void Touch()
	{
		var pos = transform.position;
		transform.position = new Vector3(pos.x, 5, pos.z);
	}
}
