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
                pm.DoubleJump = true;          // Aktivoi tuplahyppy k�ytt��n
                pm.canDoubleJump = true;       // Varmista ett� sit� voi k�ytt�� heti
                Destroy(gameObject);           // Poista powerup-objekti
            }
        }
    }
}