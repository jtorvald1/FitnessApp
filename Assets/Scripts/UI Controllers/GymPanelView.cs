using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GymPanelView : MonoBehaviour {

	public GameObject gymPanelView;
	public Image gymImageView;
	public Text gymTextView;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GeneratePanel(string gymname, string gymaddress, Sprite gympic){

		/* Alternative if I decide to use prefabs for all panels:
		 * var instance : GameObject = Instantiate(Resources.Load("Generated Gym Panel"));
		 * 
		 * Then somehow get the image and text from the prefab instance?
		 */

		gymTextView.text = gymname + ", " + gymaddress;
		gymImageView.sprite = gympic;
		gymPanelView.SetActive (true);

	}
	public void DeactivatePanel(){
		gymPanelView.SetActive (false);
	}
}
