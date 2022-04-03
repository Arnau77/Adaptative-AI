using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseOrDecreaseFeedback : MonoBehaviour
{
    public float secondsToLive;
    public bool increase;
    System.DateTime lifetime;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPos = gameObject.transform.position;
        newPos.y += increase ? -1f : 1f;
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
        
        Vector3 newPos = gameObject.transform.position;
        newPos.y += increase ? 0.005f : -0.005f;
        gameObject.transform.position = newPos;
    }
}
