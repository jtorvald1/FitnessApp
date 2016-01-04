using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]

public class GymMarker : MonoBehaviour {

	public string gymID;
	public BoxCollider bc;

	// Use this for initialization
	void Start () {
		bc = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		print ("MouseDown");
		gameObject.GetComponent<Renderer> ().material.color = Color.red;
		UIController.Instance.PrepareGymPanel (gymID);
	}
}
