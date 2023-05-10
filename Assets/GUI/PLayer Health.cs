using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    private Stats player_stats;
    private TextMeshProUGUI textMesh;
    public GameObject player;
    public bool isPlayerDisplay;
    // Start is called before the first frame update
    void Start()
    {
        player_stats = player.GetComponent<Stats>();
        Debug.Log(player_stats);
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        Debug.Log(textMesh);
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "Player Health: " + player_stats.health + "\nStocks: " + player_stats.stocks;

        if (player_stats.stocks <= 0)
        {
            if (isPlayerDisplay)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName: "AI Wins");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName: "Player Wins");
            }
        }
    }
}
