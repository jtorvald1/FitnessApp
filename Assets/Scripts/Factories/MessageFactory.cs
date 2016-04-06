using UnityEngine;
using System.Collections;

public class MessageFactory : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Message CreateMessage(string senderID, string receiverID, string messageText) {
		GameObject go = new GameObject();
		Message message = go.AddComponent<Message> ();
		message.senderID = senderID;
		message.receiverID = receiverID;
		message.message = messageText;
		return message;
	}
}
