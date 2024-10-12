using System;
using System.Collections.Generic;
using IdleGame.Workers;
using UnityEngine;

namespace IdleGame.Modules
{
    public class Shaft : MonoBehaviour
    {
        [SerializeField] private MinerController minerTemplate;
        [SerializeField] private int numberOfMiners = 1;
        private List<MinerController> _miners = new List<MinerController>();
        public int MinerCount => _miners.Count;

        private void Start()
        {
            for (var i = 0; i < numberOfMiners; i++)
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