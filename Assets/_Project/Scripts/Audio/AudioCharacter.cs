using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCharacter : MonoBehaviour
{
    [SerializeField] private AudioSource footstepAudioSource = null;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] softSteps = null;
    [SerializeField] private AudioClip[] hardSteps = null;

    [Header("Steps")]
    [SerializeField] private float timer = 0.5f;
    private float stepsTimer;

    public void PlaySteps(GroundType groundType,float speedNormalized)
    {
        if (groundType == GroundType.None)
            return;
        
        stepsTimer += Time.fixedDeltaTime * speedNormalized;

        if(stepsTimer >= timer)
        {
            var steps = groundType == GroundType.Hard ? hardSteps : softSteps;
            int index = Random.Range(0, steps.Length);
            footstepAudioSource.PlayOneShot(steps[index]);
            stepsTimer = 0;
        }


    }


}
