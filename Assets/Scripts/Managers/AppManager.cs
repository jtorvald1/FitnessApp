using UnityEngine;
using System.Collections;

public class AppManager : MonoBehaviour {
	
	enum AppState
	{
		startup,
		login,
		loading,
		running
	};

	AppState appState;
	
	#region Init
	private static AppManager _instance;
	public static AppManager Instance
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

	public void SetStateStartup() { 
		appState = AppState.startup;
		FacebookManager.Instance.LoadingComplete = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (appState == AppState.startup) {
			if (FacebookManager.Instance.CheckLoggedIn() == false && UIManager.Instance.currentPanel != UIManager.Instance.panelName.FacebookLoginPanel) {
				UIManager.Instance.PrepareFacebookLoginPanel();
				appState = AppState.login;
			}
			else if (FacebookManager.Instance.CheckLoggedIn() == true && FacebookManager.Instance.LoadingComplete == false) {
				FacebookManager.Instance.RetrieveUserInfo();
				UIManager.Instance.PrepareLoadingPanel();
				appState = AppState.loading;
			}
		}

		if (appState == AppState.login) {
			if (FacebookManager.Instance.CheckLoggedIn() == true ) {
				FacebookManager.Instance.RetrieveUserInfo();
				UIManager.Instance.PrepareLoadingPanel();
				appState = AppState.loading;
			}
		}

		if (appState == AppState.loading) {
			if (FacebookManager.Instance.LoadingComplete == true) {
				MessageManager.Instance.RefreshMessages (FacebookFriendManager.Instance.facebookFriendsList);
				UIManager.Instance.PrepareMapPanel();
				appState = AppState.running;
			}
		}


	
	}

	public void onFacebookInfoDone() {

		//MessageManager.Instance.RefreshMessages (FacebookFriendManager.Instance.facebookFriendsList);

	}

	public void NotLoggedIn(){
		UIManager.Instance.PrepareFacebookLoginPanel();
		appState = AppState.login;
	}
}
