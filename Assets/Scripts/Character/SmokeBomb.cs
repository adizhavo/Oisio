using UnityEngine;
using System.Collections;

public class SmokeBomb : MonoBehaviour 
{
    [SerializeField] private GameObject SmokeBombPrefab;

    public void Fire(Vector3 pos)
    {
        GameObject smokeInsr = Instantiate(SmokeBombPrefab) as GameObject;
        smokeInsr.transform.position = pos;
    }
}