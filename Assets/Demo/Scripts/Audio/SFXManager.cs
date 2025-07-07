using System.Collections.Generic;
using UnityEngine;

public class SFXManager : SingletonBehaviour<SFXManager>
{
    [SerializeField]
    private List<SFXPlayer> _SFXPlayers = new List<SFXPlayer>();

    public List<SFXPlayer> SFXPlayers { get => _SFXPlayers; }

    public void PlayAudio(ESFXType type)
    {
        SFXPlayer player = _SFXPlayers.Find(item => item.Type == type);
        player?.Play();
    }

    public void PlayAudioWithRandomPitch(ESFXType type, float minPitch, float maxPitch)
    {
        SFXPlayer player = _SFXPlayers.Find(item => item.Type == type);
        player?.SetPitch(Random.Range(minPitch, maxPitch));
        PlayAudio(type);
    }
}
