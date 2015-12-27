using UnityEngine;
using System.Collections;

public class AppManager : MonoBehaviour {
	
	enum AppState
	{
		startup,
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
	
	// Update is called once per frame
	void Update () {

		if (appState == AppState.startup) {
			if (FacebookManager.Instance.CheckLoggedIn() == false && UIManager.Instance.currentPanel != UIManager.Instance.panelName.FacebookLoginPanel) {
				UIManager.Instance.PrepareFacebookLoginPanel();
			}
			if (FacebookManager.Instance.LoadingComplete() == false) {
				appState = AppState.loading;
			}
		}

		if (appState == AppState.loading && UIManager.Instance.currentPanel!= UIManager.Instance.panelName.LoadingPanel) {
			UIManager.Instance.PrepareLoadingPanel();
		}


	
	}

	public void onFacebookInfoDone() {

		MessageManager.Instance.RefreshMessages (FacebookFriendManager.Instance.facebookFriendsList);

	}
}
