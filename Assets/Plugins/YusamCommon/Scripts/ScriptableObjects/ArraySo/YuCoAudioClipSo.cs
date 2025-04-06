using UnityEngine;

namespace YusamCommon
{
    [CreateAssetMenu(
        fileName = nameof(YuCoAudioClipSo),
        menuName = "YusamCommon/Configs/New " + nameof(YuCoAudioClipSo)
    )]
    public class YuCoAudioClipSo : YuCoArraySo
    {
        public AudioClip[] audioClips;

        protected override int GetMaxArray()
        {
            return audioClips.Length;
        }

        public void PlayEffectNext()
        {
            YuCoAudioUseCase.PoolEffectPlay(audioClips[GetNextIndex()]);
        }
        
        public void PlayEffectRandom()
        {
            YuCoAudioUseCase.PoolEffectPlay(audioClips[GetRandomIndex()]);
        }
        
        public void PlayMusicNext()
        {
            YuCoAudioUseCase.PoolMusicPlay(audioClips[GetNextIndex()]);
        }
        
        public void PlayMusicRandom()
        {
            YuCoAudioUseCase.PoolMusicPlay(audioClips[GetRandomIndex()]);
        }
    }
}