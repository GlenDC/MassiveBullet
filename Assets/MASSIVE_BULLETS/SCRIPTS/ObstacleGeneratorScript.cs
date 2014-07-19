using UnityEngine;
using System.Collections;

public class ObstacleGeneratorScript : MonoBehaviour {

	[ SerializeField ] GameObject[]
		TentaclePrefabArray = new GameObject[3];

	[ SerializeField ] Material
		TentacleObjectMaterial;

	[ SerializeField ] GameObject[]
		PuddlePrefabArray = new GameObject[5];

	[ SerializeField ] Material
		PuddleObjectMaterial;

	[ SerializeField ] GameObject
		TotemPrefab;

	[ SerializeField ] Vector2
		GroundSize;

	[ SerializeField ] float
		TentacleOffset,
		TentacleHeight,
		PuddleOffset,
		PuddleHeight,
		TotemOffset,
		TotemHeight;

	[ SerializeField ] int
		MinTentacle,
		MaxTentacle,
		MinPuddle,
		MaxPuddle;

	GameObject[]
		TentacleArray = new GameObject[20];

	GameObject[]
		PuddleArray = new GameObject[40];

	GameObject[]
		TotemArray = new GameObject[3];

	// Use this for initialization
	void Start () {

		GenerateObstacle();
	}

	public void GenerateObstacle(){

		int tentacle_amount = Random.Range(MinTentacle,MaxTentacle);

		for (int i=0; i < tentacle_amount; i++){

			Vector3 tentacle_object_position = this.transform.position;

			tentacle_object_position.x = Random.Range(this.transform.position.x - GroundSize.x/2 + TentacleOffset, this.transform.position.x + GroundSize.x/2 - TentacleOffset);
			tentacle_object_position.z = Random.Range(this.transform.position.z - GroundSize.y/2 + TentacleOffset, this.transform.position.z + GroundSize.y/2 - TentacleOffset);
			tentacle_object_position.y = TentacleHeight;

			int tentacle_prefab = Random.Range(0,3);

			TentacleArray[i] = Instantiate(TentaclePrefabArray[tentacle_prefab],tentacle_object_position,Quaternion.identity) as GameObject;
			TentacleArray[i].transform.parent = this.transform;
			TentacleArray[i].name = this.name + "_tentacle_" + i;
			TentacleArray[i].renderer.material = TentacleObjectMaterial;

			TentacleArray[i].transform.RotateAround(TentacleArray[i].transform.position,Vector3.right,-90.0f);

			TentacleArray[i].transform.RotateAround(TentacleArray[i].transform.position,Vector3.up,Random.Range(0.0f,360.0f));
		}

		int puddle_amount = Random.Range(MinPuddle,MaxPuddle);

		for (int j=0; j < puddle_amount; j++){

			Vector3 puddle_object_position = this.transform.position;

			puddle_object_position.x = Random.Range(this.transform.position.x - GroundSize.x/2 + PuddleOffset, this.transform.position.x + GroundSize.x/2 - PuddleOffset);
			puddle_object_position.z = Random.Range(this.transform.position.z - GroundSize.y/2 + PuddleOffset, this.transform.position.z + GroundSize.y/2 - PuddleOffset);
			puddle_object_position.y = PuddleHeight;
			
			int puddle_prefab = Random.Range(0,2);
			
			PuddleArray[j] = Instantiate(PuddlePrefabArray[puddle_prefab],puddle_object_position,Quaternion.identity) as GameObject;
			PuddleArray[j].transform.parent = this.transform;
			PuddleArray[j].name = this.name + "_puddle_" + j;
			PuddleArray[j].renderer.material = PuddleObjectMaterial;

			var rb = PuddleArray[j].AddComponent< Rigidbody >();

			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

			rb.useGravity = false;
			rb.freezeRotation = true;
			rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			
			PuddleArray[j].transform.RotateAround(PuddleArray[j].transform.position,Vector3.right,-90.0f);

			PuddleArray[j].transform.RotateAround(PuddleArray[j].transform.position,Vector3.up,Random.Range(0.0f,360.0f));
		}

		for (int t=0; t < 3; t++){

			Vector3 totem_object_position = this.transform.position;
			
			totem_object_position.x = Random.Range(this.transform.position.x - GroundSize.x/2 + TotemOffset, this.transform.position.x + GroundSize.x/2 - TotemOffset);
			totem_object_position.z = Random.Range(this.transform.position.z - GroundSize.y/2 + TotemOffset, this.transform.position.z + GroundSize.y/2 - TotemOffset);
			totem_object_position.y = TotemHeight;
			
			int puddle_prefab = Random.Range(0,2);
			
			TotemArray[t] = Instantiate(TotemPrefab,totem_object_position,Quaternion.identity) as GameObject;
			TotemArray[t].transform.parent = this.transform;
			TotemArray[t].name = this.name + "_totem_" + t;
			TotemArray[t].renderer.material = TentacleObjectMaterial;

			var rb = TotemArray[t].AddComponent< Rigidbody >();

			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

			rb.useGravity = false;
			rb.freezeRotation = true;
			rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			
			TotemArray[t].transform.RotateAround(TotemArray[t].transform.position,Vector3.right,-90.0f);
			
			TotemArray[t].transform.RotateAround(TotemArray[t].transform.position,Vector3.up,Random.Range(0.0f,360.0f));
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
