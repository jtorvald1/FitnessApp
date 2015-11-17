using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GoogleMapImageManager : MonoBehaviour {

	public GoogleMap map;
	public Image mapImage;

	// Use this for initialization
	void Start () {

		int i = 0;
		foreach (Gym gym in GymManager.Instance.gymList)
		{
			GoogleMapLocation location = new GoogleMapLocation();
			GoogleMapMarker marker = new GoogleMapMarker();
			location.latitude = gym.GymLat;
			location.longitude = gym.GymLong;
			marker.size = GoogleMapMarker.GoogleMapMarkerSize.Mid;
			marker.color = GoogleMapColor.black;
			marker.label = gym.GymName;

			List<GoogleMapLocation> locationsList = new List<GoogleMapLocation>();
			/*for (GoogleMapLocation location = 0; runs < 400; runs++)
			{
				termsList.Add(value);
			}*/
			locationsList.Add(location);
			
			// You can convert it back to an array if you would like to
			GoogleMapLocation[] locations = locationsList.ToArray();

			marker.locations[0] = location;
			map.markers[i] = marker;
			i++;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
