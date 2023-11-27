using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource Musictheme;    
    public AudioSource[] SFX;
    public AudioSource[] Voice;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Musictheme = GetComponent<AudioSource>();
    }
    public void PlayMusic(int index)
    {
        if (SFX[index].isPlaying) return;
        SFX[index].Play();
    }

    public void StopMusic(int index)
    {
        SFX[index].Stop();
    }

    public void PlayVoice(int index)
    {
        if (Voice[index].isPlaying) return;
        SFX[index].Play();
    }

    public void StopVoice(int index)
    {
        Voice[index].Stop();
    }
    private void Update()
    {               
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            Musictheme.Stop();
        }
    }
}
