using UnityEngine;
using UnityEngine.Audio;

namespace Game.Script.SoundManager
{
    public interface ISoundManager
    {
        void PlaySound(AudioClip audioClip, AudioMixerGroup audioMixer = null);

        void PlayMusic(AudioClip audioClip, AudioMixerGroup audioMixer = null);

        void StopMusic();
    }
}
