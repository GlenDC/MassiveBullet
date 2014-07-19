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

    static GameObject[] GameBullets = new GameObject[0];

    public static GameObject[] GetBullets()
    {
        return GameBullets;
    }

    public int bulletCount { get; private set; }

    public float gameTime;

    public int gameScore { get; private set; }
    FibonacciObject timeScoreObject;

    Random random;

    Config
        config;

    public bool IsActive
    {
        get
        {
            return GameScript.GetBullets().Length > 0;
        }
    }

    void Start()
    {
        Initialize();

        config = GameScript.GetConfig();

        random = new Random();
    }

    void Update()
    {
        if( IsActive )
        {
            gameTime += Time.deltaTime;

            if( (int) gameTime >= timeScoreObject.GetIdentifier() )
            {
                gameScore += 25 * timeScoreObject.GetNextValue();
            }
        }

        if( Input.GetKey( KeyCode.Escape ) )
        {
            Quit();
        }
    }

    void Quit()
    {
        Application.LoadLevel( "start_menu" );
    }

    void Initialize()
    {
        bulletCount = 0;
        gameTime = 0;
        gameScore = 0;

        timeScoreObject = new FibonacciObject();

        GameBullets = new GameObject[0];
    }

    public void OnGameOver()
    {
        if( gameScore > config.highscore )
        {
            config.highscore = gameScore;
        }

        // "reset" game
        Initialize();

        var bullets = GameScript.GetBullets();
 
        for( int i = 0; i < bullets.Length; ++i )
        {
            Destroy( bullets[ i ].gameObject );
        }
    }

    public void AddBullet()
    {
        ++bulletCount;
        GameBullets = GameObject.FindGameObjectsWithTag( TAGS.BULLET );
    }

    public void RemoveBullet()
    {
        --bulletCount;
        GameBullets = GameObject.FindGameObjectsWithTag( TAGS.BULLET );
    }

    public void AddScore( int score )
    {
        gameScore += score;
    }

    public float GetRandomFloat( float min, float max )
    {
        return Random.Range( min, max );
    }

    public int GetRandomInt( int min, int max )
    {
        return Random.Range( min, max );
    }
}
