using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField]
    private ESFXType _type;
    [SerializeField]
    private AudioSource _audio;

    public ESFXType Type { get => _type; }

    private void Awake()
    {
        if (!_audio)
        {
            _audio = GetComponent<AudioSource>();
        }
    }

    public void Play()
    {
        _audio.Play();
    }

    public void SetPitch(float pitch)
    {
        _audio.pitch = pitch;
    }

    public void Stop()
    {
        _audio.Stop();
    }
}
