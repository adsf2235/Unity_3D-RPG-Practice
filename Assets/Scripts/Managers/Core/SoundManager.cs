using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSource = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject() { name = "@Sound" };
            Object.DontDestroyOnLoad(root);
        }

        string[] soundName = System.Enum.GetNames(typeof(Define.Sound));
        for (int i = 0; i < soundName.Length - 1; i++)
        {
            GameObject go = new GameObject() { name = soundName[i] };
            _audioSource[i] = go.AddComponent<AudioSource>();
            go.transform.parent = root.transform;
        }

        _audioSource[(int)Define.Sound.Bgm].loop = true;
    }

    public void Play( string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {

        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip ==null)
        {
            return;
        }
        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSource[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();

        }
        else
        {
            AudioSource audioSource = _audioSource[(int)Define.Sound.Effect];
            audioSource.PlayOneShot(audioClip);

        }
    }

    public AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        AudioClip audioClip;
        if (path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }
        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
          
        }
        return audioClip;
    }

    public void Clear()
    {
        _audioClips.Clear();
        foreach (AudioSource audioSource in _audioSource)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
        
    }
}
