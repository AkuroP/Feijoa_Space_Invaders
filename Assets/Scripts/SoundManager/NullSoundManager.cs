using UnityEngine;
using UnityEngine.Audio;

namespace Game.Script.SoundManager
{
    public class NullSoundManager : ISoundManager
    {
        public void PlaySound(AudioClip audioClip, AudioMixerGroup audioMixer = null){}
        public void PlayMusic(AudioClip audioClip, AudioMixerGroup audioMixer = null) {}

        public void StopMusic(){}
    }
}
