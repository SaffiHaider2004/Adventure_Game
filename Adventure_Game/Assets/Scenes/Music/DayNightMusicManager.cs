using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DayNightMusicManager : MonoBehaviour
{
    public TimeController timeController;
    public AudioClip dayMusic;
    public AudioClip nightMusic;
    public float fadeDuration = 1f;

    private AudioSource audioSource;
    private bool isCurrentlyNight;
    private Coroutine fadeCoroutine;

    void Start()
    {
        if  (CalmMusicManager.Instance != null) 
        { 
                Destroy(CalmMusicManager.Instance.gameObject);
        }


        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 1f;

        // Start coroutine to delay music setup until TimeController is ready
        StartCoroutine(InitializeMusic());
    }
    private IEnumerator InitializeMusic()
    {
        // Wait one frame to ensure TimeController has initialized
        yield return null;

        UpdateIsNightFlag();

        audioSource.clip = isCurrentlyNight ? nightMusic : dayMusic;
        audioSource.Play();
    }
    void Update()
    {
        if (timeController == null) return;

        TimeSpan currentTime = timeController.GetCurrentTime().TimeOfDay;
        TimeSpan nightStart = TimeSpan.FromHours(19); // 7 PM
        TimeSpan nightEnd = TimeSpan.FromHours(timeController.sunriseHour);
        bool currentlyNight = currentTime >= nightStart || currentTime < nightEnd;

        if (currentlyNight != isCurrentlyNight)
        {
            isCurrentlyNight = currentlyNight;
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeToNewClip(isCurrentlyNight ? nightMusic : dayMusic));
        }
    }

    private void UpdateIsNightFlag()
    {
        TimeSpan currentTime = timeController.GetCurrentTime().TimeOfDay;
        TimeSpan nightStart = TimeSpan.FromHours(19); // 7 PM
        TimeSpan nightEnd = TimeSpan.FromHours(timeController.sunriseHour);
        isCurrentlyNight = currentTime >= nightStart || currentTime < nightEnd;
    }

    private IEnumerator FadeToNewClip(AudioClip newClip)
    {
        // Fade out
        float startVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = 0f;
        audioSource.Stop();

        // Change music
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = 1f;
    }
}