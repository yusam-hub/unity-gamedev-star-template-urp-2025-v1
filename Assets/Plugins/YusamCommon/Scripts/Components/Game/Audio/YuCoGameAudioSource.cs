using UnityEngine;
using UnityEngine.Audio;

namespace YusamCommon
{
    [DefaultExecutionOrder(-10001)]
    public class YuCoGameAudioSource : YuCoSingletonDontDestroyOnLoad<YuCoGameAudioSource>
    {
        [SerializeField]
        private Transform musicPoolContainer;
        
        [SerializeField] 
        private int musicPoolSize = 5;
        
        [SerializeField]
        private Transform effectPoolContainer;
        
        [SerializeField] 
        private int effectPoolSize = 50;
        
        public AudioMixerGroup masterMixerGroup;
        public AudioMixerGroup musicMixerGroup;
        public AudioMixerGroup effectMixerGroup; 
        
        public string exposedMasterVolume = "MasterVolume";
        public string exposedMusicVolume = "MusicVolume";
        public string exposedEffectVolume = "EffectVolume";

        private AudioSource testMusic;
        private AudioSource testEffect;
        private IYuCoAudioSourcePool _musicAudioSourcePool;
        private IYuCoAudioSourcePool _effectAudioSourcePool;
        protected override void AwakeOnce()
        {
            base.AwakeOnce();
            
            _musicAudioSourcePool = new YuCoAudioSourcePool(musicPoolContainer, musicMixerGroup);
            _musicAudioSourcePool.Init(musicPoolSize);
            
            _effectAudioSourcePool = new YuCoAudioSourcePool(effectPoolContainer, effectMixerGroup);
            _effectAudioSourcePool.Init(effectPoolSize);

            var go1 = new GameObject("TestMusicAudioSource");
            testMusic = go1.AddComponent<AudioSource>();
            testMusic.loop = true;
            testMusic.playOnAwake = false;
            testMusic.outputAudioMixerGroup = musicMixerGroup;
            go1.transform.SetParent(transform);
            
            var go2 = new GameObject("TestEffectAudioSource");
            testEffect = go2.AddComponent<AudioSource>();
            testEffect.loop = true;
            testEffect.playOnAwake = false;
            testEffect.outputAudioMixerGroup = effectMixerGroup;
            go2.transform.SetParent(transform);
        }

        public AudioSource GetTestMusic()
        {
            return testMusic;
        }
        
        public AudioSource GetTestEffect()
        {
            return testEffect;
        }
        
        public void MusicPlay(AudioClip audioClip, bool loop = false, Vector3 position = default)
        {
            var source = _musicAudioSourcePool.Rent();
            source.transform.position = position;
            source.clip = audioClip;
            source.loop = loop;
            source.Play();
            StartCoroutine(MusicReturn(source));
        }
        
        public void EffectPlay(AudioClip audioClip, Vector3 position = default)
        {
            var source = _effectAudioSourcePool.Rent();
            source.transform.position = position;
            source.clip = audioClip;
            source.Play();
            StartCoroutine(EffectReturn(source));
        }

        private System.Collections.IEnumerator MusicReturn(AudioSource source)
        {
            yield return new WaitForSeconds(source.clip.length);
            source.Stop();
            _musicAudioSourcePool.Return(source);
            if (source.loop)
            {
                MusicPlay(source.clip, source.loop, source.transform.position);
            }
        }
        
        private System.Collections.IEnumerator EffectReturn(AudioSource source)
        {
            yield return new WaitForSeconds(source.clip.length);
            source.Stop();
            _effectAudioSourcePool.Return(source);
        }
        
        public void SetExposedMaster(float volume)
        {
            masterMixerGroup.audioMixer.SetFloat(exposedMasterVolume, Mathf.Clamp(volume, -80f, 0f));
        }
        
        public void SetExposedMusic(float volume)
        {
            musicMixerGroup.audioMixer.SetFloat(exposedMusicVolume, Mathf.Clamp(volume, -80f, 0f));
        }
        
        public void SetExposedEffect(float volume)
        {
            effectMixerGroup.audioMixer.SetFloat(exposedEffectVolume, Mathf.Clamp(volume, -80f, 0f));
        }
        
        public float GetExposedMaster()
        {
            masterMixerGroup.audioMixer.GetFloat(exposedMasterVolume, out var result);
            return result;
        }
        
        public float GetExposedMusic()
        {
            masterMixerGroup.audioMixer.GetFloat(exposedMusicVolume, out var result);
            return result;
        }
        
        public float GetExposedEffect()
        {
            masterMixerGroup.audioMixer.GetFloat(exposedEffectVolume, out var result);
            return result;
        }
    }
 
}