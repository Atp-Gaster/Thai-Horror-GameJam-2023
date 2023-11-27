using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class withMusicPlayer : MonoBehaviour
{
    [SerializeField] private SerializableKeyPair<string, AudioClip>[] voices = default;
    public AudioSource source;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public bool PlayVoice(string name, float volume = 1)
    {
        foreach (var a in voices)
        {
            if (a.Key == name)
            {
                source.PlayOneShot(a.Value, volume);
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class SerializableKeyPair<TKey, TValue>
{
    [SerializeField] private TKey key;
    [SerializeField] private TValue value;

    public TKey Key => key;
    public TValue Value => value;
}