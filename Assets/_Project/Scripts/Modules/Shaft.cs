using System;
using System.Collections.Generic;
using IdleGame.Workers;
using UnityEngine;

namespace IdleGame.Modules
{
    public class Shaft : MonoBehaviour
    {
        [SerializeField] private MinerController minerTemplate;
        [SerializeField] private Stat numberOfMinerStat;
        private List<MinerController> _miners = new List<MinerController>();
        public int MinerCount => _miners.Count;

        private void OnEnable()
        {
            numberOfMinerStat.OnUpdated += OnMinerCountUpdated;
        }
        private void OnDisable()
        {
            numberOfMinerStat.OnUpdated -= OnMinerCountUpdated;
        }

        private void OnMinerCountUpdated(Stat stat)
        {
            if(numberOfMinerStat.currentValue > _miners.Count)
            {
                var toAdd = Mathf.CeilToInt(numberOfMinerStat.currentValue - _miners.Count);
                for (int i = 0; i < toAdd; i++)
                    AddMiner(i);
            }
        }

        private void Start()
        {
            for (var i = 0; i < numberOfMinerStat.currentValue; i++)
            {
                AddMiner(i);
            }

            minerTemplate.gameObject.SetActive(false);
        }

        private void AddMiner(int i = 0)
        {
            var miner = Instantiate(minerTemplate, minerTemplate.transform.parent);
            miner.gameObject.SetActive(true);
            var localPosition = miner.transform.localPosition;
            miner.transform.localPosition = new Vector3(localPosition.x + 0.1f * i, localPosition.y, localPosition.z);
            _miners.Add(miner);
        }
    }
}