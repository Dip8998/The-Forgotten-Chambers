using UnityEngine;

public class ZigZagProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float frequency = 5f;
    public float magnitude = 0.5f;
    public float lifetime = 5f;

    private Vector2 direction;
    private float spawnTime;
    private Vector3 startPosition;
    private float verticalOffsetDirection = 1f;

    public void Init(int facingDirection, float verticalOffsetDirection)
    {
        direction = new Vector2(facingDirection, 0).normalized;
        this.verticalOffsetDirection = verticalOffsetDirection;
        spawnTime = Time.time;
        startPosition = transform.position;
    }

    void Update()
    {
        float timeAlive = Time.time - spawnTime;
        float zigzag = Mathf.Sin(timeAlive * frequency) * magnitude * verticalOffsetDirection;

        Vector3 offset = new Vector3(0, zigzag, 0);
        Vector3 forward = direction * speed * Time.deltaTime;

        transform.position += forward + offset;

        if (timeAlive > lifetime)
            Destroy(gameObject);
    }
}
