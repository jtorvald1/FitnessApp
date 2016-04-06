using UnityEngine;
using System.Collections;

public class AppController : MonoBehaviour {
	
	enum AppState
	{
		startup,
		login,
		loadingFacebook,
		loadingServer,
		running
	};

	AppState appState;
	
	#region Init
	private static AppController _instance;
	public static AppController Instance
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

		appState = AppState.startup;
	
	}
	
	// Update is called once per frame
	void Update () {

		switch(appState)
		{

		case AppState.startup:
			if (FacebookInfoHandler.Instance.CheckLoggedIn() == false && UIController.Instance.currentPanel != UIController.Instance.panelName.FacebookLoginPanel) {
				StateChange(AppState.login);
			}
			else if (FacebookInfoHandler.Instance.CheckLoggedIn() == true && FacebookInfoHandler.Instance.LoadingComplete == false) {
				StateChange(AppState.loadingFacebook);
			}
			break;
		case AppState.login:
			if (FacebookInfoHandler.Instance.CheckLoggedIn() == true) {
				StateChange (AppState.loadingFacebook);
			}
			break;
		case AppState.loadingFacebook:
			if (FacebookInfoHandler.Instance.LoadingComplete == true) {
				StateChange(AppState.loadingServer);
			}
			break;
		case AppState.loadingServer:
			if (ServerInfoHandler.Instance.LoadingComplete == true) {
				StateChange (AppState.running);
			}
			break;
		case AppState.running:
			break;
		}
	
	}

	void StateChange(AppState newState)
	{ 
		switch(newState)
		{
		case AppState.startup:
			break;
		case AppState.login:
			UIController.Instance.PrepareFacebookLoginPanel();
			break;
		case AppState.loadingFacebook:
			FacebookInfoHandler.Instance.RetrieveUserInfo();
			UIController.Instance.PrepareLoadingPanel();
			break;
		case AppState.loadingServer:
			MessageManager.Instance.RefreshMessages (FacebookFriendManager.Instance.facebookFriendsList);
			break;
		case AppState.running:
			UIController.Instance.PrepareMapPanel();
			break;
		}
		appState = newState; 
	}

	public void NotLoggedIn(){
		UIController.Instance.PrepareFacebookLoginPanel();
		appState = AppState.login;
	}
}
