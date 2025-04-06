using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace YusamCommon
{
    public sealed class YuCoAudioSourcePool : YuCoObject, IYuCoAudioSourcePool
    {
        private readonly Transform _container;
        private readonly AudioMixerGroup _audioMixerGroup;
        private readonly Queue<AudioSource> _queue = new();

        public YuCoAudioSourcePool(Transform container, AudioMixerGroup audioMixerGroup)
        {
            _container = container;
            _audioMixerGroup = audioMixerGroup;
        }

        public void Init(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var audioSource = DoCreate();
                _queue.Enqueue(audioSource);
            }
        }

        private AudioSource DoCreate()
        {
            var go = new GameObject($"AudioSource_{_container.name}");
            go.transform.SetParent(_container);
            var audioSource = go.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.outputAudioMixerGroup = _audioMixerGroup;
            OnCreate(audioSource);
            return audioSource;
        }

        public AudioSource Rent()
        {
            if (!_queue.TryDequeue(out var audioSource))
            {
                audioSource = DoCreate();
            }

            OnRent(audioSource);
            return audioSource;
        }

        public void Return(AudioSource audioSource)
        {
            if (!_queue.Contains(audioSource))
            {
                OnReturn(audioSource);
                _queue.Enqueue(audioSource);
            }
        }

        public void Clear()
        {
            foreach (var audioSource in _queue)
            {
                OnDestroy(audioSource);
                Object.Destroy(audioSource);
            }

            _queue.Clear();
        }

        private void OnCreate(AudioSource audioSource)
        {
            audioSource.gameObject.SetActive(false);
        }

        private void OnDestroy(AudioSource audioSource)
        {
        }

        private void OnRent(AudioSource audioSource)
        {
            audioSource.gameObject.SetActive(true);
        }

        private void OnReturn(AudioSource audioSource)
        {
            audioSource.gameObject.SetActive(false);
            audioSource.transform.SetParent(_container);
        }
    }
}