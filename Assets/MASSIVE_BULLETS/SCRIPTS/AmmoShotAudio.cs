using UnityEngine;
using System.Collections;

public class AmmoShotAudio : MonoBehaviour
{
    AudioSource
        audioSource;
    float
        liveTime,
        currentTime;

    void Start()
    {
        liveTime = 5.0f;
    }

    public void SetAudioSource( int audio, float time = 1.5f )
    {
        audio = (int) Mathf.Clamp( (float) audio, 1.0f, 11.0f );

        audioSource = GetComponent< AudioSource >();
        audioSource.clip = (AudioClip) Instantiate( Resources.Load( "sp" + audio ) );
        audioSource.Play();

        currentTime = 0.0f;
        liveTime = time;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if( currentTime > liveTime )
        {
            Destroy( gameObject );
        }
    }
}
