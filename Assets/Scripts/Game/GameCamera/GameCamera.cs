using UnityEngine;

namespace Oisio.Game
{
    public class GameCamera : MonoBehaviour 
    {
        public Transform Character;
        public float LerpSpeed;

        private Vector3 offset;

        private void Start()
        {
            offset = transform.position - Character.position;
        }

        private void Update()
        {
            if (Character == null) return;
            transform.position = Vector3.Lerp(transform.position, Character.position + offset, LerpSpeed * Time.deltaTime);
        }
    }
}