using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject mapPanel;

	#region Init
	private static UIManager _instance;
	public static UIManager Instance
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
	
	}
	
	// Update is called once per frame
	void Update () {
	
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

		GetComponent<CreateActiveFriendScrollList>().PrepareFriendList(gym);

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
		
		GetComponent<CreateActiveFriendScrollList>().PrepareFriendList(gym);
		
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



}
