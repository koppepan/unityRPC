using UnityEngine;
using System.Collections;

public class NetView : MonoBehaviour {
	
	public 		GameObject 	prefub;				// 生成したいprefab
	
	public 		string 		ip = "127.0.0.1";	// IPアドレス
	private 	string 		port = "5000";		// port番号
	
	private 	bool 		connected = false;	// 接続されているか
	
	void Start()
	{
		// 秒間何回通信するかの設定
		//Network.sendRate = 60;
	}

	void Update()
	{
	}
		
	
	// purefabを実体化
	private void CreatePlayer()
	{
		connected = true;
		// 接続されているネットワーク内すべてに生成される
		Network.Instantiate(prefub, transform.position, transform.rotation, 1);
	}
	
	// サーバーとの接続が解除された(client)
	public void OnDisconnectedFromServer()
	{	
		Application.Quit();
	}
	
	// クライアントが切断された(host) 
	public void OnPlayerDisconnected(NetworkPlayer pl)
	{	
		Network.DestroyPlayerObjects(pl);
	}
	
	
	// サーバーに接続(client)
	public void OnConnectedToServer()
	{
		CreatePlayer();
	}
	// サーバー立ち上げ(host)
	public void OnServerInitialized()
	{
		CreatePlayer();
	}
	
	public void OnGUI()
	{
		if(!connected)
		{
			// IP,Port表示(入力もできるよ！)
			ip = GUI.TextField( new Rect(10,10,90,20),ip);
			port = GUI.TextField( new Rect(10, 40, 90, 20), port);
			
			// コネクトボタン、サーバーに接続
			if( GUI.Button( new Rect( 10,70,90, 20), "Connect"))
			{
				Network.Connect(ip, int.Parse(port) );
			}
			// ホストボタン、サーバー立ち上げ
			if( GUI.Button( new Rect(10, 100, 90, 20), "host"))
			{	
				Network.InitializeServer(10, int.Parse(port), true);
			}
		}
	}
}
