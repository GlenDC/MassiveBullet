using UnityEngine;
using System.Collections;

public class ObjectScaleScript : MonoBehaviour {

	[ SerializeField ] float 
		ScaleRange,
		ScaleSpeed;

	[ SerializeField ] Vector3
		ScaleDirection;

	[ SerializeField ] bool
		LoopingScale;

	Vector3
		ScaleOrigin,
		ScaleTarget;

	bool
		ScaleOn;

	// Use this for initialization
	void Start () {

	}

	public void SetUpScale(){

		if (LoopingScale){
			
			ScaleOn = true;
			
			ScaleOrigin = this.transform.localScale;
			
			ScaleTarget = this.transform.localScale;
			
			ScaleTarget.x += ScaleRange * ScaleTarget.x * ScaleDirection.x;
			ScaleTarget.y += ScaleRange * ScaleTarget.y * ScaleDirection.y;
			ScaleTarget.z += ScaleRange * ScaleTarget.z * ScaleDirection.z;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if (ScaleOn){

			Vector3 current_scale = this.transform.localScale;

			current_scale.x += ScaleSpeed * ScaleDirection.x * Time.deltaTime;
			current_scale.y += ScaleSpeed * ScaleDirection.y * Time.deltaTime;
			current_scale.z += ScaleSpeed * ScaleDirection.z * Time.deltaTime;

			if (current_scale.x > ScaleTarget.x || current_scale.y > ScaleTarget.y || current_scale.z > ScaleTarget.z){

				ScaleSpeed = -ScaleSpeed;

				if (current_scale.x > ScaleTarget.x){

					current_scale.x = ScaleTarget.x;
				}
				else if (current_scale.y > ScaleTarget.y){
					
					current_scale.y = ScaleTarget.y;
				}
				else if (current_scale.z > ScaleTarget.z){
					
					current_scale.z = ScaleTarget.z;
				}

				//current_scale.x = ScaleTarget.x;
				//current_scale.y = ScaleTarget.y;
				//current_scale.z = ScaleTarget.z;
			}

			if (current_scale.x <= ScaleOrigin.x || current_scale.y <= ScaleOrigin.y || current_scale.z <= ScaleOrigin.z){
				
				ScaleSpeed = -ScaleSpeed;
				
				current_scale.x = ScaleOrigin.x;
				current_scale.y = ScaleOrigin.y;
				current_scale.z = ScaleOrigin.z;
			}

			this.transform.localScale = current_scale;
		}
	}
}
