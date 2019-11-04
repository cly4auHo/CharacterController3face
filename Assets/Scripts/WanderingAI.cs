using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    private bool isAlive;

    private float obstacleRange = 5.0f;
    private float angle;

    private Ray ray;
    private float radiusOfRay = 1f;
    private GameObject hitObject;

    [SerializeField] private GameObject fireBallPrefab;
    private GameObject currentFireball;

    void Start()
    {
        isAlive = true;
    }

    void Update()
    {
        if (isAlive)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);

            ray = new Ray(transform.position, transform.right);
            RaycastHit hit;

            if (Physics.SphereCast(ray, radiusOfRay, out hit))
            {
                hitObject = hit.transform.gameObject;

                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (!currentFireball)
                    {
                        currentFireball = Instantiate(fireBallPrefab);
                        currentFireball.transform.position = transform.TransformPoint(Vector3.forward);
                        currentFireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    angle = Random.Range(-90, 90);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetIsAlive(bool isAlive)
    {
        this.isAlive = isAlive;
    }
}
