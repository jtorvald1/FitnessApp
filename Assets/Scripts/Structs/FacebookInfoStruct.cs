using System;
using UnityEngine;
using System.Collections.Generic;

public struct FacebookInfoStruct {
	
	private string userName;
	private string userID;
	private string userGender;
	private string userEmail;
	private Sprite userProfilePic;
	private List<string> userFriends;

	/*
	public FacebookInfoStruct () {
		
		userName = null;
		userID = null;
		userGender = null;
		userProfilePic = null;
		
	}
	*/

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
	public List<string> UserFriends {
		get { return this.userFriends; }
		set { userFriends = value; }
	}
}