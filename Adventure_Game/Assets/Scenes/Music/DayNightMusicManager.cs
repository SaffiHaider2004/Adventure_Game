using System;
using System.Collections;
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

    private TimeSpan nightStart;
    private TimeSpan nightEnd;

    void Start()
    {
        if (CalmMusicManager.Instance != null)
        {
            Destroy(CalmMusicManager.Instance.gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 1f;

        // Pre-cache night start time
        nightStart = TimeSpan.FromHours(19);

        StartCoroutine(InitializeMusic());
    }

    private IEnumerator InitializeMusic()
    {
        yield return null; // Wait one frame

        if (timeController == null)
        {
            Debug.LogWarning("TimeController is missing.");
            yield break;
        }

        nightEnd = TimeSpan.FromHours(timeController.sunriseHour);
        UpdateIsNightFlag();

        audioSource.clip = isCurrentlyNight ? nightMusic : dayMusic;
        audioSource.Play();
    }

    void Update()
    {
        if (timeController == null) return;

        TimeSpan currentTime = timeController.GetCurrentTime().TimeOfDay;
        bool currentlyNight = currentTime >= nightStart || currentTime < nightEnd;

        if (currentlyNight != isCurrentlyNight)
        {
            isCurrentlyNight = currentlyNight;

            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeToNewClip(isCurrentlyNight ? nightMusic : dayMusic));
        }
    }

    private void UpdateIsNightFlag()
    {
        TimeSpan currentTime = timeController.GetCurrentTime().TimeOfDay;
        nightEnd = TimeSpan.FromHours(timeController.sunriseHour);
        isCurrentlyNight = currentTime >= nightStart || currentTime < nightEnd;
    }

    private IEnumerator FadeToNewClip(AudioClip newClip)
    {
        float startVolume = audioSource.volume;

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
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
