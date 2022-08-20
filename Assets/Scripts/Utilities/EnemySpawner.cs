using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemies;

    [SerializeField]
    private List<GameObject> collectables;

    private Vector2 screenBounds;

    private Timer timer;
    private float duration;

    private Vector3[] startPositions = new Vector3[4];

    private System.Random random;

    private float heartSpawnProb;
    private int typesOfEnemies;


    // Start is called before the first frame update
    void Start()
    {
        var vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
        var horzExtent = vertExtent * Screen.height / Screen.height;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(horzExtent, vertExtent, Camera.main.transform.position.z));

        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 5f;
        timer.Run();

        startPositions[0] = new Vector3(0, -screenBounds.y, -Camera.main.transform.position.z);
        startPositions[1] = new Vector3(-screenBounds.x-3f, 0, -Camera.main.transform.position.z);
        startPositions[2] = new Vector3(0, screenBounds.y, -Camera.main.transform.position.z);
        startPositions[3] = new Vector3(screenBounds.x+3f, 0, -Camera.main.transform.position.z);

        random = new System.Random();
        heartSpawnProb = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

        if (timer.Finished)
        {
            updateParameters();
            spawn();
        }

        
    }

    private void updateParameters()
    {
        if(ScoreManager.instance.Score <= 1500)
        {
            typesOfEnemies = 3;
            duration = 8;
        } else if(ScoreManager.instance.Score <= 3000)
        {
            typesOfEnemies = 5;
            duration = 6;
        } else if (ScoreManager.instance.Score <= 4500)
        {
            typesOfEnemies = 7;
            duration = 5;
        } else if (ScoreManager.instance.Score <= 6000)
        {
            typesOfEnemies = 9;
            duration = 3;
        } 
    }

    private void spawn()
    {
        int i = Random.Range(0, 4);
        Vector3 location = startPositions[i];

        int e = Random.Range(0, typesOfEnemies);
        GameObject enemy = Instantiate(enemies[e]);

        Ghost g = enemy.GetComponent<Ghost>();
        g.Appear(location, (Direction)i);
        
        double n = random.NextDouble();
        if(n <= heartSpawnProb)
        {
            g.AddCollectable(collectables[0]);
        }

        timer.Duration = duration;
        timer.Run();

    }

}
