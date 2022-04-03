using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackFeedback : MonoBehaviour
{
    public AttackFeedback attackFeedback = null; 
    public float secondsToLive;
    System.DateTime lifetime;
    bool goToRight = true;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x > 0)
        {
            goToRight = false;
        }
        Vector3 newPos = gameObject.transform.position;
        gameObject.transform.position = newPos;
        lifetime = System.DateTime.Now.AddSeconds(secondsToLive);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime <= System.DateTime.Now)
        {
            Player player = GetComponentInParent<Player>().enemy;
            Instantiate(attackFeedback, player.position, Quaternion.identity, player.transform);
            Destroy(gameObject);
        }

        Vector3 newPos = gameObject.transform.position;
        newPos.x += goToRight? 0.05f : -0.05f;
        gameObject.transform.position = newPos;
    }
}
