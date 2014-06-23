using UnityEngine;
using System.Collections;

public class giveTeamColor : MonoBehaviour {

	GameObject player;


	void Start () {
		GameObject[] imagetargets = GameObject.FindGameObjectsWithTag ("imageTarget");

		foreach (GameObject target in imagetargets) {
			giveColorToTeamTarget(ColorModel.RED,target);
		}

		player = GameObject.Find("Player");
	}

	public void giveColorToTeamTarget(Color color, GameObject imageTarget){
		Transform marker = imageTarget.transform.Find ("TeamColorMarker");
		giveColor (color, marker);
	}
	
	public void giveColor(Color color, Transform marker){
		foreach (Transform markerpart in marker) {
			markerpart.renderer.material.color = color;
		}
	}

	public void Update(){
		if(player.GetComponent<PlayerInfo>().getTeamInfo()!=null){
			GameObject imageTarget = GameObject.Find(player.GetComponent<PlayerInfo>().getTeamInfo().ImageTarget);
			giveColorToTeamTarget (ColorModel.GREEN, imageTarget);
		}
	}

}
