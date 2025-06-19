using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreCalmMusic : MonoBehaviour
{
    public AudioClip calmMusicClip;

    void Start()
    {
        if (CalmMusicManager.Instance == null)
        {
            GameObject calmMusicObj = new GameObject("CalmMusicManager");
            CalmMusicManager calm = calmMusicObj.AddComponent<CalmMusicManager>();
            calm.calmMusicClip = calmMusicClip;
        }
    }
}
