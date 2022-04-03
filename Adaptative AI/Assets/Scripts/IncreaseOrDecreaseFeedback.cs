using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseOrDecreaseFeedback : MonoBehaviour
{
    public float secondsToLive;
    public bool increase;
    System.DateTime lifetime;
    float multiplier;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPos = gameObject.transform.position;
        multiplier = Screen.fullScreen ? 4 : Screen.width / 400;
        if (!Screen.fullScreen)
        {
            newPos.y += increase ? -1f * multiplier : 1f * multiplier;
        }
        if (Screen.fullScreen)
        {
            newPos.x -= 1;
        }
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
        newPos.y += increase ? 0.005f * multiplier : -0.005f * multiplier;
        gameObject.transform.position = newPos;
    }
}
