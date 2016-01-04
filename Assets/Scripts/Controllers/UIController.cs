using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent (typeof (PanelName))]
public class UIController : MonoBehaviour {

	/*
	public enum CurrentPanel
	{
		FacebookLoginPanel,
		LoadingPanel,
		GymPanel
	};
	*/
	//Panel Organization
	public PanelName panelName;
	public GameObject currentPanel;
	public GameObject lastPanel;

	//All Panels
	public List<GameObject> allPanelsList;

	//Map members
	public GameObject mapPanel;

	//Messages members
	public Image messageCountImage;
	public Text messageCountText;

	//Friend buttons
	public List<SampleButton> buttonList = new List<SampleButton>();

	#region Init
	private static UIController _instance;
	public static UIController Instance
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
		panelName = GetComponent<PanelName> ();
	}
	
	// Update is called once per frame
	void Update () {

		//Update message icons
		if (MessageManager.Instance.unreadMessagesList.Count > 0) {
			messageCountImage.enabled = true;
			messageCountText.enabled = true;
		} else { 
			messageCountImage.enabled = false;
			messageCountText.enabled = false;
		}
		if (MessageManager.Instance.unreadMessagesList.Count < 10)
			messageCountText.text = MessageManager.Instance.unreadMessagesList.Count.ToString ();
		else
			messageCountText.text = "9+";

		//Update message icons on friend buttons
		foreach (SampleButton button in buttonList) {
			
			if (button.unreadMessagesCount > 0) {
				button.messagesCountImage.enabled = true;
				Text text = button.messagesCountImage.GetComponentInChildren<Text> ();
				text.enabled = true;
			} else { 
				button.messagesCountImage.enabled = false;
				Text text = button.messagesCountImage.GetComponentInChildren<Text> ();
				text.enabled = false;
			}
			if (button.unreadMessagesCount < 10) {
				Text text = button.messagesCountImage.GetComponentInChildren<Text> ();
				text.text = button.unreadMessagesCount.ToString (); 
			} else {
				Text text = button.messagesCountImage.GetComponentInChildren<Text> ();
				text.text = "9+";
			}
		}
	
	}

	public void PrepareGymPanel (string gymID) {
		Gym gym = GymManager.Instance.gymList.Find(item => item.GymID == gymID);
		GymManager.Instance.currentGym = gym;
		string gymName = gym.GymName;
		string gymAddress = gym.GymAddress;
		Sprite gymPic = gym.GymPicList[0];

		GymPanelView gymView = GetComponent<GymPanelView>();
		gymView.GeneratePanel (gymName, gymAddress, gymPic);
		mapPanel.SetActive (false);
		GameObject map = GameObject.Find ("[Map]");
		map.SetActive (false);
		//gameObject.GymPanelView.GeneratePanel ();

		GetComponent<CreateActiveFriendScrollList>().PrepareFriendListCheckedIn(gym);

		GetComponent<CreateExerciseScrollList>().PrepareExerciseList(gym);
	}

	public void BackToGymPanel () {
		Gym gym = GymManager.Instance.currentGym;
		string gymName = gym.GymName;
		string gymAddress = gym.GymAddress;
		Sprite gymPic = gym.GymPicList[0];
		
		GymPanelView gymView = GetComponent<GymPanelView>();
		gymView.GeneratePanel (gymName, gymAddress, gymPic);

		ExercisePanelController exercisePanelController = GetComponent<ExercisePanelController>();
		exercisePanelController.DeactivatePanel ();
		//gameObject.GymPanelView.GeneratePanel ();
		
		GetComponent<CreateActiveFriendScrollList>().PrepareFriendListCheckedIn(gym);
		
		GetComponent<CreateExerciseScrollList>().PrepareExerciseList(gym);
	}

	public void PrepareExercisePanel (string exerciseID) {
		Exercise exercise = GymManager.Instance.currentGym.exerciseList.Find(item => item.ExerciseID == exerciseID);
		GymManager.Instance.currentExercise = exercise;
		string exerciseName = exercise.ExerciseName;
		string exerciseVidPath = exercise.ExerciseVideoPath;
		Sprite exercisePic = exercise.exercisePic;
		
		ExercisePanelController exercisePanelController = GetComponent<ExercisePanelController>();
		exercisePanelController.GeneratePanel (exerciseName, exerciseVidPath, exercisePic);

		GymPanelView gymView = GetComponent<GymPanelView> ();
		gymView.DeactivatePanel ();
		//gameObject.GymPanelView.GeneratePanel ();
		
		//GetComponent<CreateActiveFriendScrollList>().PrepareFriendList(gym);
		
		//GetComponent<CreateMachineScrollList>().PrepareMachineList(gym);
	}

	public void PrepareAllFriendsPanel() {
		GetComponent<CreateFriendScrollList>().PrepareFriendList();
		DisableAllPanels ();
		EnablePanel(panelName.AllFriendsPanel);
	}

	public void PrepareMapPanel() {
		DisableAllPanels ();
		EnablePanel(panelName.MapPanel);
	}

	public void PrepareFacebookLoginPanel () {
		DisableAllPanels ();
		EnablePanel (panelName.FacebookLoginPanel);
	}

	public void PrepareLoadingPanel () {
		DisableAllPanels ();
		EnablePanel (panelName.LoadingPanel);
	}

	public void PreparePleaseLoginPanel () {
		DisableAllPanels ();
		EnablePanel (panelName.FacebookLoginPanel);
		EnablePanel (panelName.LoadingPanel);
	}

	public void DisableAllPanels() {
		foreach (GameObject panel in allPanelsList) {
			panel.SetActive (false);
		}
	}

	public void EnablePanel(GameObject panel) {
		panel.SetActive(true);
		lastPanel = currentPanel;
		currentPanel = panel;
	}
}
