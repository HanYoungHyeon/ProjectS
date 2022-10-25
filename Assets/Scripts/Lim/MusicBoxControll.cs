using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxControll : MonoBehaviour
{
    private AudioSource audioSource;
    private int curClipNum;
    public AudioClip[] audioClip;

    private void OnEnable()
    {
        curClipNum = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip[curClipNum];
    }
    private void Start()
    {
        audioSource.Play();
    }
    private void Update()
    {
        MusicPlay();
    }
    private void MusicPlay()
    {
        if (audioSource.isPlaying)
            return;
        if (curClipNum + 1 >= audioClip.Length)
            curClipNum = 0;
        audioSource.clip = audioClip[curClipNum];
        audioSource.Play();
        curClipNum++;
    }
}
