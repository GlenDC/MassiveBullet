using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    Transform gameSpawn;
    GameScript gameScript;

    const float OFFSET_SCALE = 0.1f;

    void Start()
    {
        gameSpawn =
            GameObject.FindWithTag( TAGS.SPAWN ).transform;

        gameScript =
            GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >();

        SpawnPlayer();
    }

    void Update()
    {
        if( Input.GetButtonDown( "Fire" ) )
        {
            ShootBullet( new Vector3( 0.0f, 1.0f, 0.0f ) );
            ShootBullet( new Vector3( -1.0f, 0.0f, 0.0f ) );
            ShootBullet( new Vector3( 1.0f, 0.0f, 0.0f ) );
        }
    }

    void SpawnPlayer()
    {
        transform.position = gameSpawn.position;
    }

    public void OnGameOver()
    {
        SpawnPlayer();
        gameScript.OnGameOver();
    }

    void ShootBullet( Vector3 offset )
    {
        GameObject
            bullet;

        bullet = ( GameObject ) Instantiate( Resources.Load( "BULLET_PREFAB" ) );

        bullet.transform.rotation = transform.rotation;

        bullet.transform.position = transform.position;
        bullet.transform.position += transform.forward * offset.z * OFFSET_SCALE;
        bullet.transform.position += transform.right * offset.x * OFFSET_SCALE;
        bullet.transform.position += transform.up * offset.y * OFFSET_SCALE;

        bullet.name = "BULLET_" + gameScript.bulletCount;
        gameScript.AddBullet();

        bullet.tag = TAGS.BULLET;
    }
}
