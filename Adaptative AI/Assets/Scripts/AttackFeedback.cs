using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFeedback : MonoBehaviour
{
    public float secondsToLive;
    System.DateTime lifetime;
    bool embiggening = true;
    Vector2 multiplier;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPos = gameObject.transform.position;
        multiplier.x = Screen.fullScreen ? 5f : Screen.width / 750;
        multiplier.y = Screen.fullScreen ? 5f : Screen.height / 400;
        newPos.y += 0.5f;
        newPos.x += Screen.fullScreen ? -1.5f : 0.5f;
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
            newScale.x += 0.01f * multiplier.x;
            newScale.y += 0.01f * multiplier.y;
        }
        else
        {
            newScale.x -= 0.001f * multiplier.x;
            newScale.y -= 0.001f * multiplier.y;
        }
        Vector3 newPos = gameObject.transform.position;
        if (embiggening)
        {
            newPos.x += 0.06f * multiplier.x;
            newPos.y += 0.005f * multiplier.y;
        }
        else
        {
            newPos.x -= 0.006f * multiplier.x;
            newPos.y -= 0.0005f * multiplier.y;
        }
        gameObject.transform.localScale = newScale;
        gameObject.transform.position = newPos;

    }
}
