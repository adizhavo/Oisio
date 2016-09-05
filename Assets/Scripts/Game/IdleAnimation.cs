using UnityEngine;

public class IdleAnimation : MonoBehaviour 
{
	void Start () 
    {
        Animate();
    }

    protected virtual void Animate()
    {
        float xScale = 0.25f;
        float yScale = 0.25f;
        float animTime = Random.Range(0.7f, 1.3f);

        LeanTween.scaleX(gameObject, xScale, animTime);
        LeanTween.scaleY(gameObject, 0.3f, animTime).setOnComplete(
            ()=>
            {
                LeanTween.scaleX(gameObject, 0.3f, animTime);
                LeanTween.scaleY(gameObject, yScale, animTime).setOnComplete(Animate);
            }
        );
    }
}

