using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Vector2 _teleportTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rogue>())
        {
            collision.transform.position = _teleportTarget;
        }
    }
}
