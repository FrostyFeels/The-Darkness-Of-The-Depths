using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public Weapon weapon;
    public WaveManager wave;
    public Text Ammo;
    public Text Health;
    public Text Kills;
    public Text Wave;



    // Update is called once per frame
    void Update()
    {
        Ammo.text = weapon.ammo.ToString() + " / " + weapon.maxAmmo.ToString();
        Wave.text = "Wave: " + wave.waveNumber.ToString() + " / " + wave.maxWave.ToString();
        Kills.text = "Kills: " +  wave.kill.ToString() + " / " + wave.killsNeeded.ToString(); 
    }
}
