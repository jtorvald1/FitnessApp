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
		Message message = new Message(senderID, receiverID, messageText);
		return message;
	}
}
