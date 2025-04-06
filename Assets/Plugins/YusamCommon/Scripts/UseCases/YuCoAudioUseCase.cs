using UnityEngine;

namespace YusamCommon
{
    public static class YuCoAudioUseCase
    {
        public static void PlayClipAtPointAsMainCamera(AudioClip audioClip)
        {
            var pos = Vector3.zero;
            if (Camera.main)
            {
                pos = Camera.main.transform.position;
            }
            AudioSource.PlayClipAtPoint(audioClip, pos);      
        }
        
        public static void PoolEffectPlay(AudioClip audioClip, Vector3 position = default)
        {
            YuCoGameAudioSource.Instance.EffectPlay(audioClip, position); 
        }
        public static void PoolMusicPlay(AudioClip audioClip, bool loop = false, Vector3 position = default)
        {
            YuCoGameAudioSource.Instance.MusicPlay(audioClip, loop, position);  
        }
        
        public static AudioSource GetTestMusic()
        {
            return YuCoGameAudioSource.Instance.GetTestMusic();  
        }
        
        public static AudioSource GetTestEffect()
        {
            return YuCoGameAudioSource.Instance.GetTestEffect();  
        }
        
        /*
         * volume -80 : 0
         */
        public static void SetExposedMaster(float volume)
        {
            YuCoGameAudioSource.Instance.SetExposedMaster(volume);
        }
        
        /*
         * volume -80 : 0
         */
        public static void SetExposedMusic(float volume)
        {
            YuCoGameAudioSource.Instance.SetExposedMusic(volume);
        }
        
        /*
         * volume -80 : 0
         */
        public static void SetExposedEffect(float volume)
        {
            YuCoGameAudioSource.Instance.SetExposedEffect(volume);
        }
        
        /*
         * return -80 : 0
         */
        public static float GetExposedMaster()
        {
            return YuCoGameAudioSource.Instance.GetExposedMaster();
        }
        
        /*
         * return -80 : 0
         */
        public static float GetExposedMusic()
        {
            return YuCoGameAudioSource.Instance.GetExposedMusic();
        }
        
        /*
         * return -80 : 0
         */
        public static float GetExposedEffect()
        {
            return YuCoGameAudioSource.Instance.GetExposedEffect();
        }
    }
}