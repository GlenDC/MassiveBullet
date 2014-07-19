using UnityEngine;
using System.Collections;

public class ObstacleGeneratorScript : MonoBehaviour {

	[ SerializeField ] GameObject
		ObstacleObjectPrefab;

	[ SerializeField ] float
		ObstacleObjectOffset;

	[ SerializeField ] Vector2
		GroundSize;

	[ SerializeField ] int
		MinObstacle,
		MaxObstacle;

	GameObject[]
		ObstacleArray = new GameObject[20];

	// Use this for initialization
	void Start () {

		GenerateObstacle();
	}

	public void GenerateObstacle(){

		int obstacle_amount = Random.Range(MinObstacle,MaxObstacle);

		for (int i=0; i < obstacle_amount; i++){

			Vector3 obstacle_object_position = this.transform.position;

			obstacle_object_position.x = Random.Range(this.transform.position.x - GroundSize.x/2, this.transform.position.x + GroundSize.x/2);
			obstacle_object_position.z = Random.Range(this.transform.position.z - GroundSize.y/2, this.transform.position.z + GroundSize.y/2);
			obstacle_object_position.y += ObstacleObjectOffset;

			ObstacleArray[i] = Instantiate(ObstacleObjectPrefab,obstacle_object_position,Quaternion.identity) as GameObject;
			ObstacleArray[i].transform.parent = this.transform;
			ObstacleArray[i].name = this.name + "_obstacle_" + i;
			ObstacleArray[i].GetComponent<ObstacleObjectScript>().SetUpObstacleObject();
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
