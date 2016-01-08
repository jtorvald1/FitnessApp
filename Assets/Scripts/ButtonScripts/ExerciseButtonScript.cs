using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ExerciseButtonScript : MonoBehaviour {

	public string name;
	public string id;
	public Sprite icon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PrepareButtonEvent() {
		ScrollListButton button = gameObject.GetComponent <ScrollListButton> ();
		UnityAction myAction = new UnityAction (UIPrepareExercisePanel);
		button.button.onClick.AddListener(myAction);
	}

	public void UIPrepareExercisePanel () {
		UIController.Instance.PrepareExercisePanel (id);
	}
}
