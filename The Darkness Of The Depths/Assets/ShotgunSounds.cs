using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSounds : MonoBehaviour
{
    public AudioClip[] Shot;
    public AudioClip[] Reload;
    public AudioClip[] Empty;

    public void Start()
    {
        AudioManager.ssShot = Shot;
        AudioManager.ssReload = Reload;
        AudioManager.ssEmpty = Empty;
    }
}
