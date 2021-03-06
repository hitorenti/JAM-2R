using UnityEngine;

public class Puas : MonoBehaviour
{
    private Player_Manager DamageAlPlayer;
    public BoxCollider2D Box;

    private void Awake()
    {
        // Obtener la referencia del jugador con el tag "PLAYER" paar evitar colocarla desde
        // el inspector
        DamageAlPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Manager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamageAlPlayer.Damage(PuasControl.DamageAlPlayer, true, 0); // colocar Dalo al player, indicando que el dalo viene de arriba
            // deactivate collider
            this.transform.parent.GetComponent<BoxCollider2D>().isTrigger = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (collision.gameObject.CompareTag("GROUND"))
        {
            Box.isTrigger=true;
        }
    }
}