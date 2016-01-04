using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (MessageFactory))]
public class MessageManager : MonoBehaviour {
	
	public List<Message> unreadMessagesList;
	public List<Message> unsentMessagesList;
	private MessageFactory messageFactory;


	#region Init
	private static MessageManager _instance;
	public static MessageManager Instance
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

		messageFactory = GetComponent<MessageFactory> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddMessageToUnreadList(Message message) {
		unreadMessagesList.Add (message);
	}

	public void RemoveMessageFromUnreadList(Message message) {
		unreadMessagesList.Remove (message);
	}

	public void RemoveMessageFromUnsentList(Message message) {
		unsentMessagesList.Remove (message);
	}

	public void GetMessagesFromServer(string facebookUserID) {
		/*
		 * uses the user's FacebookUserID to get all messages that contain this user as Receiver.
		 * Should This method have the FacebookID passed to it?
		 * Or should this method just tap into the Facebook Manager?
		 * */
		//ServerInfoGetter.GetMessages (FacebookManager.Instance.facebookInfoStruct.UserID);
		//RefreshMessages ();
		ServerInfoHandler.Instance.GetMessagesWrapper (facebookUserID);
	}

	public void RefreshMessages(List<FacebookFriend> userFriends) {

		GetMessagesFromServer (FacebookInfoHandler.Instance.facebookInfoStruct.UserID);

		foreach (FacebookFriend friend in FacebookFriendManager.Instance.facebookFriendsList) {
			friend.friendUnreadMessages.Clear();
		}

		foreach (Message message in unreadMessagesList) {
			/*foreach (FacebookFriend friend in userFriends) {
				if(message.senderID == friend.FriendID)
				{
					friend.FriendUnreadMessages.Add(message);
				}
			}*/

			FacebookFriend friend = FacebookFriendManager.Instance.GetFriendByID(message.senderID);
			if(friend != null) {
				friend.FriendUnreadMessages.Add(message);
			}
		}

	}

	public void TrySendOutgoingMessages(){
		foreach (Message message in unsentMessagesList)
			ServerInfoHandler.Instance.SendMessageWrapper (message);
	}

	public void PrepareNewOutgoingMessage(string senderID, string receiverID, string messageText) {
		unsentMessagesList.Add (CreateNewAppMessage (senderID, receiverID, messageText));
	}

	public void ReadMessagesByFriendID(string friendID) {

		foreach (Message message in GetMessagesBySenderID(friendID)) {

			//Need any code to show the message?

			RemoveMessageFromUnreadList (message);
		}

	}

	public List<Message> GetMessagesBySenderID(string friendID) {
		List<Message> result = new List<Message>();
		foreach (Message message in unreadMessagesList) {
			if (message.senderID == friendID)
				result.Add(message);
		}
		return result;
	}

	public Message CreateNewAppMessage(string senderID, string receiverID, string messageText) {
		Message message;
		message = messageFactory.CreateMessage (senderID, receiverID, messageText);
		return message;
	}
}
