using UnityEngine;
using System.Collections;
using Facebook.Unity;
using Facebook.MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class FacebookInfoHandler : MonoBehaviour {

	private delegate void SetPicture(string userID, Sprite pic);

	private bool loadingComplete;
	private bool busy = false;
	private FacebookFriend fbfriend = new FacebookFriend();

	//Struct to store Facebook info
	public FacebookUserDataStruct facebookInfoStruct = new FacebookUserDataStruct();

	#region Init
	private static FacebookInfoHandler _instance;
	public static FacebookInfoHandler Instance
	{
		get
		{
			return _instance;
		}
	}

	void Awake ()
	{
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

		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		facebookInfoStruct.UserFriends = new List<FacebookFriend>();
		facebookInfoStruct.UserPermissions = new List<string>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}
	
	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	public void FacebookLoginWithPublish(){
		var perms = new List<string> (){"publish_actions"};
		FB.LogInWithPublishPermissions (perms, LoginCallback);
	}


	public void FacebookLoginWithRead(){
		var perms = new List<string>(){"public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(perms, LoginCallback);
	}

	private void LoginCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
		} else {
			Debug.Log("User cancelled login");
			AppController.Instance.NotLoggedIn();
		}
	}

	public void RetrieveUserInfo () {
		if (FB.IsLoggedIn) {
			FB.API("/me?fields=name,email,gender", Facebook.Unity.HttpMethod.GET, APICallback);
			FB.API ("me/friends?fields=installed,name,id", Facebook.Unity.HttpMethod.GET, APIFriendsListCallback);
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}
		} else {
			Debug.Log("User Not Logged In");
			AppController.Instance.NotLoggedIn();
		}
	}

	public void FacebookPrepareShareLink(){
		if (FB.IsLoggedIn) {
				System.Uri uri1 = new System.Uri ("http://google.com/");
				string title = "This is the title";
				string description = "This is the description";
				System.Uri uri2 = new System.Uri ("http://ladiesloot.com/wp-content/uploads/2015/05/smiley-face-1-4-15.png");
				FacebookShareLink (uri1, title, description, uri2);	
		} else {
			Debug.Log("User Not Logged In");
			AppController.Instance.NotLoggedIn();
		}

	}

	void FacebookShareLink(System.Uri fitnessWebsiteURL, string title, string description, System.Uri photoURL){
		FB.ShareLink (fitnessWebsiteURL, title, description, photoURL);
	}

	void FacebookLogOut(){
		FB.LogOut ();
	}

	public void FacebookGetUserPictureWrapper(string userID){
		StartCoroutine (RetrieveUserImage(userID, SetProfilePic));
	}

	public void FacebookGetFriendPictureWrapper(string userID){
		StartCoroutine (RetrieveUserImage(userID, SetFriendProfilePic));
	}

	IEnumerator RetrieveUserImage(string userID, SetPicture setPicMethod)
	{
		WWW url = new WWW("https" + "://graph.facebook.com/" + userID + "/picture?type=large");
		Texture2D textFb2 = new Texture2D(128, 128, TextureFormat.DXT1, false); //TextureFormat must be DXT5
		yield return url;
		url.LoadImageIntoTexture(textFb2);
		Sprite sprite = Sprite.Create(textFb2, new Rect(0, 0, textFb2.width, textFb2.height), new Vector2(0,0));
		yield return sprite;
		setPicMethod (userID, sprite);
	}

	private void SetProfilePic(string userID, Sprite pic) {
		facebookInfoStruct.UserProfilePic = pic;
	}
	private void SetFriendProfilePic(string userID, Sprite pic) {
		FacebookFriendManager.Instance.SetFacebookFriendImage (userID, pic);
	}

	void CreateFacebookFriend(string id, string name)
	{
		GameObject go = new GameObject();
		fbfriend = go.AddComponent<FacebookFriend>();
		fbfriend.FriendID = id;
		fbfriend.FriendName = name;
		fbfriend.CheckedInGymID = "11";
		FacebookFriendManager.Instance.facebookFriendsList.Add (fbfriend);
		FacebookGetFriendPictureWrapper (id);
	}

	void APICallback(IGraphResult result)                                                                                              
	{                                                                                                                              
		if (result.Error != null) {
			FacebookLogOut ();
			AppController.Instance.NotLoggedIn ();
		} else {
			var dict = Json.Deserialize(result.RawResult) as Dictionary<string,object>;
			string name = null;
			string gender = null;
			string userID = null;
			string email = null;
			name = (string)(dict ["name"]);
			gender = (string)(dict ["gender"]);
			userID = (string)(dict ["id"]);

			facebookInfoStruct.UserName = name;
			facebookInfoStruct.UserGender = gender;
			facebookInfoStruct.UserID = userID;
		}
	}

	void APIFriendsListCallback(IGraphResult result)
	{
		if (result.Error != null) {
			FacebookLogOut ();
			AppController.Instance.NotLoggedIn ();
		} else {
			var dict = Json.Deserialize(result.RawResult) as Dictionary<string,object>;
			var friendList = new List<object>();
			friendList = (List<object>)(dict["data"]);
			FacebookFriendManager.Instance.facebookFriendsList.Clear ();

			int _friendCount = friendList.Count;
			Debug.Log("Found friends on FB, _friendCount ... " +_friendCount);
			List<string> friendIDsFromFB = new List<string>();
			for (int i=0; i<_friendCount; i++) {
				string friendFBID = getDataValueForKey( (Dictionary<string,object>)(friendList[i]), "id");
				string friendName =    getDataValueForKey( (Dictionary<string,object>)(friendList[i]), "name");
				Debug.Log( i +"/" +_friendCount +" " + "FriendFBID " +friendFBID +" " + "FriendName " +friendName);
				friendIDsFromFB.Add(friendFBID);
				CreateFacebookFriend(friendFBID, friendName);
			}
			FacebookGetUserPictureWrapper(facebookInfoStruct.UserID);
			loadingComplete = true;
		}
	}

	public void FacebookCheckpermissions() {
		FB.API("/me/permissions", HttpMethod.GET, CheckPermissionsAPICallback);
	}

	void CheckPermissionsAPICallback (IGraphResult result) {
		if (result.Error != null) {
			FacebookLogOut ();
			AppController.Instance.NotLoggedIn ();
		} else {
			facebookInfoStruct.UserPermissions.Clear ();
			var dict = Json.Deserialize(result.RawResult) as Dictionary<string,object>;
			var permissionsList = new List<object>();
			permissionsList = (List<object>)(dict["data"]);
			for (int i=0; i<permissionsList.Count; i++) {
				string permission = getDataValueForKey( (Dictionary<string,object>)(permissionsList[i]), "permission");
				Debug.Log(permission);
				facebookInfoStruct.UserPermissions.Add(permission);
			}

		}
	}

	private bool FacebookHasPostPermission() {
		if (facebookInfoStruct.UserPermissions.Contains ("publish_actions")) {
			return true;
		}
		return false;
	}

	private string getDataValueForKey(Dictionary<string, object> dict, string key) {
		object objectForKey;
		if (dict.TryGetValue(key, out objectForKey)) {
			return (string)objectForKey;
		} else {
			return "";
		}
	}

	public bool CheckLoggedIn () {
		return FB.IsLoggedIn;
	}


	public bool LoadingComplete {
		get {
			return this.loadingComplete;
		}
		set {
			loadingComplete = value;
		}
	}
}
