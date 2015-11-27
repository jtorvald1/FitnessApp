using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollListSizeFixer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			// do whatever you want with child transform object here
		
			if (Screen.height <= 400) {
				child.GetComponent <LayoutElement> ().minWidth = 100f;
			} else if (Screen.height > 400 && Screen.height <= 800) {
				child.GetComponent <LayoutElement>().minWidth = 150f;
			} else if(Screen.height > 800 && Screen.height <= 1200) {
				child.GetComponent <LayoutElement>().minWidth = 200f;
			} else if(Screen.height > 1200 && Screen.height <= 1600) {
				child.GetComponent <LayoutElement>().minWidth = 250f;
			} else if(Screen.height > 1600 && Screen.height <= 1900) {
				child.GetComponent <LayoutElement>().minWidth = 300f;
			} else if (Screen.height > 1900) {
				child.GetComponent <LayoutElement> ().minWidth = 350f;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
