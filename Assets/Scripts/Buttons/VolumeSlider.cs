using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mainAudioMixer;
    public Slider MainVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        //    AudioMixer.SetFloat("mainVolume", 20.0f);

        OnMainVolumeChange();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMainVolumeChange()
    {
        float newVolume = MainVolumeSlider.value;
        if (newVolume < 0)
        {
            newVolume = -80;
           
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;
        }

        mainAudioMixer.SetFloat("MainVolume", newVolume);
        Debug.Log(newVolume);
    }
}
