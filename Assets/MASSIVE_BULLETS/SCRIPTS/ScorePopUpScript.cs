using UnityEngine;
using System.Collections;

public class ScorePopUpScript : MonoBehaviour {

	[ SerializeField ] GameObject
		ParticleEffect1,
		ParticleEffect2;

	[ SerializeField ] GameObject
		FloatingScorePrefab;

	[ SerializeField ] Material[]
		FloatingScoreColor = new Material[3];
	
	GameObject
		FloatingScoreObject;

	bool
		ScorePopUpOn;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpScorePopUp(int score_value, int color_value){

		FloatingScoreObject = Instantiate(FloatingScorePrefab,this.transform.position,Quaternion.identity) as GameObject;
		FloatingScoreObject.transform.parent = this.transform;
		FloatingScoreObject.name = this.name + "_floating_score_object";

		FloatingScoreObject.GetComponent<FloatingScoreObjectScript>().SetUpFloatingScore(score_value,FloatingScoreColor[color_value]);

		ParticleEffect1.GetComponent<ParticleSystem>().Play();
		ParticleEffect2.GetComponent<ParticleSystem>().Play();

		ScorePopUpOn = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (ScorePopUpOn){

			if (!FloatingScoreObject.GetComponent<FloatingScoreObjectScript>().GetFloatingScoreOn()){

				Destroy(this);
			}
		}
	}
}
