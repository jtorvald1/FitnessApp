using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour {

	public string machineID;
	public string machineName;
	public string machineVideoPath;
	public Sprite machinePic;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string MachineID {
		get {
			return this.machineID;
		}
		set {
			machineID = value;
		}
	}

	public string MachineName {
		get {
			return this.machineName;
		}
		set {
			machineName = value;
		}
	}

	public string MachineVideoPath {
		get {
			return this.machineVideoPath;
		}
		set {
			machineVideoPath = value;
		}
	}

	public Sprite MachinePic {
		get {
			return this.machinePic;
		}
		set {
			machinePic = value;
		}
	}
}
