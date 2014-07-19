using UnityEngine;
using System.Collections;

public class AmmoShotAudio : MonoBehaviour
{
    AudioSource
        audioSource;
    float
        liveTime,
        currentTime;
    bool
        isOK;

    void Start()
    {
        isOK = false;
    }

    public void SetAudioSource( int audio, float time = 1.5f )
    {
        audio = (int) Mathf.Clamp( (float) audio, 1.0f, 11.0f );

        audioSource = GetComponent< AudioSource >();
        audioSource.clip = (AudioClip) Instantiate( Resources.Load( "sp" + audio ) );
        audioSource.Play();

        currentTime = 0.0f;
        liveTime = time;

        isOK = true;
    }

    void Update()
    {
        if( isOK )
        {
            currentTime += Time.deltaTime;
            if( currentTime > liveTime )
            {
                Destroy( gameObject );
            }
        }
    }
}
