using UnityEngine;
using System.Collections;

public class BulletObjectScript : MonoBehaviour {

	[ SerializeField ] GameObject
		ScorePopUpPrefab;

	[ SerializeField ] Vector3
		PopUpOffset;

	// Use this for initialization
	void Start () {
	
	}

	public void TriggerBulletExplosion(){

		Vector3 score_popup_position = this.transform.position + PopUpOffset;

		GameObject score_popup = Instantiate(ScorePopUpPrefab,score_popup_position,Quaternion.identity) as GameObject;
		score_popup.name = this.name + "_score_pop_up";

		//score_popup.GetComponent<ScorePopUpScript>().SetUpScorePopUp();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
