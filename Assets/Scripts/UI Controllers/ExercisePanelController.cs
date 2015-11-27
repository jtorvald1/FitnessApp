using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExercisePanelController : MonoBehaviour {

	public GameObject exercisePanel;
	public Image exerciseImage;
	public Text exerciseText;
	public string exerciseVidAddress;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GeneratePanel(string exercisename, string vidpath, Sprite exercisepic){
		
		exerciseText.text = exercisename;
		exerciseImage.sprite = exercisepic;
		exercisePanel.SetActive (true);
		
	}

	public void DeactivatePanel(){
		exercisePanel.SetActive (false);
	}
}
