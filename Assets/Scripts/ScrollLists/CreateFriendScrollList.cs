//Could probably combine this class with CreateActiveFriendScrollList.
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
	public int messagesCount;
	public Button.ButtonClickedEvent thingToDo;
}

public class CreateFriendScrollList : MonoBehaviour {
	
	public GameObject friendButton;
	public List<FacebookFriendItem> itemList;

	
	public Transform contentPanel;
	
	void Start () {
		//PrepareFriendList ();
		//PopulateList ();
	}

	// Update is called once per frame
	void Update () {

		
	}

	public void PrepareFriendList() {
		itemList.Clear();
		foreach (var friend in FacebookFriendManager.Instance.facebookFriendsList) {
			FacebookFriendItem item = new FacebookFriendItem();
			item.id = friend.FriendID;
			item.name = friend.FriendName;
			item.icon = friend.FriendProfilePic;
			item.messagesCount = friend.friendUnreadMessages.Count;
			itemList.Add(item);
		}
		PopulateList ();
	}
	
	void PopulateList () {
		UIController.Instance.buttonList.Clear ();
		foreach (var item in itemList) {
			GameObject newButton = Instantiate (friendButton) as GameObject;
			ScrollListButton button = newButton.GetComponent <ScrollListButton> ();
			button.nameLabel.text = item.name;
			button.icon.sprite = item.icon;
			button.unreadMessagesCount = item.messagesCount;
			//          button.button.onClick = item.thingToDo;
			UIController.Instance.buttonList.Add(button);
			newButton.transform.SetParent (contentPanel);
			if (Screen.height <= 400)
			{newButton.GetComponent <LayoutElement>().minHeight = 50f;}
			else if(Screen.height > 400 && Screen.height <= 800)
			{newButton.GetComponent <LayoutElement>().minHeight = 100f;}
			else if(Screen.height > 800 && Screen.height <= 1200)
			{newButton.GetComponent <LayoutElement>().minHeight = 150f;}
			else if(Screen.height > 1200 && Screen.height <= 1600)
			{newButton.GetComponent <LayoutElement>().minHeight = 200f;}
			else if(Screen.height > 1600 && Screen.height <= 1900)
			{newButton.GetComponent <LayoutElement>().minHeight = 250f;}
			else if(Screen.height > 1900)
			{newButton.GetComponent <LayoutElement>().minHeight = 300f;}
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