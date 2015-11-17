using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gym : MonoBehaviour {
	
	public string gymID;
	public string gymName;
	public string gymAddress;
	public float gymLat;
	public float gymLong;
	public List<string> userIDsCheckedIn;
	public List<Sprite> gymPicList;
	public List<Machine> machineList;
	public List<Exercise> exerciseList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string GymID {
		get {
			return this.gymID;
		}
		set {
			gymID = value;
		}
	}

	public string GymName {
		get {
			return this.gymName;
		}
		set {
			gymName = value;
		}
	}

	public string GymAddress {
		get {
			return this.gymAddress;
		}
		set {
			gymAddress = value;
		}
	}

	public float GymLat {
		get {
			return this.gymLat;
		}
		set {
			gymLat = value;
		}
	}

	public float GymLong {
		get {
			return this.gymLong;
		}
		set {
			gymLong = value;
		}
	}

	public List<string> UserIDsCheckedIn {
		get {
			return this.userIDsCheckedIn;
		}
		set {
			userIDsCheckedIn = value;
		}
	}

	public List<Sprite> GymPicList {
		get {
			return this.gymPicList;
		}
		set {
			gymPicList = value;
		}
	}

	public List<Machine> MachineList {
		get {
			return this.machineList;
		}
		set {
			machineList = value;
		}
	}

	public List<Exercise> ExerciseList {
		get {
			return this.exerciseList;
		}
		set {
			exerciseList = value;
		}
	}

}
