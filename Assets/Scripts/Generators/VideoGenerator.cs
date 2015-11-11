using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VideoGenerator : MonoBehaviour {
	/*
	private WWW www;
	private bool done=false;
	private MovieTexture mt;

	public string m_fileAddress;
	public RawImage m_vidImage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateVideo(string fileAddress, RawImage vidImage) {
		m_fileAddress = fileAddress;
		m_vidImage = vidImage;

		www = new WWW("file://" + Application.streamingAssetsPath + m_fileAddress);
		
		while (!www.isDone) {
			
			//yield www;
			//Downloading video texture
			Debug.Log ("Downloading video texture");
			
			Debug.Log ("Done Downloading Texture");
			
			if (www.isDone) {
				break; //if done downloading image break loop
			}
		}

		mt = www.movie;
		m_vidImage.texture = mt;
		mt = (MovieTexture)m_vidImage.mainTexture;

	}

	public void PlayVideo() {
		if (!mt.isPlaying)
		{
			mt.Play();
		}
	}

	public void PauseVideo() {
		if (mt.isPlaying)
		{
			mt.Stop();
		}
	}
*/

	public void PlayVideoFullscreen (string fileAddress) {
		
		Handheld.PlayFullScreenMovie (fileAddress, Color.black, FullScreenMovieControlMode.CancelOnInput);
	
	}

}
