using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServerInfoHandler : MonoBehaviour {

	public string GET_MESSAGES_URL = "http://test-server.com/getmessages.php?";
	public string SEND_MESSAGE_URL = "http://test-server.com/sendmessage.php?";
	public string REQUEST_URL;// = "http://test-server.com/script.php";

	private bool loadingComplete;

	#region Init
	private static ServerInfoHandler _instance;
	public static ServerInfoHandler Instance
	{
		get
		{
			return _instance;
		}
	}
	
	void Awake() {
		
		//first Singleton is created
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetMessagesWrapper (string userID) {
		StartCoroutine(GetMessages(userID));
	}

	IEnumerator GetMessages(string userID)
	{
		loadingComplete = false;
		REQUEST_URL = GET_MESSAGES_URL + "?userID=" + userID;
		WWW www = new WWW(REQUEST_URL);
		yield return www;
		// check for errors
		if (www.error == null)
		{
			string[] data = www.text.Split(':');
			List<string> tempList = new List<string>();
			for( int i=0; i<data.Length-1; i+=1 ) {
				if (tempList.Count < 3) {
					tempList.Add( data[i] );
				} else {
					MessageManager.Instance.AddMessageToUnreadList(MessageManager.Instance.CreateNewAppMessage(tempList[0], tempList[1], tempList[2]));
					tempList.Clear();
					tempList.Add( data[i] );
				}
			}
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}
		loadingComplete = true;
	}

	public void SendMessageWrapper (Message message) {
		StartCoroutine(SendMessage(message));
	}

	IEnumerator SendMessage (Message message) {
		string senderID = message.senderID;
		string receiverID = message.receiverID;
		string messageText = message.message;
		REQUEST_URL = SEND_MESSAGE_URL + "?senderID=" + senderID + "&receiverID=" + receiverID + "&messageText=" + messageText;
		WWW www = new WWW(REQUEST_URL);
		yield return www;
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
			MessageManager.Instance.RemoveMessageFromUnsentList(message);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}   
	}

	public bool LoadingComplete {
		get {
			return this.loadingComplete;
		}
		set {
			loadingComplete = value;
		}
	}
}
