using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Rogue _rogue;
    [SerializeField] private Vector2 _teleportTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision==_rogue.GetComponent<Collider2D>())
        {
            _rogue.transform.position = _teleportTarget;
        }
    }
}
