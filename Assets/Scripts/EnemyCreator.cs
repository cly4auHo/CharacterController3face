using UnityEngine;
using UnityEngine.UI;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] private GameObject targtetPrefab;
    [SerializeField] private GameObject enemyPrefab;

    private float xLeft = -45f;
    private float xRight = 45f;
    private float zLeft = -45f;
    private float zRight = 45f;
    private float yHeight = 1;

    private GameObject currentTarget;
    private GameObject currentEnemy;
    private Vector3 currentPosition;

    [SerializeField] private Text scoreText;
    private int score;

    void Start()
    {
        score = -1;
    }

    void Update()
    {
        if (!currentTarget)
        {
            CreateTarget();
            score++;
        }
        //else if (!currentEnemy)
        //{
        //    CreateEnemy();
        //    score++;
        //}

        scoreText.text = score.ToString();
    }

    void CreateTarget()
    {
        currentPosition = RandomPosition();
        currentTarget = Instantiate(targtetPrefab, currentPosition, Quaternion.identity);
    }

    void CreateEnemy()
    {
        currentPosition = RandomPosition();
        currentTarget = Instantiate(enemyPrefab, currentPosition, Quaternion.identity);
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(xLeft, xRight), yHeight, Random.Range(zLeft, zRight));
    }

    public void SetScore(int score)
    {
        this.score = score;
    }
}
