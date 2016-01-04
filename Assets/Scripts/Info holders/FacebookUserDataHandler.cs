using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FacebookUserDataHandler : MonoBehaviour {

	private string userName;
	private string userID;
	private string userGender;
	private string userEmail;
	private Sprite userProfilePic;
	private List<FacebookFriend> userFriends;
	private string checkedInGymID;
	private List<string> userPermissions;


	// Use this for initialization
	void Start () {

		userFriends = new List<FacebookFriend>();
		userPermissions = new List<string>();
	
	}
	
	// Update is called once per frame
	void Update () {

		/*
		userPermissions = FacebookManager.FacebookCurrentPermissions();
		if (userPermissions != null){
			// the list is ready: do whatever you want to do with it
			
		}*/
	
	}

	public string UserName {
		get { return this.userName; }
		set { userName = value; }
	}
	
	public string UserID {
		get { return this.userID; }
		set { userID = value; }
	}
	
	public string UserGender {
		get { return this.userGender; }
		set { userGender = value; }
	}
	
	public string UserEmail {
		get { return this.userEmail; }
		set { userEmail = value; }
	}
	
	public Sprite UserProfilePic {
		get { return this.userProfilePic; }
		set { userProfilePic = value; }
	}
	public List<FacebookFriend> UserFriends {
		get { return this.userFriends; }
		set { userFriends = value; }
	}
	public string CheckedInGymID {
		get { return this.checkedInGymID; }
		set { checkedInGymID = value; }
	}
	public List<string> UserPermissions {
		get { return this.userPermissions; }
		set { userPermissions = value; }
	}
}
