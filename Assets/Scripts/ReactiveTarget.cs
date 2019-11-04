using System.Collections;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    private WanderingAI behavior;
    [SerializeField] private GameObject target;

    public void ReactToHit()
    {
        behavior = GetComponent<WanderingAI>();

        if (behavior)
        {
            behavior.SetIsAlive(false);
        }

        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(target);
    }
}
