using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MachineItem {
	public string name;
	public string id;
	public Sprite icon;
	public Button.ButtonClickedEvent thingToDo;
}

public class CreateMachineScrollList: MonoBehaviour {
	
	public GameObject sampleButton;
	public List<MachineItem> itemList;
	
	public Transform contentPanel;
	
	void Start () {
		//PrepareFriendList ();
		//PopulateList ();
	}
	
	public void PrepareMachineList(Gym gym) {
		itemList.Clear();
		foreach (var machine in gym.machineList) {
			MachineItem item = new MachineItem();
			item.id = machine.MachineID;
			item.name = machine.MachineName;
			item.icon = machine.MachinePic;
			itemList.Add(item);
		}
		PopulateList ();
	}
	
	void PopulateList () {
		foreach (var item in itemList) {
			GameObject newButton = Instantiate (sampleButton) as GameObject;
			SampleButton button = newButton.GetComponent <SampleButton> ();
			button.nameLabel.text = item.name;
			button.icon.sprite = item.icon;
			//          button.button.onClick = item.thingToDo;
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