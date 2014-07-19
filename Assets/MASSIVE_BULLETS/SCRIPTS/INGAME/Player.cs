using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [ SerializeField ] GameObject
		HUDCamera;

	Transform gameSpawn;
    GameScript gameScript;

    const float OFFSET_SCALE = 0.1f;

    Camera
        mainCamera;

    const float SHOOT_DELAY_TIME = 0.65f;
    float shootTimer;

    void Start()
    {
        gameSpawn =
            GameObject.FindWithTag( TAGS.SPAWN ).transform;

        gameScript =
            GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >();

        SpawnPlayer();

        mainCamera =
            GameObject.FindWithTag( TAGS.MAIN_CAMERA ).GetComponent< Camera >();

        shootTimer = 0.0f;
    }

    void Update()
    {
        if( shootTimer <= SHOOT_DELAY_TIME )
        {
            shootTimer += Time.deltaTime;
        }
        else if( gameScript.GameIsActive && Input.GetButtonDown( "Fire" ) )
        {
            ShootBullet( new Vector3( 0.0f, 1.0f, 1.0f ) );
            ShootBullet( new Vector3( -1.0f, 0.0f, 1.0f ) );
            ShootBullet( new Vector3( 1.0f, 0.0f, 1.0f ) );

			HUDCamera.GetComponent<HudCameraScript>().ShootPistol();

            shootTimer = 0.0f;

            PlaySound( 1, 0.5f );
        }
    }

    void SpawnPlayer()
    {
        transform.position = gameSpawn.position;

        PlaySound( 11, 0.35f );
    }

    public void OnGameOver()
    {
        SpawnPlayer();
        shootTimer = 0.0f;
        gameScript.OnGameOver();

        PlaySound( 9 );
    }

    void PlaySound( int audio, float volume = 1.0f )
    {
        var soundEffect = ( GameObject ) Instantiate( Resources.Load( "SOUND_NODE" ) );
        soundEffect.transform.position = transform.position;
        soundEffect.GetComponent< AudioSource >().volume = volume;

        var audioScript = soundEffect.GetComponent< AmmoShotAudio >();
        audioScript.SetAudioSource( audio );
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
