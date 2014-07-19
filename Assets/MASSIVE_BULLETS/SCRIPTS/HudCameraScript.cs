using UnityEngine;
using System.Collections;

public class HudCameraScript : MonoBehaviour
{
    [ SerializeField ]
    GameObject
        PistolPrefab;

    GameObject
        PistolObject;

    [ SerializeField ]
    Vector3
        PistolPosition;

    [ SerializeField ]
    Quaternion
        PistolOrientation;

    [ SerializeField ]
    Texture2D
        CrosshairTexture,
        BulletWarningTexture;

    Config
        configScript;

    Camera
        mainCamera;

    GameScript
        gameScript;

    protected int HSEH { get { return Screen.height / 2; } }
    protected int HSEW { get { return Screen.width / 2; } }

    void Start()
    {
        configScript = GameScript.GetConfig();

        mainCamera =
            GameObject.FindWithTag( TAGS.MAIN_CAMERA ).GetComponent< Camera >();

        gameScript =
            GameObject.FindWithTag( TAGS.WORLD ).GetComponent< GameScript >();

        SetUpHUDCamera(
            configScript.HandState == HAND_STATE.RIGHT 
            );
    }

    public void SetUpHUDCamera( bool playerIsRightHanded )
    {
        if ( !playerIsRightHanded )
        {
            PistolPosition.x -= PistolPosition.x * 2;
            PistolOrientation.x += 2 * Mathf.PI;
        }

        PistolObject =
            Instantiate(
                PistolPrefab,
                Vector3.zero,
                Quaternion.identity
                ) as GameObject;

        PistolObject.transform.parent = transform;
        PistolObject.name = "HUD_PistolObject";

        PistolObject.transform.localPosition = PistolPosition;
        
        PistolObject.GetComponent<PistolObjectScript>().SetUpPistolObject();
    }

	public void ShootPistol(){

		//PistolObject.GetComponent<Animator>().Play();
	}

    void OnGUI()
    {
        var bullets = GameObject.FindGameObjectsWithTag( TAGS.BULLET );
 
        for( int i = 0; i < bullets.Length; ++i )
        {
            DrawBulletWarning( bullets[ i ].transform, bullets[i].renderer );
        }

        GUI.DrawTexture(
            new Rect( ( Screen.width / 2 ) - 25, ( Screen.height / 2 ) - 25, 50, 50 ),
            CrosshairTexture
            );

        if( gameScript.IsActive )
        {
            GUI.TextField(
                new Rect( 10, 10, 200, 20 ),
                "Score: " + gameScript.gameScore
                );
        }
        else
        {
            GUI.TextField(
                new Rect( Screen.width / 2 - 130, Screen.height / 2 + 50, 260, 20),
                "Fire some massive bullets and join the fun!"
                );

            GUI.TextField(
                new Rect( Screen.width / 2 - 130, Screen.height / 2 + 100, 260, 20),
                "Highscore: " + configScript.highscore
                );
        }
    }

    void DrawBulletWarning( Transform bullet, Renderer bulletRenderer )
    {
        if( !bulletRenderer.isVisible )
        {
            Vector3
                direction;
            float
                distance;

            direction = bullet.position - mainCamera.transform.position;
            distance = direction.magnitude;

            direction.Normalize();

            if( distance > 1.0f && Vector3.Dot( mainCamera.transform.forward, direction ) <= 0 )
            {
                Vector3
                    screenPosition = mainCamera.WorldToScreenPoint( bullet.position );

                if( screenPosition.x <= 0 || screenPosition.x >= Screen.width ||
                    screenPosition.y <= 0 || screenPosition.y >= Screen.height )
                {
                    int
                        dimension,
                        half_dimension;

                    distance = Mathf.Clamp( distance / 50.0f, 0.05f, 1.0f );
                    distance = 1.0f - distance;

                    dimension = ( int ) ( 50.0f * distance );
                    dimension += 15;
                    half_dimension = dimension / 2;

                    screenPosition.x = Mathf.Clamp(
                        screenPosition.x,
                        half_dimension,
                        Screen.width - half_dimension
                        );

                    screenPosition.x = Screen.width - screenPosition.x;

                    screenPosition.y = Mathf.Clamp(
                        screenPosition.y,
                        half_dimension,
                        Screen.height - half_dimension
                        );

                    screenPosition.y = Screen.height - screenPosition.y;

                    GUI.DrawTexture(
                        new Rect(
                            screenPosition.x - half_dimension,
                            screenPosition.y - half_dimension,
                            dimension,
                            dimension
                            ),
                        BulletWarningTexture
                        );
                }
            }
        }
    }
}
