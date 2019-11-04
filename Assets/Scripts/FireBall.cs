using UnityEngine;

public class FireBall : MonoBehaviour
{
    private PlayerCharacter player;

    [SerializeField] private float speed = 15.0f;
    private int damage = 1;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerCharacter>();

            if (player)
            {
                player.Hurt(damage);
            }
        }

        Destroy(gameObject);
    }
}
