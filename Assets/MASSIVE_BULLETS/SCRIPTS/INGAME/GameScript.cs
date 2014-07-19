using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour
{
    public static Config GetConfig()
    {
        GameObject
            config;

        config = GameObject.FindWithTag( TAGS.CONFIG );

        if( config == null )
        {
            config = ( GameObject ) Instantiate( Resources.Load( "CONFIG" ) );
            config.name = "_CONFIG";
        }

        return config.GetComponent< Config >();
    }

    public int bulletCount { get; private set; }

    void Start()
    {
        bulletCount = 0;
    }

    void Update()
    {
        if( Input.GetKey( KeyCode.Escape ) )
        {
            Quit();
        }
    }

    void Quit()
    {
        Application.LoadLevel( "start_menu" );
    }

    public void OnGameOver()
    {
        bulletCount = 0;

        var bullets =
            GameObject.FindGameObjectsWithTag( TAGS.BULLET );
 
        for( int i = 0; i < bullets.Length; ++i )
        {
            Destroy( bullets[ i ].gameObject );
        }
    }

    public void AddBullet()
    {
        ++bulletCount;
    }
}
