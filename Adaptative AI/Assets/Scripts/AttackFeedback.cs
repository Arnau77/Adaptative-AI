using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFeedback : MonoBehaviour
{
    public float secondsToLive;
    System.DateTime lifetime;
    bool embiggening = true;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPos = gameObject.transform.position;
        newPos.y += 0.5f;
        newPos.x += 0.5f;
        gameObject.transform.position = newPos;
        lifetime = System.DateTime.Now.AddSeconds(secondsToLive);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime <= System.DateTime.Now)
        {
            if (embiggening)
            {
                lifetime = System.DateTime.Now.AddSeconds(secondsToLive-0.5);
                embiggening = false;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        Vector3 newScale = gameObject.transform.localScale;
        if (embiggening)
        {
            newScale.x += 0.01f;
            newScale.y += 0.01f;
        }
        else
        {
            newScale.x -= 0.001f;
            newScale.y -= 0.001f;
        }
        Vector3 newPos = gameObject.transform.position;
        if (embiggening)
        {
            newPos.x += 0.06f;
            newPos.y += 0.005f;
        }
        else
        {
            newPos.x -= 0.006f;
            newPos.y -= 0.0005f;
        }
        gameObject.transform.localScale = newScale;
        gameObject.transform.position = newPos;

    }
}
