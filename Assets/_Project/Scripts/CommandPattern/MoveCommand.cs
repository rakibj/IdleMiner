using System.Collections;
using UnityEngine;

namespace IdleGame.Command
{
    public class MoveCommand : CommandBehavior
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed;
        private Transform _commander;

        public override void Init(Transform commander)
        {
            _commander = commander;      
        }

        public override IEnumerator Execute()
        {
            var startPosition = transform.position;
            var distanceToTarget = Mathf.Abs(transform.position.x - target.position.x); 
            var xScale = startPosition.x < target.position.x ? 1 : -1;
            _commander.transform.localScale = new Vector3(xScale, 1, 1);
    
            while (distanceToTarget > 0.01f)  
            {
                var step = speed * Time.deltaTime; 
                _commander.transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                distanceToTarget = Mathf.Abs(transform.position.x - target.position.x);  
                yield return null;  
            }

            transform.position = target.position;
        }
    }

}