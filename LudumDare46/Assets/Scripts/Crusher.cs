using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    private bool isRetracting = false;
    private bool isActive = false;
    [HideInInspector]
    public bool isHorizontal;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float contractTime;
    [HideInInspector]
    public float delay;
    [HideInInspector]
    public GameObject warning;
    [HideInInspector]
    public float warningOffset;
    
    private void Start()
    {
        StartCoroutine(Warning());
    }
    
    private void Update()
    {
        if (isHorizontal == true && isRetracting == false && isActive == true)
        {
            transform.localScale = new Vector2(transform.localScale.x + 1 * speed * Time.deltaTime, transform.localScale.y);
        }
        else if (isHorizontal == false && isRetracting == false && isActive == true)
        {
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + 1 * speed * Time.deltaTime);
        }
        else if (isHorizontal == true && isRetracting == true && transform.localScale.x > .5f && transform.localScale.y > .5f)
        {
            transform.localScale = new Vector2(transform.localScale.x - 1 * speed * Time.deltaTime, transform.localScale.y);
        }
        else if (isHorizontal == false && isRetracting == true && transform.localScale.x > .5f && transform.localScale.y > .5f)
        {
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y - 1 * speed * Time.deltaTime);
        }
        else if (transform.localScale.x < .5f  || transform.localScale.y < .5f)
        {
            Destroy(gameObject);
        }
    }
    
    private IEnumerator Warning()
    {
        GameObject newWarning = CreateWarning();
        yield return new WaitForSeconds(delay);
        Destroy(newWarning);
        StartCoroutine(Contract());
    }

    private GameObject CreateWarning()
    {
        if (isHorizontal == true && transform.position.x < 0f)
        {
            return Instantiate(warning, new Vector2(transform.position.x + warningOffset, transform.position.y), Quaternion.identity);
        }
        else if (isHorizontal == true && transform.position.x > 0f)
        {
            return Instantiate(warning, new Vector2(transform.position.x - warningOffset, transform.position.y), Quaternion.identity);
        }
        else if (isHorizontal == false && transform.position.y > 0f)
        {
            return Instantiate(warning, new Vector2(transform.position.x, transform.position.y - warningOffset), Quaternion.identity);
        }
        else if (isHorizontal == false && transform.position.y < 0f)
        {
            return Instantiate(warning, new Vector2(transform.position.x, transform.position.y + warningOffset), Quaternion.identity);
        }

        return null;
    }
    
    private IEnumerator Contract()
    {
        isActive = true;
        yield return new WaitForSeconds(contractTime);
        isRetracting = true;
    }
}
