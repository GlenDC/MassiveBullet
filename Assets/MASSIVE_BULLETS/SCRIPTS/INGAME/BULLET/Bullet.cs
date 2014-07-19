using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    const float SPEED = 2500.0f;

    const float MIN_DISTANCE = 4.5f;
    const float MAX_DISTANCE = 6.0f;

    const float MIN_ROTATION_TIME = 3.0f;
    const float MAX_ROTATION_TIME = 10.0f;

    [SerializeField]
        TrailRenderer trail;

    Rigidbody rigidBody;

    public bool IsDead;

    BULLET_STATE state;

    Transform player;

    GameScript gameScript;

    public GameObject PhysicsBullet;

    Vector3 RotationTime;
    float currentTiming;

    BULLET_TYPE type;
    public int groupID { get; protected set; }

    static int BULLET_COUNTER = 0;
    static int GROUP_COUNTER = 0;

    Vector3 direction, originalDirection, targetDirection;

    bool YFlipBlocked = false;

    public void SetDirection( Vector3 dir )
    {
        direction = dir;
        direction.Normalize();
    }

    void Start()
    {
        player = GameObject.FindWithTag( TAGS.PLAYER ).transform;
        gameScript = GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >();

        rigidBody = GetComponent< Rigidbody >();
        state = BULLET_STATE.GOING;

        direction = Vector3.zero;

        type = ( BULLET_TYPE ) BULLET_COUNTER;
        groupID = GROUP_COUNTER;

        YFlipBlocked = false;

        IsDead = false;

        if( ++BULLET_COUNTER >= ( int ) BULLET_TYPE.COUNT )
        {
            BULLET_COUNTER = 0;
            ++GROUP_COUNTER;
        }

        switch( type )
        {
            case BULLET_TYPE.RED:
            {
                trail.material = 
                    ( Material ) Instantiate( Resources.Load( "BULLET_TRAIL_RED_MATERIAL" ) );
            } break;

            case BULLET_TYPE.PURPLE:
            {
                trail.material = 
                    ( Material ) Instantiate( Resources.Load( "BULLET_TRAIL_PURPLE_MATERIAL" ) );
            } break;

            case BULLET_TYPE.YELLOW:
            {
                trail.material = 
                    ( Material ) Instantiate( Resources.Load( "BULLET_TRAIL_YELLOW_MATERIAL" ) );
            } break;
        }
    }

    void Update()
    {
        float
            distance,
            speed;

        if( direction == Vector3.zero )
        {
            direction = transform.forward;
            distance = 0.0f;
        }

        targetDirection = player.position - transform.position;
        distance = targetDirection.magnitude;
        targetDirection.Normalize();

        if( transform.localScale.x < 4.0f )
        {
            transform.localScale *= 1.0f + 5.0f * Time.deltaTime;
        }
        else
        {
            PhysicsBullet.active = true;
        }

        speed = SPEED;

        if( distance < MIN_DISTANCE )
        {
            speed *= Mathf.Clamp( distance / MIN_DISTANCE, 0.35f, 1.0f );
        }

        if( state == BULLET_STATE.GOING )
        {
            if( distance > MAX_DISTANCE )
            {
                state = BULLET_STATE.RETURNING;

                RotationTime = Vector3.zero;

                currentTiming = 0.0f;

                RotationTime.x = gameScript.GetRandomFloat( MIN_ROTATION_TIME, MAX_ROTATION_TIME );
                RotationTime.y = gameScript.GetRandomFloat( MIN_ROTATION_TIME, MAX_ROTATION_TIME );
                RotationTime.z = gameScript.GetRandomFloat( MIN_ROTATION_TIME, MAX_ROTATION_TIME );

                originalDirection = direction;
            }

            rigidBody.velocity = direction * speed * Time.deltaTime;
        }
        else // returning
        {
            currentTiming += Time.deltaTime;

            direction.x = Mathf.Lerp( originalDirection.x, targetDirection.x, currentTiming / RotationTime.x );
            direction.y = Mathf.Lerp( originalDirection.y, targetDirection.y, currentTiming / RotationTime.y );
            direction.z = Mathf.Lerp( originalDirection.z, targetDirection.z, currentTiming / RotationTime.z );

            rigidBody.velocity = direction * speed * Time.deltaTime;

            if( distance < MIN_DISTANCE )
            {
                state = BULLET_STATE.GOING;
            }
        }

        if( PhysicsBullet.transform.position.y < 1.0f && direction.y < 0.0f )
        {
            direction.y *= -1.0f;
        }
        else if( PhysicsBullet.transform.position.y < -1.0f )
        {
            Destroy( gameObject );
        }

        transform.LookAt( transform.position + direction );
    }
}
