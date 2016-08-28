using UnityEngine;
using System.Collections;

public class SmokeBomb : MonoBehaviour
{
    [SerializeField] private GameObject SmokeBombPrefab;

    public void Fire(Vector3 pos)
    {
        GameObject smokeInsr = Instantiate(SmokeBombPrefab) as GameObject;
        smokeInsr.transform.position = pos;

        EventTrigger smokeBomb = new CustomEvent(transform.position, EventSubject.SmokeBomb, GameConfig.smokeBombPriotity, GameConfig.smokeBombEnableTime, true);
        EventObserver.subscribedAction.Add(smokeBomb);
    }
}