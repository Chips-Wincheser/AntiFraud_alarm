using UnityEngine;

public class Rogue : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Vector2 _direction;

    private void Start()
    {
        _direction = transform.right.normalized;
    }

    private void Update()
    {
        transform.Translate(_direction*_speed*Time.deltaTime,Space.World);
    }
}
