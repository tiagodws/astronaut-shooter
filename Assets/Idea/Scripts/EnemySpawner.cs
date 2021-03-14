using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeToSpawn = 2f;

    private float nextTimeToSpawn;

    private void Awake() {
        nextTimeToSpawn = Time.time + timeToSpawn;
    }

    private void Update() {
        if (Time.time >= nextTimeToSpawn) {
            SpawnEnemy();
            nextTimeToSpawn = Time.time + timeToSpawn;
        }
    }

    private void SpawnEnemy() {
        Camera cam = Camera.main;
        Vector3 positionTopCamera = cam.ScreenToWorldPoint(new Vector3(Random.Range(0, cam.pixelWidth), cam.pixelHeight, 0));
        Vector3 positionToSpawn = new Vector3(positionTopCamera.x, positionTopCamera.y + 1, transform.position.z);

        Instantiate(enemyPrefab, positionToSpawn, Quaternion.identity);
    }
}
