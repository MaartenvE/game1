using UnityEngine;
using BuildingBlocks.Player;

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
		if(Player.LocalPlayer.Team!=null){
			GameObject imageTarget = GameObject.Find(Player.LocalPlayer.Team.Target);
			giveColorToTeamTarget (ColorModel.GREEN, imageTarget);
		}
	}

}
