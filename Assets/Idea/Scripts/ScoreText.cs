using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
    Text score;

    void Awake() {
        score = GetComponent<Text>();
    }

    void Update() {
        score.text = "Score: " + ScoreManager.score;
    }
}
