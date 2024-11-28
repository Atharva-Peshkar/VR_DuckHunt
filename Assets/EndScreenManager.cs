using UnityEngine;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    public TextMeshPro finalScoreText; // Reference to the TextMeshPro object for final score

    void Start()
    {
        // Retrieve the final score from PlayerPrefs and display it
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        finalScoreText.text = "Game Over!\nYour Final Score: " + finalScore +"\n\nPress left hand trigger to replay\n";
    }
}
