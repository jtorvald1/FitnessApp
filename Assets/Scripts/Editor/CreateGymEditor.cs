using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CreateGymEditor : EditorWindow {
	string myString = "Hello World";
	bool groupEnabled;
	bool myBool = true;
	float myFloat = 1.23f;

	public GymManager gymManager;
	GameObject go;

	string gymID;
	string gymName;
	string gymAddress;
	float gymLat;
	float gymLong;
	Sprite gymPic;
	List<Sprite> gymPicList = new List<Sprite>();

	string machineID;
	string machineName;
	string machineVidPath;

	string exerciseID;
	string exerciseName;
	string exerciseVidPath;

	List<Machine> machList = new List<Machine>();
	List<Exercise> exList = new List<Exercise>();
	
	// Add menu named "My Window" to the Window menu
	[MenuItem ("Window/Create Gym")]
	static void Init () {
		// Get existing open window or if none, make a new one:
		CreateGymEditor window = (CreateGymEditor)EditorWindow.GetWindow (typeof (CreateGymEditor));
		window.Show();
	}
	
	void OnGUI () {
		gymManager = EditorGUILayout.ObjectField ("GymManager", gymManager, typeof(GymManager), true) as GymManager;
		GUILayout.Label ("Gym Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField ("Text Field", myString);
		
		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
		myBool = EditorGUILayout.Toggle ("Toggle", myBool);
		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
		EditorGUILayout.EndToggleGroup ();

		gymID = EditorGUILayout.TextField ("Gym ID", gymID);
		gymName = EditorGUILayout.TextField ("Gym Name", gymName);
		gymAddress = EditorGUILayout.TextField ("Gym Address", gymAddress);
		gymLat = EditorGUILayout.FloatField ("Gym Latitude", gymLat);
		gymLong = EditorGUILayout.FloatField ("Gym Longitude", gymLong);
		gymPic = EditorGUILayout.ObjectField ("GymPic", gymPic, typeof(Sprite), false) as Sprite;

		if(GUILayout.Button ("Add Gym Picture"))
		{
			if(gymPic != null)
			{
				gymPicList.Add(gymPic);
			}
		}


		GUILayout.Label ("Machine Settings", EditorStyles.boldLabel);

		machineID = EditorGUILayout.TextField ("Machine ID", machineID);
		machineName = EditorGUILayout.TextField ("Machine Name", machineName);
		machineVidPath = EditorGUILayout.TextField ("Machine video path", machineVidPath);
		
		
		if(GUILayout.Button ("Create Machine"))
		{
			machList.Add(CreateMachine(machineID, machineName, machineVidPath));
		}



		GUILayout.Label ("Exercise Settings", EditorStyles.boldLabel);

		exerciseID = EditorGUILayout.TextField ("Exercise ID", exerciseID);
		exerciseName = EditorGUILayout.TextField ("Exercise Name", exerciseName);
		exerciseVidPath = EditorGUILayout.TextField ("Exercise video path", exerciseVidPath);


		if(GUILayout.Button ("Create Exercise"))
		{
			exList.Add(CreateExercise(exerciseID, exerciseName, exerciseVidPath));
		}

		if(GUILayout.Button ("Create Gym"))
		{
			CreateGym(gymID, gymName, gymAddress, gymLat, gymLong, gymPicList, machList, exList);
		}
	}

	public Machine CreateMachine(string id, string name, string vid)
	{
		go = new GameObject();
		Machine ma = go.AddComponent<Machine> ();

		go.name = name + id;
		
		ma.MachineID = id;
		ma.MachineName = name;
		ma.MachineVideoPath = vid;
		
		return ma;
	}

	public Exercise CreateExercise(string id, string name, string vid)
	{
		go = new GameObject();
		Exercise ex = go.AddComponent<Exercise> ();

		go.name = name + id;
		
		ex.ExerciseID = id;
		ex.ExerciseName = name;
		ex.ExerciseVideoPath = vid;
		
		return ex;
	}

	public Gym CreateGym(string id, string name, string address, float lat, float lon, List<Sprite> picList, List<Machine> machList, List<Exercise> exList)
	{
		go = new GameObject();
		Gym gy = go.AddComponent<Gym> ();

		go.name = name + id;
		
		gy.GymID = id;
		gy.GymName = name;
		gy.GymAddress = address;
		gy.GymLat = lat;
		gy.GymLong = lon;
		gy.GymPicList = picList;
		gy.ExerciseList = exList;
		gy.MachineList = machList;

		foreach (Exercise ex in exList) {
			ex.transform.parent = gy.transform;
		}

		foreach (Machine ma in machList) {
			ma.transform.parent = gy.transform;
		}

		gymManager.gymList.Add (gy);
		return gy;
	}
}