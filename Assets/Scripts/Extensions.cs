using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*public class Extensions : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool Raycast(This rigi)
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}*/


// 2 DAY Jump max on the ground velocity.y and vetor2.down
public static class Extensions
{
    public static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic) // if no physics return false or if physics true 
        {
            return false;
        }
        float radius = 0.25f;
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody; // return true if CircleCast hit collider or oppsite
    }

    // 3 DAY Jump on the ground and on the ground velocity.y and vector2.up 

    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection) // true 
    {

        /*
         * EX: 
        transform.position là (0, 0)
        other.position là (1, 1)
        testDirection là (1, 0) (hướng x dương)
        */
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }
}
