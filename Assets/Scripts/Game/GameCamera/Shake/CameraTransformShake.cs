using UnityEngine;

namespace Oisio.Game
{
    public class CameraTransformShake : MonoBehaviour
    {    
        public float LerpSpeed;

    	private void Update () 
        {
            CameraShake.Instance.FrameFeed();
            Vector2 shakePoint = CameraShake.Instance.GetShakePoint();
            Vector3 localShakePoint = new Vector3(shakePoint.x, shakePoint.y, transform.localPosition.z);
            transform.localPosition = Vector3.Lerp(transform.localPosition, localShakePoint, LerpSpeed * Time.deltaTime);
    	}
    }
}