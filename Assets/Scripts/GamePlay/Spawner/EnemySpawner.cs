using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{

    public class EnemySpawner : ISpawner
    {
        //5웨이브 동안 최대로 필요한 몬스터의 갯수만큼 생성
        private const int DefaultPoolSize = 20;
        private Dictionary<EnemyType, ObjectPool<BaseEnemy>> enemyPoolDictionary;
        //private WaveData waveData;

        public EnemySpawner(List<BaseEnemy> enemies)
        {
            CreatePool(enemies);
        }

        private void CreatePool(List<BaseEnemy> enemies/*, WaveDatas */)
        {
 /*           for(int i = 0; i < (int)EnemyTypes.End;i++)
            {
                EnemyTypes currentEnemyType = (EnemyTypes)i;
                GameObject parent = UnityEngine.Object.Instantiate(new GameObject(currentEnemyType.ToString() + "Pool"));
                enemyPoolDictionary.Add(currentEnemyType, new ObjectPool<BaseEnemy>(enemies[i], DefaultPoolSize, parent.transform));
            }*/
        }

        private async void SpawnEnemyRoutine()
        {

        }

        public bool IsSpawnAble()
        {
            return true;    
        }

        public void OnSpawn(EnemyType enemyType, Vector2 position)
        {
            enemyPoolDictionary[enemyType].Spawn(position, Quaternion.identity);
        }

        public void StartSpawning()
        {
            throw new System.NotImplementedException();
        }

        public void StopSpawning()
        {
            throw new System.NotImplementedException();
        }

        public void ClearAll()
        {
            throw new System.NotImplementedException();
        }
    }
}