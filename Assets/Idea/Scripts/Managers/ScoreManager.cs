using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager instance = null;
    private int _score;
    public static int score => instance._score;

    void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        InitScore();
    }

    void InitScore() {
        instance._score = 0;
    }

    public static void IncrementScore(int value) {
        if (instance._score + value < 0) {
            instance._score = 0;
        } else {
            instance._score += value;
        }
    }
}