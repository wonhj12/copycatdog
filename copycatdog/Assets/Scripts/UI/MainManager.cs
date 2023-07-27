using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    // 0 - bazzi , 1 - dao
    public int player1;
    public int player2;

    public int loser;

    private AudioSource audio;
    public AudioClip die;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GameOver(int loser)
    {
        this.loser = loser;
        Debug.Log("Game Over");
        audio.clip = die;
        audio.Play();
        SceneManager.LoadScene("GameOver"); 
    }
}
