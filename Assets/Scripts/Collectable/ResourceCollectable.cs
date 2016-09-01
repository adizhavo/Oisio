using UnityEngine;

public abstract class ResourceCollectable : MonoBehaviour, Collectable, Chargable
{
    private float minCollectPercentage = Mathf.Epsilon;

    #region Collectable implementation
    public Vector3 WorlPos
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

    public abstract CollectableType type { get; }

    public virtual void Gather(Collector collector)
    {
        if (current.Equals(ChargableState.Charging)) return;

        percentage -= Time.deltaTime/collectionTime;

        if (percentage < minCollectPercentage) collector.CompleteCollection(type);
    }
    #endregion

    #region Chargable implementation
    public ChargableState current
    {
        get 
        {
            return state;
        }
    }
    private ChargableState state;
    #endregion


    [SerializeField] private CollectableStatusBar statusBar;

    [SerializeField] protected float collectionTime;
    [SerializeField] protected float reloadTime;

    protected float percentage = 1f;

    protected virtual void Awake()
    {
        statusBar.Init(percentage);
        Animate();
    }

    protected virtual void CheckStatus()
    {
        if (percentage < minCollectPercentage && current.Equals(ChargableState.Charged))
        {
            state = ChargableState.Charging;
        }
        else
        {
            percentage += Time.deltaTime/reloadTime;
            if (percentage > 1) state = ChargableState.Charged;
        }

        percentage = Mathf.Clamp01(percentage);
    }

    protected virtual void Update()
    {
        CheckStatus();
        statusBar.SetBarView(percentage, current);
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