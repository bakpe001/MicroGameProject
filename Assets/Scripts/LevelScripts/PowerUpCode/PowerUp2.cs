using UnityEngine;

public class PowerUp2 : MonoBehaviour
{
    //DoubleJump PowerUp
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var pm = other.GetComponent<PlayerManager>();
            if (pm != null)
            {
                pm.DoubleJump = true; // grant double jump
                Destroy(gameObject);
            }
        }
    }
}
