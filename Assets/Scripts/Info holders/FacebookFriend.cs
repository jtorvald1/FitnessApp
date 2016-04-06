using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;

public class FacebookFriend : MonoBehaviour {

	private string friendID;
	private string friendName;
	private Sprite friendProfilePic;
	private string checkedInGymID;
	public List<Message> friendUnreadMessages = new List<Message>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string FriendName {
		get { return this.friendName; }
		set { friendName = value; }
	}

	public string FriendID {
		get { return this.friendID; }
		set { friendID = value; }
	}

	public Sprite FriendProfilePic {
		get { return this.friendProfilePic; }
		set { friendProfilePic = value; }
	}
	public string CheckedInGymID {
		get {return this.checkedInGymID;}
		set {checkedInGymID = value;}
	}
	public List<Message> FriendUnreadMessages {
		get {return this.friendUnreadMessages;}
		set {friendUnreadMessages = value;}
	}
}
