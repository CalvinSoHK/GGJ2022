using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utility.MessageQueue;

namespace AudioManagement
{
    /// <summary>
    /// Listens for invoked events and passes audio to the right sources
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        //Data to be used for managing audio sources
        [SerializeField]
        private class AudioSourceInfo
        {
            [SerializeField]
            private AudioSource source;

            /// <summary>
            /// The audio source
            /// </summary>
            public AudioSource Source
            {
                get
                {
                    return source;
                }
            }

            public AudioSourceInfo(AudioSource _source)
            {
                source = _source;
            }
        }

        [Tooltip("Used to make more audio sources if necessary.")]
        [SerializeField]
        private GameObject sourcePrefab;

        private List<AudioSourceInfo> sourceList = new List<AudioSourceInfo>();

        [Tooltip("Minimum number of audio sources to maintain")]
        [SerializeField]
        private int minNumSources = 3;

        [Tooltip("Mixer to manipulate")]
        [SerializeField]
        private AudioMixer masterMixer;


        private void OnEnable()
        {
            AudioPlayEvent.PlayAudioEvent += HandleAudioClip;
            AudioMixerEvent.TransitionAudioMixerEvent += HandleMixer;
        }

        private void OnDisable()
        {
            AudioPlayEvent.PlayAudioEvent -= HandleAudioClip;
            AudioMixerEvent.TransitionAudioMixerEvent -= HandleMixer;
        }

        private void Awake()
        {
            for(int i = 0; i < minNumSources; i++)
            {
                AddNewSource();
            }
        }

        /// <summary>
        /// Adds a new source and returns it
        /// </summary>
        /// <returns></returns>
        private AudioSource AddNewSource()
        {
            //IF we can't find one add a new one
            GameObject prefab = Instantiate(sourcePrefab, transform);
            AudioSource source = prefab.GetComponent<AudioSource>();

            //Add new one to list
            sourceList.Add(new AudioSourceInfo(source));
            return source;
        }

        /// <summary>
        /// Plays audio from the given audio message object
        /// </summary>
        /// <param name="obj"></param>
        private void HandleAudioClip(AudioClipMessageObject obj)
        {
            AudioSource source = PickSource(obj);
            PlayAudioOnSource(source, obj);
        }


        /// <summary>
        /// Picks out the first audio source idling to play audio.
        /// If we can't find one, make a new one and return that.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private AudioSource PickSource(AudioClipMessageObject obj)
        {
            //First try to find one that is not playing something
            foreach(AudioSourceInfo info in sourceList)
            {
                if (!info.Source.isPlaying)
                {
                    return info.Source;
                }
            }

            return AddNewSource();
        }

        /// <summary>
        /// Plays the audio message object on the given target source
        /// </summary>
        /// <param name="targetSource"></param>
        /// <param name="obj"></param>
        private void PlayAudioOnSource(AudioSource targetSource, AudioClipMessageObject obj)
        {
            targetSource.outputAudioMixerGroup = obj.TargetGroup;
            targetSource.clip = obj.TargetClip;
            targetSource.Play();
        }

        private void HandleMixer(AudioMixerMessageObject obj)
        {
            //Takes in an array of snapshots with weights for all snapshots
            AudioMixerSnapshot[] snapshots = new AudioMixerSnapshot[1];
            snapshots[0] = obj.TargetSnapshot;

            float[] weights = new float[1];
            weights[0] = 1;

            masterMixer.TransitionToSnapshots(snapshots, weights, obj.TransitionTime);
        }

        private void Update()
        {

        }

        /// <summary>
        /// Cleans up sources
        /// </summary>
        private void CleanupSources()
        {

        }
    }  
}
