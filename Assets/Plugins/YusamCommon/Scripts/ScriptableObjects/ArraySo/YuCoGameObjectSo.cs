using UnityEngine;

namespace YusamCommon
{
    [CreateAssetMenu(
        fileName = nameof(YuCoGameObjectSo),
        menuName = "YusamCommon/Configs/New " + nameof(YuCoGameObjectSo)
    )]
    public class YuCoGameObjectSo : YuCoArraySo
    {
        public GameObject[] gameObjects;
        protected override int GetMaxArray()
        {
            return gameObjects.Length;
        }
        
        public GameObject InstantiateNext(Transform parent)
        {
            return Instantiate(gameObjects[GetNextIndex()], parent.position, parent.rotation, parent);
        }
        
        public GameObject InstantiateRandom(Transform parent)
        {
            return Instantiate(gameObjects[GetRandomIndex()], parent.position, parent.rotation, parent);
        }
        
        public void InstantiateNextDestroy(Transform parent, float destroy)
        {
            Destroy(Instantiate(gameObjects[GetNextIndex()], parent.position, parent.rotation, parent), destroy);
        }
        
        public void InstantiateRandomDestroy(Transform parent, float destroy)
        {
            Destroy(Instantiate(gameObjects[GetRandomIndex()], parent.position, parent.rotation, parent), destroy);
        }
    }
}