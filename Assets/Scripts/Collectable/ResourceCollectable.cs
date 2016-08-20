using UnityEngine;

public abstract class ResourceCollectable : MonoBehaviour, Collectable, Chargable
{
    #region Collectable implementation
    public Vector3 WorlPos
    {
        get
        {
            return transform.position;
        }
    }

    public abstract CollectableType type { get; }
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
    }

    public virtual void Collect()
    {
        if (current.Equals(ChargableState.Charging)) return;

        percentage -= Time.deltaTime/collectionTime;
    }

    protected virtual void CheckStatus()
    {
        if (percentage < Mathf.Epsilon && current.Equals(ChargableState.Charged))
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
}