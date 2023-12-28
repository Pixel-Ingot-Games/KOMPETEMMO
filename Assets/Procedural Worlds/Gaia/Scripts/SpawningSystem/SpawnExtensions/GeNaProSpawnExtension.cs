#if GENA_PRO
//using GeNa.Core;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gaia
{
    /// <summary>
    /// Simple Spawn Extension for demo / debug purposes. Just writes some info to the console when being executed.
    /// </summary>
    public class GeNaProSpawnExtension : MonoBehaviour, ISpawnExtension
    {
        public string Name { get { return "GeNaProSpawnExtension"; } }

        public bool AffectsHeights => false;

        public bool AffectsTextures => false;

        public GameObject m_genaSpawnerPrefab;
#if GENA_PRO
       // private GeNa.Core.GeNaSpawner m_genaSpawnerInstance;
#endif

        public void Close()
        {
            ////Debug.Log("Spawn Extension is closing down.");
            //if (m_genaSpawnerInstance != null)
            //{
            //    DestroyImmediate(m_genaSpawnerInstance.gameObject);
            //}
        }

        public void Init(Spawner spawner)
        {
#if GENA_PRO
          
#endif

        }

        public void Spawn(Spawner spawner, Transform target, int ruleIndex, int instanceIndex, SpawnExtensionInfo spawnExtensionInfo)
        {
#if GENA_PRO
            //Debug.Log("Spawn Extension spawning.");
           
#endif
        }

        public void Delete(Transform target)
        {
            DestroyImmediate(target.gameObject);
        }
    }
}
