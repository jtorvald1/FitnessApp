using UnityEngine;
using System.Collections;

public class Exercise : MonoBehaviour {

	public string exerciseID;
	public string exerciseName;
	public string exerciseVideoPath;
	public Sprite exercisePic;

	public Exercise (string exID, string exName, string exVidPath) {

		exerciseID = exID;
		exerciseName = exName;
		exerciseVideoPath = exVidPath;

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string ExerciseID {
		get {
			return this.exerciseID;
		}
		set {
			exerciseID = value;
		}
	}

	public string ExerciseName {
		get {
			return this.exerciseName;
		}
		set {
			exerciseName = value;
		}
	}

	public string ExerciseVideoPath {
		get {
			return this.exerciseVideoPath;
		}
		set {
			exerciseVideoPath = value;
		}
	}

	public Sprite ExercisePic {
		get {
			return this.exercisePic;
		}
		set {
			exercisePic = value;
		}
	}
}
