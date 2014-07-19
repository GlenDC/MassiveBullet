using UnityEngine;
using System.Collections;

public class FloatingScoreObjectScript : MonoBehaviour {

	[ SerializeField ] float
		FloatingSpeed,
		FadingSpeed;

	bool
		FloatingScoreOn;

	Transform
		PlayerTransform;

	// Use this for initialization
	void Start () {

		PlayerTransform = GameObject.FindWithTag(TAGS.PLAYER).transform;
	}

	public void SetUpFloatingScore(int score_value, Material text_color){

		this.GetComponent<TextMesh>().text = "+ " + score_value.ToString();

		this.GetComponent<TextMesh>().color = text_color.color;

		FloatingScoreOn = true;

		//this
	}

	public bool GetFloatingScoreOn(){
		bool floating_score_on = FloatingScoreOn;
		return floating_score_on;
	}

	// Update is called once per frame
	void Update () {
		
		if (FloatingScoreOn){

			transform.LookAt(PlayerTransform.position);

			//this.transform.Rotate(Vector3.up,Mathf.PI);

			Vector3 floating_score_position = this.transform.position;

			floating_score_position.y += FloatingSpeed * Time.deltaTime;

			this.transform.position = floating_score_position;

			Color floating_score_color = this.GetComponent<TextMesh>().color;

			floating_score_color.a -= FadingSpeed;

			if (floating_score_color.a < 0.0f){

				//FloatingScoreOn = false;
			}

			this.GetComponent<TextMesh>().color = floating_score_color;
		}
	}
}
