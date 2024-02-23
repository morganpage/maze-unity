using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _textGameOver;
    [SerializeField] private GameObject _player;

    void Awake(){
        _textScore.text = "0";
        _gameOverPanel.SetActive(false);
    }

    void OnEnable(){
        Gatherable.OnGathered += HandleGathered;
        EnemyAI.OnPlayerAttacked += HandlePlayerAttacked;
    }

    void OnDisable(){
        Gatherable.OnGathered -= HandleGathered;
        EnemyAI.OnPlayerAttacked -= HandlePlayerAttacked;
    }

    void HandlePlayerAttacked(){
        _gameOverPanel.SetActive(true);
        _player.SetActive(false);
    }

    void HandleGathered(int gathered){
        _textScore.text = gathered.ToString();
        //Is this the final gatherable???
        int totalGatherablesLeft = FindObjectsByType<Gatherable>(FindObjectsInactive.Exclude,FindObjectsSortMode.None).Length;
        Debug.Log($"Gatherables Left: {totalGatherablesLeft}");
        if(totalGatherablesLeft == 0){
            _textGameOver.text = "You Win!!!";
            _gameOverPanel.SetActive(true);
            _player.SetActive(false);
        }
    }

    public void RestartGame(){
        SceneManager.LoadScene(0);
    }

}
