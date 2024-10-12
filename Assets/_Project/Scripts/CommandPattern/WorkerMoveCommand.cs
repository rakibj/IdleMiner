using System.Collections;
using UnityEngine;

namespace IdleGame.Command
{
    public class WorkerMoveCommand : CommandBehavior
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform body;
        [SerializeField] private Stat speedStat;
        private Transform _commandRunner;

        public override void Init(Transform commandRunner)
        {
            _commandRunner = commandRunner;      
        }

        public override IEnumerator Execute()
        {
            var startPosition = transform.position;
            var distanceToTarget = Mathf.Infinity; 
            var xScale = startPosition.x < target.position.x ? 1 : -1;
            body.transform.localScale = new Vector3(xScale, 1, 1);
    
            while (distanceToTarget > 0.1f)  
            {
                var step = speedStat.currentValue * Time.deltaTime; 
                _commandRunner.transform.position = Vector3.MoveTowards(_commandRunner.transform.position, target.position, step);
                distanceToTarget = Mathf.Abs(_commandRunner.transform.position.x - target.position.x);  
                yield return null;  
            }

            // _commandRunner.transform.position = target.position;
        }
    }

}