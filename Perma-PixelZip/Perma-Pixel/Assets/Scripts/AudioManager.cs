using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    List<AudioSource> sources;

    public void PlayAt(int x)
    {
        if(x < sources.Count && !sources[x].isPlaying)
            sources[x].Play();
    }

    public void StopPlayAt(int x)
    {
        if (x < sources.Count && sources[x].isPlaying)
            sources[x].Stop();
    }
}
