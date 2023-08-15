
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	//public static SoundManager instance;

	public List<AudioClip> sources;
	public AudioSource audioSource;
	
private void Awake()
	{
		
	}

	public void Start()
	{
		int x = sources.Count;
		AudioClip clip = sources[x];
		audioSource.clip = clip;
		audioSource.Play();
		
	}
}
