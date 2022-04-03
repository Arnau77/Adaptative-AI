using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAndManaFeedback : MonoBehaviour
{
    public float secondsToLive;
    System.DateTime lifetime;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPos = gameObject.transform.position;
        newPos.y += 0.5f;
        gameObject.transform.position = newPos;
        lifetime = System.DateTime.Now.AddSeconds(secondsToLive);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime <= System.DateTime.Now)
        {
            Destroy(gameObject);
        }

        Vector3 newScale = gameObject.transform.localScale;
        newScale.x += 0.01f;
        newScale.y += 0.01f;
        Vector3 newPos = gameObject.transform.position;
        newPos.x += 0.06f;
        newPos.y += 0.005f;
        gameObject.transform.localScale = newScale;
        gameObject.transform.position = newPos;

    }
}
