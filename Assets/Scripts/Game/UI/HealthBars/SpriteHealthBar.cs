using UnityEngine;
using System.Collections;

namespace Oisio.Game
{
    public class SpriteHealthBar : HealthBar
    {
        [SerializeField] private Transform pivot;
        
    	protected override void Update()
        {
            base.Update();
            pivot.localScale = new Vector3(percentage, 1f, 1f);
            transform.eulerAngles = Vector3.zero;
    	}
    }
}