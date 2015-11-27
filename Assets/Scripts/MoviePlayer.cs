using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoviePlayer : MonoBehaviour {

	private WWW www;
	private bool done=false;
	/*
	private MovieTexture mt;

	public RawImage m_vidImage;

	// Use this for initialization
	void Start () {
		www = new WWW("file://" + Application.streamingAssetsPath + "/Converted.ogg");
	}
	
	// Update is called once per frame
	void Update () {

		if (www.isDone==false && string.IsNullOrEmpty(www.error)==true)
		{
			Debug.Log("Progress: " + www.progress);
		}
		
		if (done==true)
			return;
		
		mt = www.movie;       
		if (www.isDone && string.IsNullOrEmpty(www.error)==false)
		{
			Debug.Log(www.error);
			done=true;
		}
		
		if (mt != null && mt.isReadyToPlay==true && mt.isPlaying == false)
		{
			/*
			audio.clip = mt.audioClip;
			audio.Play();
			
			renderer.material.mainTexture = mt;
			mt.Play();
			done=true;
			*//*
		}
	
	}
*/

	public void PlayMovie () {

	Handheld.PlayFullScreenMovie ("IMG_0342.ogv", Color.black, FullScreenMovieControlMode.CancelOnInput);
	}
	/*

	public void PlayUIMovie() {
		//mt.Play();
		m_vidImage.texture = mt;
		mt.Play();
		//m_vidImage.mainTexture.Play();

		//function Start(){ GetComponent(RawImage).mainTexture.Play(); }
	}
*/
	
	public void PlayMovieFromAddress (string address) {
		
		Handheld.PlayFullScreenMovie (address, Color.black, FullScreenMovieControlMode.CancelOnInput);
	}

	public void PlayCurrentExerciseVideo () {
	string address = GymManager.Instance.currentExercise.ExerciseVideoPath;
		
		Handheld.PlayFullScreenMovie (address, Color.black, FullScreenMovieControlMode.CancelOnInput);
	}

}
