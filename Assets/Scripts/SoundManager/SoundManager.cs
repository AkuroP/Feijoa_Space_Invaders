using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Script.SoundManager
{
    public class SoundManager : MonoBehaviour, ISoundManager
    {
        public void PlaySound(AudioClip audioClip, AudioMixerGroup audioMixer = null)
        {
            if (audioClip == null) return;
            
            if (gameObject.GetComponent<AudioSource>() == null)
            {
                gameObject.AddComponent<AudioSource>();   
            }

            var audioSource = gameObject.GetComponent<AudioSource>();
            if (audioMixer != null) audioSource.outputAudioMixerGroup = audioMixer;
            audioSource.PlayOneShot(audioClip);
        }

        public void PlayMusic(AudioClip audioClip, AudioMixerGroup audioMixer = null)
        {
            if (gameObject.GetComponent<AudioSource>() == null)
            {
                gameObject.AddComponent<AudioSource>();
            }

            var audioSource = gameObject.GetComponent<AudioSource>();
            if (audioMixer != null) audioSource.outputAudioMixerGroup = audioMixer;
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        
        public void StopMusic()
        {
            var audioSource = gameObject?.GetComponent<AudioSource>();
            if (audioSource.clip == null) return;
            audioSource.Pause();
        }
    }
}
