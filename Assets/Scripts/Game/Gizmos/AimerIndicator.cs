using UnityEngine;

namespace Oisio.Game
{
    public class AimerIndicator : MonoBehaviour 
    {
        public Transform indicator;
        public Transform aimerTr;

    	private void Update ()
        {
            Vector3 direction = (new Vector3(aimerTr.position.x, indicator.position.y, aimerTr.position.z) - indicator.position);
            if (direction.sqrMagnitude > Mathf.Epsilon)
            {
                indicator.rotation = Quaternion.LookRotation(direction);
                indicator.gameObject.SetActive(aimerTr.gameObject.activeInHierarchy);
            }
            else 
                indicator.gameObject.SetActive(false);
    	}
    }
}