using UnityEngine;
using System.Collections;
using Facebook.Unity;
using Facebook.MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour {

	public Text ErrorText;
	public Image UserPic;
	Texture2D UserImg;
	public FacebookFriend fbfriend = new FacebookFriend();

	//Struct to store Facebook info
	public FacebookUserDataStruct facebookInfoStruct = new FacebookUserDataStruct();

	public List<string> Friendlist = new List<string>();

	#region Init
	private static FacebookManager _instance;
	public static FacebookManager Instance
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

	void FacebookActivateApp() {
		FB.ActivateApp ();
	}

	public void FacebookLoginWithPublish(){
		var perms = new List<string> (){"publish_actions"};
		FB.LogInWithPublishPermissions (perms, AuthCallback);
	}


	public void FacebookLoginWithRead(){
		var perms = new List<string>(){"public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}

	private void AuthCallback (ILoginResult result) {
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
			Debug.Log("User cancelled login");
		}
	}

	public void FacebookPost(){
		var postData = 
		new Dictionary<string, string>() {{"test post", "post 1"}};

		FB.API("/me", Facebook.Unity.HttpMethod.POST, APICallback, postData);
	}

	public void FacebookPostLink(){
		System.Uri uri1 = new System.Uri("http://google.com/");
		string title = "This is the title";
		string description = "This is the description";
		System.Uri uri2 = new System.Uri ("http://ladiesloot.com/wp-content/uploads/2015/05/smiley-face-1-4-15.png");
		FacebookShareLink (uri1, title, description, uri2);
	}

	void OnLoggedIn()
	{
		//Relies on that Menu class
		/*
		Util.Log("Logged in. ID: " + FB.UserId);
		
		// Reqest player info and profile picture                                                                           
		FB.API("/me?fields=id,first_name,friends.limit(100).fields(first_name,id)", Facebook.HttpMethod.GET, APICallback);  
		LoadPicture(Util.GetPictureURL("me", 128, 128),MyPictureCallback);
		*/
	}
	
	void FacebookLogOut(){
		FB.LogOut ();
	}

	void FacebookShareLink(System.Uri fitnessWebsiteURL, string title, string description, System.Uri photoURL){
		FB.ShareLink (fitnessWebsiteURL, title, description, photoURL);
	}

	public void FacebookGetFriendsInstalled(){
		FB.API ("me/friends?fields=installed", Facebook.Unity.HttpMethod.GET, APIFriendsListCallback);
		//FB.API ("me?fields=id,name,picture", Facebook.Unity.HttpMethod.GET, APICallback);
	}

	//Right now gets called at end of GetFriendsCallback. Better spot for this?
	public void FacebookGetUserPicture(string userID){
		/*
		WWW url = new WWW("https" + "://graph.facebook.com/me/picture?type=large"); 
		Texture2D textFb2 = new Texture2D(128, 128, TextureFormat.DXT1, false); //TextureFormat must be DXT5
		url.LoadImageIntoTexture(textFb2);
		UserImg = textFb2;
		UserPic.sprite = Sprite.Create(UserImg, new Rect(0, 0, UserImg.width, UserImg.height), new Vector2(0,0));
		*/

		StartCoroutine (UserImage(userID));

		//Using coroutinewithdata util
		/*
		CoroutineWithData cd = new CoroutineWithData(this, UserImage(userID));
		yield return cd.coroutine;
		return (Sprite) cd.result;
		*/
	}

	public void FacebookGetFriendPicture(string userID){
		StartCoroutine (FriendImage(userID));
	}

	public void FacebookGetUserPicture2(){
		/*FB.API(FacebookUtil.GetPictureURL(userInfo.id, 128, 128), Facebook.HttpMethod.GET, (FBResult pictureResult) => {
			if (pictureResult.Error != null) {
				Debug.LogError("Error getting FB friend picture: " + pictureResult.Error);
			}
			else {
				Util.GetPictureURL(
				pictureURL = FacebookUtil.DeserializePictureURLString(pictureResult.Text);
			}
			finishedGettingPictureURL = true;
		});*/
	}


	//UserImage() and FriendImage() SHOULD BE COMBINED TO A SINGLE FUNCTION THAT RETURNS THE IMAGE.
	//BUT I DON'T KNOW HOW TO DO THAT WITH A COROUTINE
	IEnumerator UserImage(string userID)
	{
		//WWW url = FB.API ("me/picture?type=large", Facebook.Unity.HttpMethod.GET, APICallback);
		WWW url = new WWW("https" + "://graph.facebook.com/" + userID + "/picture?type=large");
		//WWW url = new WWW ("http://ladiesloot.com/wp-content/uploads/2015/05/smiley-face-1-4-15.png");
		Texture2D textFb2 = new Texture2D(128, 128, TextureFormat.DXT1, false); //TextureFormat must be DXT5
		yield return url;
		url.LoadImageIntoTexture(textFb2);
		Sprite sprite = Sprite.Create(textFb2, new Rect(0, 0, textFb2.width, textFb2.height), new Vector2(0,0));
		yield return sprite;
		//UserPic.sprite = Sprite.Create(textFb2, new Rect(0, 0, textFb2.width, textFb2.height), new Vector2(0,0));
		facebookInfoStruct.UserProfilePic = Sprite.Create(textFb2, new Rect(0, 0, textFb2.width, textFb2.height), new Vector2(0,0));
	}
	//UserImage() and FriendImage() SHOULD BE COMBINED TO A SINGLE FUNCTION THAT RETURNS THE IMAGE.
	//BUT I DON'T KNOW HOW TO DO THAT WITH A COROUTINE
	IEnumerator FriendImage(string userID)
	{
		//WWW url = FB.API ("me/picture?type=large", Facebook.Unity.HttpMethod.GET, APICallback);
		WWW url = new WWW("https" + "://graph.facebook.com/" + userID + "/picture?type=large");
		//WWW url = new WWW ("http://ladiesloot.com/wp-content/uploads/2015/05/smiley-face-1-4-15.png");
		Texture2D textFb2 = new Texture2D(128, 128, TextureFormat.DXT1, false); //TextureFormat must be DXT5
		yield return url;
		url.LoadImageIntoTexture(textFb2);
		Sprite sprite = Sprite.Create(textFb2, new Rect(0, 0, textFb2.width, textFb2.height), new Vector2(0,0));
		yield return sprite;
		foreach (var friend in facebookInfoStruct.UserFriends) {
			if (friend.FriendID == userID)
				friend.FriendProfilePic = Sprite.Create(textFb2, new Rect(0, 0, textFb2.width, textFb2.height), new Vector2(0,0));
				UserPic.sprite = Sprite.Create(textFb2, new Rect(0, 0, textFb2.width, textFb2.height), new Vector2(0,0));
		}
	}

	//IEnumerator UserImage()
	//{
		//FB.API("me/picture?type=med", Facebook.Unity.HttpMethod.GET, GetPicture);
		/*
		WWW url = new WWW("https" + "://graph.facebook.com/" + FB.UserId.ToString() + "/picture?type=large"); 
		Texture2D textFb2 = new Texture2D(128, 128, TextureFormat.DXT1, false); //TextureFormat must be DXT5
		yield return url;
		url.LoadImageIntoTexture(textFb2);
		UserImg = textFb2;
		*/

		// Reqest player info and profile picture                                                                           
		/*FB.API("/me?fields=id,first_name,friends.limit(100).fields(first_name,id)", Facebook.HttpMethod.GET, APICallback);  
		LoadPicture(Util.GetPictureURL("me", 128, 128),MyPictureCallback); */   
	//}

	/*private void GetPicture(IResult result)
	{
		if (result.Error == null)
		{          
			Image img = UIFBProfilePic.GetComponent<Image>();
			img.sprite = Sprite.Create(result.Texture, new Rect(0,0, 128, 128), new Vector2());         
		}
		
	}*/

	void CreateFacebookFriend(string id, string name)
	{
		fbfriend = new FacebookFriend();
		fbfriend.FriendID = id;
		fbfriend.FriendName = name;
		fbfriend.CheckedInGymID = "11";
		facebookInfoStruct.UserFriends.Add(fbfriend);
		//fbfriend.FriendProfilePic = FacebookGetFriendPicture (id);
		FacebookGetFriendPicture (id);
		//fbfriend.GetProfilePic ();
	}

	void APICallback(IGraphResult result)                                                                                              
	{                                                                                                                              
		//Util.Log("APICallback");                                                                                                
		/*if (result.Error != null)                                                                                                  
		{                                                                                                                          
			//Util.LogError(result.Error);                                                                                           
			// Let's just try again                                                                                                
			FB.API("/me?fields=id,first_name", Facebook.HttpMethod.GET, APICallback);     
			return;                                                                                                                
		}                                                                                                                          
		
		profile = Util.DeserializeJSONProfile(result.Text); 
		*/
		ErrorText.text=result.RawResult;
		var dict = Json.Deserialize(result.RawResult) as Dictionary<string,object>;
		string name = null;
		string gender = null;
		string userID = null;
		string email = null;
		name = (string)(dict ["name"]);
		gender = (string)(dict ["gender"]);
		userID = (string)(dict ["id"]);
		//email = (string)(dict ["email"]);

		facebookInfoStruct.UserName = name;
		facebookInfoStruct.UserGender = gender;
		facebookInfoStruct.UserID = userID;
	}

	void APIFriendsListCallback(IGraphResult result)
	{
		ErrorText.text=result.RawResult;
		var dict = Json.Deserialize(result.RawResult) as Dictionary<string,object>;
		var friendList = new List<object>();
		friendList = (List<object>)(dict["data"]);
		facebookInfoStruct.UserFriends.Clear ();

		/*
		string temp = "";
		foreach(Object str in friendList)
		{
			temp += str.ToString(); //maybe also + '\n' to put them on their own line.
		}
		*/

		int _friendCount = friendList.Count;
		ErrorText.text = _friendCount.ToString ();
		Debug.Log("Found friends on FB, _friendCount ... " +_friendCount);
		List<string> friendIDsFromFB = new List<string>();
		for (int i=0; i<_friendCount; i++) {
			string friendFBID = getDataValueForKey( (Dictionary<string,object>)(friendList[i]), "id");
			string friendName =    getDataValueForKey( (Dictionary<string,object>)(friendList[i]), "name");
			Debug.Log( i +"/" +_friendCount +" " + "FriendFBID " +friendFBID +" " + "FriendName " +friendName);
			friendIDsFromFB.Add(friendFBID);
			CreateFacebookFriend(friendFBID, friendName);
		}
		//facebookInfoStruct.UserFriends = friendIDsFromFB;
		FacebookGetUserPicture(facebookInfoStruct.UserID);
	}

	private string getDataValueForKey(Dictionary<string, object> dict, string key) {
		object objectForKey;
		if (dict.TryGetValue(key, out objectForKey)) {
			return (string)objectForKey;
		} else {
			return "";
		}
	}
	
	
}
