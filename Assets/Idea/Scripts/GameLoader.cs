using UnityEngine;

public class GameLoader : MonoBehaviour {
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject soundManager;
    [SerializeField] private GameObject scoreManager;

    void Awake() {
        if (GameManager.instance == null) Instantiate(gameManager);
        if (SoundManager.instance == null) Instantiate(soundManager);
        if (ScoreManager.instance == null) Instantiate(scoreManager);
    }
}
