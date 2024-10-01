using System.Collections;
using UnityEngine;

namespace IdleGame.Command
{
    public class ElevatorMoveCommand : CommandBehavior
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed;
        private Transform _commandRunner;

        public override void Init(Transform commandRunner)
        {
            _commandRunner = commandRunner;      
        }

        public override IEnumerator Execute()
        {
            var distanceToTarget = Mathf.Abs(_commandRunner.transform.position.y - target.position.y); 
    
            while (distanceToTarget > 0.01f)  
            {
                var step = speed * Time.deltaTime; 
                _commandRunner.transform.position = Vector3.MoveTowards(_commandRunner.transform.position, target.position, step);
                distanceToTarget = Mathf.Abs(_commandRunner.transform.position.y - target.position.y);  
                yield return null;  
            }

            _commandRunner.transform.position = target.position;
        }
    }
}