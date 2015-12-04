﻿//Could probably combine this class with CreateActiveFriendScrollList.
//Would need to somehow handle "Sample Button" and "Content Panel" assignment for different panels.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class FacebookFriendItem {
	public string name;
	public string id;
	public Sprite icon;
	public Button.ButtonClickedEvent thingToDo;
}

public class CreateFriendScrollList : MonoBehaviour {
	
	public GameObject sampleButton;
	public List<FacebookFriendItem> itemList;
	
	public Transform contentPanel;
	
	void Start () {
		//PrepareFriendList ();
		//PopulateList ();
	}

	public void PrepareFriendList() {
		itemList.Clear();
		foreach (var friend in FacebookManager.Instance.facebookInfoStruct.UserFriends) {
			FacebookFriendItem item = new FacebookFriendItem();
			item.id = friend.FriendID;
			item.name = friend.FriendName;
			item.icon = friend.FriendProfilePic;
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
			{newButton.GetComponent <LayoutElement>().minHeight = 50f;}
			else if(Screen.height > 400 && Screen.height <= 800)
			{newButton.GetComponent <LayoutElement>().minHeight = 80f;}
			else if(Screen.height > 800 && Screen.height <= 1200)
			{newButton.GetComponent <LayoutElement>().minHeight = 100f;}
			else if(Screen.height > 1200 && Screen.height <= 1600)
			{newButton.GetComponent <LayoutElement>().minHeight = 120f;}
			else if(Screen.height > 1600 && Screen.height <= 1900)
			{newButton.GetComponent <LayoutElement>().minHeight = 140f;}
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