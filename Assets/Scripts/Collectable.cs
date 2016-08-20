using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour 
{
    [SerializeField] private CollectableStatusBar statusBar;

    public enum State
    {
        Charged,
        Charging
    }

    private State current;
    [SerializeField] private float collectionTime;
    [SerializeField] private float reloadTime;

    private float percentage = 1f;

    private void Awake()
    {
        statusBar.Init(percentage);
    }

    public void Collect()
    {
        if (current.Equals(State.Charging)) return;

        percentage -= Time.deltaTime/collectionTime;
    }

    private void CheckStatus()
    {
        if (percentage < Mathf.Epsilon && current.Equals(State.Charged))
        {
            current = State.Charging;
        }
        else
        {
            percentage += Time.deltaTime/reloadTime;
            if (percentage > 1) current = State.Charged;
        }

        percentage = Mathf.Clamp01(percentage);
    }

    private void Update()
    {
        CheckStatus();
        statusBar.SetBarView(percentage, current);
    }
}