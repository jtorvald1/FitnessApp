using UnityEngine;
using System.Collections;

public class MoviePlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayMovie () {
		Handheld.PlayFullScreenMovie ("/IMG_0342.mov", Color.black, FullScreenMovieControlMode.Full);
	}

}
