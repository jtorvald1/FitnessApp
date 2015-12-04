using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageManager : MonoBehaviour {

	public List<Message> unreadMessagesList;


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

	public void GetMessagesFromServer(string facebookID) {
		/*
		 * uses the user's FacebookID to get all messages that contain this user as Receiver.
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

	public void sendNewMessage(string message){



	}
}
