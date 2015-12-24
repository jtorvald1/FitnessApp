using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour {

	public string messageID;
	public string senderID;
	public string receiverID;
	public string message;
	public bool read;


	public Message (string senderID, string receiverID, string message)
	{
		this.senderID = senderID;
		this.receiverID = receiverID;
		this.message = message;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
