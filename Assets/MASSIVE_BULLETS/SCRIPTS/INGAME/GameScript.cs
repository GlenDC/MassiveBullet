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

    void Start()
    {
        if( GameObject.FindWithTag( TAGS.CONFIG ) == null )
        {
            GameObject
                config;

            config = ( GameObject ) Instantiate( Resources.Load( "CONFIG" ) );
            config.name = "_CONFIG";
        }
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
        Debug.Log( ":TODO: GameOver!" );
    }
}
