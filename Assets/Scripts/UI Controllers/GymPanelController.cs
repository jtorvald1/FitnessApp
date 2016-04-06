using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GymPanelController : MonoBehaviour {

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

		gymTextView.text = gymname + ", " + gymaddress;
		gymImageView.sprite = gympic;
		gymPanelView.SetActive (true);

	}

	public void DeactivatePanel(){
		gymPanelView.SetActive (false);
	}
}
