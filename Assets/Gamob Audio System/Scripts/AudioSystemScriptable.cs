using System;
using UnityEngine;

namespace Gamob
{   
    [Serializable]
    public enum MusicAudios
    {
        NoneBGMusic = 0,
        MusicSample = 1,
    }

    [Serializable]
    public enum SFXAudios
    {
        NoneSFX = 0,
        SFXSample = 1,
    }

    [Serializable]
    public enum UIAudios
    {
        NoneUIAudio = 0,
        UISample = 1,
    }

    [CreateAssetMenu(fileName = "New Audio Scriptable", menuName = "Gamob/AudioScriptable")]
    public class AudioSystemScriptable : ScriptableObject
    {
        public SerializedDictionary<MusicAudios, AudioClip> MusicAudios = new SerializedDictionary<MusicAudios, AudioClip>();
        public SerializedDictionary<SFXAudios, AudioClip> SFXAudios = new SerializedDictionary<SFXAudios, AudioClip>();
        public SerializedDictionary<UIAudios, AudioClip> UIAudios = new SerializedDictionary<UIAudios, AudioClip>();
    }

    

}
