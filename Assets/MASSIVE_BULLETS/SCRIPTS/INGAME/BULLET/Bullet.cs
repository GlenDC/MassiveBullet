using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    const float SPEED = 1000.0f;

    const float MIN_DISTANCE = 0.5f;
    const float MAX_DISTANCE = 10.0f;

    const float MIN_ROTATION_TIME = 3.0f;
    const float MAX_ROTATION_TIME = 10.0f;

    Vector3 Velocity
        { get { return transform.forward * SPEED * Time.deltaTime; } }

    Rigidbody rigidBody;

    BULLET_STATE state;

    Transform player;

    GameScript gameScript;

    Vector3 RotationTime;
    float currentTiming;

    void Start()
    {
        player = GameObject.FindWithTag( TAGS.PLAYER ).transform;
        gameScript = GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >();

        rigidBody = GetComponent< Rigidbody >();
        state = BULLET_STATE.GOING;
    }

    void Update()
    {
        Vector3
            direction;
        float
            distance;

        direction = transform.position - player.position;
        distance = direction.magnitude;
        direction.Normalize();

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
            }

            rigidBody.velocity = Velocity;
        }
        else // returning
        {
            Vector3
                rotation_speed,
                target_direction;

            target_direction = direction * -1.0f;

            currentTiming += Time.deltaTime;

            direction.x = Mathf.Lerp( direction.x, target_direction.x, currentTiming / RotationTime.x );
            direction.y = Mathf.Lerp( direction.y, target_direction.y, currentTiming / RotationTime.y );
            direction.z = Mathf.Lerp( direction.z, target_direction.z, currentTiming / RotationTime.z );

            transform.rotation = Quaternion.LookRotation( transform.position + direction );

            if( distance < MIN_DISTANCE )
            {
                state = BULLET_STATE.GOING;
            }

            rigidBody.velocity = direction * SPEED * Time.deltaTime;
        }

    }
}
