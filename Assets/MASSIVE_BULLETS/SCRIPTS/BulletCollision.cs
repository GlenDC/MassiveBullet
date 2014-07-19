using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour
{
    public Bullet BulletInfo { get; private set; }

    void Start()
    {
        BulletInfo = transform.parent.gameObject.GetComponent< Bullet >();
        BulletInfo.PhysicsBullet.active = false;
    }

    void OnCollisionEnter( Collision collision )
    {
        BulletInfo.ReverseDirection();
    }

    void OnTriggerEnter( Collider other )
    {
        if( other.gameObject.tag == TAGS.PLAYER_COLLISION )
        {
            other.transform.parent.gameObject.GetComponent< Player >().OnGameOver();
        }
        else if( other.gameObject.tag == TAGS.BULLET_COLLISION )
        {
            Bullet
                otherBullet;

            otherBullet = other.gameObject.transform.parent.GetComponent< Bullet >();

            if( !otherBullet.IsDead && !BulletInfo.IsDead && BulletInfo.groupID != otherBullet.groupID )
            {
				PlayBulletFeedback();

                PlayExplosionSound( otherBullet.type, BulletInfo.type );

				Destroy( other.transform.parent.gameObject );
                Destroy( transform.parent.gameObject );

                GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >().RemoveBullet();
            }
        }
    }

	void PlayBulletFeedback()
    {
		GameObject score_popup_object = ( GameObject ) Instantiate( Resources.Load( "ScorePopUpPrefab" ) );

		score_popup_object.transform.position = this.transform.position;

		score_popup_object.GetComponent<ScorePopUpScript>().SetUpScorePopUp(1,0);
	}

    void PlayExplosionSound( BULLET_TYPE a, BULLET_TYPE b )
    {
        GameObject
            soundEffect;
        AmmoShotAudio
            audio;

        soundEffect = ( GameObject ) Instantiate( Resources.Load( "SOUND_NODE" ) );
        soundEffect.transform.position = transform.position;

        audio = soundEffect.GetComponent< AmmoShotAudio >();

        if( a == BULLET_TYPE.YELLOW && b == BULLET_TYPE.YELLOW )
        {
            audio.SetAudioSource( 10 );
        }
        else
        {
            audio.SetAudioSource( ( (int) a + (int) b ) * 2 );
        }
    }
}
