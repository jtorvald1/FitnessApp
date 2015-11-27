using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class ExerciseItem {
	public string name;
	public string id;
	public Sprite icon;
	public Button.ButtonClickedEvent thingToDo;
}

public class CreateExerciseScrollList: MonoBehaviour {
	
	public GameObject sampleButton;
	public List<ExerciseItem> itemList;
	
	public Transform contentPanel;
	
	void Start () {
		//PrepareFriendList ();
		//PopulateList ();
	}
	
	public void PrepareExerciseList(Gym gym) {
		itemList.Clear();
		foreach (var exercise in gym.exerciseList) {
			ExerciseItem item = new ExerciseItem();
			item.id = exercise.ExerciseID;
			item.name = exercise.ExerciseName;
			item.icon = exercise.ExercisePic;
			itemList.Add(item);
		}
		PopulateList ();
	}
	
	void PopulateList () {
		foreach (var item in itemList) {
			GameObject newButton = Instantiate (sampleButton) as GameObject;

			//SampleButton and ExerciseButton should be combined into one class
			SampleButton button = newButton.GetComponent <SampleButton> ();
			ExerciseButtonScript exerciseButtonScript = newButton.GetComponent<ExerciseButtonScript>();
			button.nameLabel.text = item.name;
			button.icon.sprite = item.icon;

			exerciseButtonScript.name = item.name;
			exerciseButtonScript.id = item.id;
			exerciseButtonScript.icon = item.icon;
			exerciseButtonScript.PrepareButtonEvent();

			//button.button.onClick.AddListener(myYesAction);
			newButton.transform.SetParent (contentPanel);
			if (Screen.height <= 400)
			{newButton.GetComponent <LayoutElement>().minWidth = 100f;}
			else if(Screen.height > 400 && Screen.height <= 800)
			{newButton.GetComponent <LayoutElement>().minWidth = 140f;}
			else if(Screen.height > 800 && Screen.height <= 1200)
			{newButton.GetComponent <LayoutElement>().minWidth = 160f;}
			else if(Screen.height > 1200 && Screen.height <= 1600)
			{newButton.GetComponent <LayoutElement>().minWidth = 180f;}
			else if(Screen.height > 1600 && Screen.height <= 1900)
			{newButton.GetComponent <LayoutElement>().minWidth = 200f;}
			//newButton.GetComponent<RectTransform>().localScale = new Vector3(1f,1f,1f);﻿
		}
	}
	
	public void SomethingToDo () {
		Debug.Log ("I done did something!");
	}
	
	public void SomethingElseToDo (GameObject item) {
		Debug.Log (item.name);
	}
}