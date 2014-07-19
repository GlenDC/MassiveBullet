using UnityEngine;
using System.Collections;

public class FloatingScoreObjectScript : MonoBehaviour {

	[ SerializeField ] GameObject
		FloatingScoreObject;

	[ SerializeField ] float
		FloatingSpeed,
		FadingSpeed;

	bool
		FloatingScoreOn;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpFloatingScore(int score_value, Material text_color){

		FloatingScoreObject.GetComponent<TextMesh>().text = "+ " + score_value.ToString();

		FloatingScoreObject.GetComponent<TextMesh>().color = text_color.color;

		FloatingScoreOn = true;
	}

	public bool GetFloatingScoreOn(){
		bool floating_score_on = FloatingScoreOn;
		return floating_score_on;
	}

	// Update is called once per frame
	void Update () {
	
		if (FloatingScoreOn){

			Vector3 floating_score_position = this.transform.position;

			floating_score_position.y += FloatingSpeed * Time.deltaTime;

			FloatingScoreObject.transform.position = floating_score_position;

			Color floating_score_color = FloatingScoreObject.GetComponent<TextMesh>().color;

			floating_score_color.a -= FadingSpeed;

			if (floating_score_color.a < 0.0f){

				FloatingScoreOn = false;
			}

			FloatingScoreObject.GetComponent<TextMesh>().color = floating_score_color;
		}
	}
}
