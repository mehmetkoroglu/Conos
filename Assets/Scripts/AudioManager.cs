using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip bulletSound,deadSound;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        bulletSound = Resources.Load<AudioClip>("hit");
        deadSound = Resources.Load<AudioClip>("dead2");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    public static void soundPlay(string soundType){

        switch (soundType)
        {
            case "hit":
                audioSource.PlayOneShot(bulletSound);
                break;
            case "dead2":
                audioSource.pitch = 1;
                audioSource.volume = 1;
                audioSource.PlayOneShot(deadSound);
                
                break;
            default:
                break;
        }
    }
}
