//Could probably combine this class with CreateActiveFriendScrollList.
//Would need to somehow handle "Sample Button" and "Content Panel" assignment for different panels.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GymItem {
	public string name;
	public string id;
	public string address;
	public Sprite icon;
	public Button.ButtonClickedEvent thingToDo;
}

public class CreateGymScrollList : MonoBehaviour {
	
	public GameObject gymButton;
	public List<GymItem> itemList;
	
	
	public Transform contentPanel;
	
	void Start () {
		//PrepareFriendList ();
		//PopulateList ();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	public void PrepareGymList() {
		itemList.Clear();
		foreach (var gym in GymManager.Instance.gymList) {
			GymItem item = new GymItem();
			item.id = gym.GymID;
			item.name = gym.GymName;
			item.icon = gym.GymPicList[0];
			item.address = gym.GymAddress;
			itemList.Add(item);
		}
		PopulateList ();
	}
	
	void PopulateList () {
		UIController.Instance.gymButtonList.Clear ();
		foreach (var item in itemList) {
			GameObject newButton = Instantiate (gymButton) as GameObject;
			ScrollListButton button = newButton.GetComponent <ScrollListButton> ();
			button.nameLabel.text = item.name;
			button.icon.sprite = item.icon;
			button.addressLabel.text = item.address;
			//          button.button.onClick = item.thingToDo;
			UIController.Instance.gymButtonList.Add(button);
			newButton.transform.SetParent (contentPanel);
			if (Screen.height <= 400)
			{newButton.GetComponent <LayoutElement>().minWidth = 50f;}
			else if(Screen.height > 400 && Screen.height <= 800)
			{newButton.GetComponent <LayoutElement>().minWidth = 100f;}
			else if(Screen.height > 800 && Screen.height <= 1200)
			{newButton.GetComponent <LayoutElement>().minWidth = 150f;}
			else if(Screen.height > 1200 && Screen.height <= 1600)
			{newButton.GetComponent <LayoutElement>().minWidth = 200f;}
			else if(Screen.height > 1600 && Screen.height <= 1900)
			{newButton.GetComponent <LayoutElement>().minWidth = 250f;}
			else if(Screen.height > 1900)
			{newButton.GetComponent <LayoutElement>().minWidth = 300f;}
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