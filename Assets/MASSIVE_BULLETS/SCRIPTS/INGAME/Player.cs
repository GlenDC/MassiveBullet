using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    Transform gameSpawn;
    GameScript gameScript;

    const float OFFSET_SCALE = 0.1f;

    Camera
        mainCamera;

    void Start()
    {
        gameSpawn =
            GameObject.FindWithTag( TAGS.SPAWN ).transform;

        gameScript =
            GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >();

        SpawnPlayer();

        mainCamera =
            GameObject.FindWithTag( TAGS.MAIN_CAMERA ).GetComponent< Camera >();
    }

    void Update()
    {
        if( Input.GetButtonDown( "Fire" ) )
        {
            ShootBullet( new Vector3( 0.0f, 1.0f, 1.0f ) );
            ShootBullet( new Vector3( -1.0f, 0.0f, 1.0f ) );
            ShootBullet( new Vector3( 1.0f, 0.0f, 1.0f ) );
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

        bullet.transform.rotation = mainCamera.transform.rotation;

        bullet.transform.position = mainCamera.transform.position;
        bullet.transform.position += mainCamera.transform.forward * offset.z * OFFSET_SCALE;
        bullet.transform.position += mainCamera.transform.right * offset.x * OFFSET_SCALE;
        bullet.transform.position += mainCamera.transform.up * offset.y * OFFSET_SCALE;

        bullet.name = "BULLET_" + gameScript.bulletCount;
        gameScript.AddBullet();

        bullet.GetComponent< Bullet >().SetDirection( bullet.transform.position - mainCamera.transform.position );

        bullet.tag = TAGS.BULLET;
    }
}
