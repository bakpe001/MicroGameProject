using UnityEngine;

public class PowerUp2 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var pm = other.GetComponent<PlayerManager>();
            if (pm != null)
            {
                pm.doubleJumpCount++;          // Kasvata laskuria
                pm.DoubleJump = true;          // Aktivoi tuplahyppy käyttöön
                pm.canDoubleJump = true;       // Varmista että sitä voi käyttää heti
                Destroy(gameObject);           // Poista powerup-objekti
            }
        }
    }
}