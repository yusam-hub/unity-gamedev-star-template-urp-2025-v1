using UnityEngine;

namespace YusamCommon
{
    public interface IYuCoAudioSourcePool
    {
        public AudioSource Rent();
        public void Return(AudioSource audioSource);
        
        public void Init(int initialCount);
        public void Clear();
    }
}