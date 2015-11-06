using UnityEngine;
using System.Collections;
using Facebook.Unity;
using Facebook.MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour {

	public Text ErrorText;
	Texture2D UserImg;

	void Awake ()
	{
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
	}

	// Use this for initialization
	void Start () {
	
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
			FB.API("/me?fields=name,email", Facebook.Unity.HttpMethod.GET, APICallback);
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
		FB.API ("me/friends?fields=installed", Facebook.Unity.HttpMethod.GET, APICallback);
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

	void APICallback(IResult result)                                                                                              
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
		
	}    


}
