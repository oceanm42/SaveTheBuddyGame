using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikeball : MonoBehaviour
{
    private Animator animator;
    [HideInInspector]
    public float lifetime;
    [HideInInspector]
    public float rotateSpeed;
    private bool isActive = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SpikeFadeIn") && isActive == false)
        {
            animator.SetBool("Active", true);
            isActive = true;
            StartCoroutine(StartLifetime());
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SpikeFadeOut"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator StartLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        animator.SetBool("Active", false);
    }
}
