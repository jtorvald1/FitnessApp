using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GymManager : MonoBehaviour {

	public List<Gym> gymList;
	public List<Gym> gymHistory;
	public Gym currentGym;
	public Exercise currentExercise;
	public string currentExerciseID;
	public string currentExerciseVidPath;

	#region Init
	private static GymManager _instance;
	public static GymManager Instance
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

	public Gym GetGymByID(string gymID) {
		Gym result = gymList.Find(x => x.GymID == gymID);
		return result;
	}

	public void AddGymHistory(Gym gym){
		if (gymHistory.Contains (gym))
			gymHistory.Remove (gym);
		gymHistory.Insert(0, gym);
		//gymHistory.Add (gym);
	}
}
