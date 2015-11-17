using UnityEngine;
using System.Collections;
using Facebook.Unity;

public class FacebookFriend : MonoBehaviour {

	private string friendID;
	private string friendName;
	private Sprite friendProfilePic;
	private string checkedInGymID;

	/*
	public FacebookFriend (string id, string name)
	{
		friendID = id;
		friendName = name;
	}
	*/
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//This API call (using the util class) WORKS to get data, and could be used to clean up FacebookManager.
	//But it shouldn't be here. And you would have to deserialize the result to make it useable.
	/*
	public void GetProfilePic() {
		Facebook.Unity.FB.API (Util.GetPictureURL (friendID, 96, 96), Facebook.Unity.HttpMethod.GET, this.FriendPictureCallback);
	}

	void FriendPictureCallback(IGraphResult result)
		
	{
		if (result.Error != null)
		{
			Debug.LogError(result.Error);
			return;
		}
		
		//friendAvatarImage.image = result.Texture;
		//result.= Sprite.Create(textFb2, new Rect(0, 0, textFb2.width, textFb2.height), new Vector2(0,0));
		Debug.Log (result.RawResult);
	}
	*/
	
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
}
