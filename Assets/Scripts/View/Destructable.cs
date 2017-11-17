using UnityEngine;

namespace Game
{ 
    public class Destructable:MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision2D)
        {
            Destroy(gameObject);
        }
    }
}
