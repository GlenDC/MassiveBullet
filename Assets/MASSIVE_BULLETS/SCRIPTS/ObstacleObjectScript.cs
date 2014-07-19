using UnityEngine;
using System.Collections;

public class ObstacleObjectScript : MonoBehaviour {

	[ SerializeField ] GameObject
		ObstacleObjectTree,
		ObstacleObjectCube;

	[ SerializeField ] Vector2
		TreeHeightOffset;

	// Use this for initialization
	void Start () {
	
	}

	public void SetUpObstacleObject(){

		Vector3 tree_position = ObstacleObjectTree.transform.position;

		tree_position.y -= Random.Range(TreeHeightOffset.x,TreeHeightOffset.y);

		ObstacleObjectTree.transform.position = tree_position;

		ObstacleObjectTree.transform.RotateAround(this.transform.position,Vector3.up,Random.Range(0.0f,360.0f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
