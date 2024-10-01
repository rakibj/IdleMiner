using UnityEngine;

namespace IdleGame.Workers
{
    public class TapDetector : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main; 
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                DetectTap(Input.mousePosition);
            }
        }
        
        private void DetectTap(Vector3 inputPosition)
        {
            var worldPosition = _mainCamera.ScreenToWorldPoint(inputPosition);
            var worldPosition2D = new Vector2(worldPosition.x, worldPosition.y);
            var hit = Physics2D.Raycast(worldPosition2D, Vector2.zero);

            if (hit.collider != null)
            {
                var tapReceiver = hit.transform.GetComponent<TapReceiver>();
                if (tapReceiver)
                {
                    tapReceiver.Tap();
                }
            }
        }
    }
}