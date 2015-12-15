using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (MessageFactory))]
public class MessageManager : MonoBehaviour {
	
	public List<Message> unreadMessagesList;
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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetMessagesFromServer(string facebookUserID) {
		/*
		 * uses the user's FacebookUserID to get all messages that contain this user as Receiver.
		 * Should This method have the FacebookID passed to it?
		 * Or should this method just tap into the Facebook Manager?
		 * */
		//ServerInfoGetter.GetMessages (FacebookManager.Instance.facebookInfoStruct.UserID);
		//RefreshMessages ();
	}

	public void RefreshMessages(List<FacebookFriend> userFriends) {

		foreach (Message message in unreadMessagesList) {
			/*foreach (FacebookFriend friend in userFriends) {
				if(message.senderID == friend.FriendID)
				{
					friend.FriendUnreadMessages.Add(message);
				}
			}*/
			FacebookFriend friend = FacebookFriendManager.Instance.GetFriendByID(message.senderID);
			friend.FriendUnreadMessages.Add(message);
		}

	}

	public void SendNewMessage(Message message){



	}

	public void ReadMessage(string friendID) {

		foreach (Message message in GetMessagesBySenderID(friendID)) {
			unreadMessagesList.Remove (message);
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

	public void CreateNewAppMessage(string senderID) {
		Message message = messageFactory.CreateMessage (senderID, "receiverID", "messageText");
		SendNewMessage (message);
	}

	public void poop(){
	}
}
