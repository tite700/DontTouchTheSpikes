using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private List<GameObject> spikes;
    [SerializeField] private GameManager gameManager;
    
    private bool _hasStarted = false;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var spike in spikes)
        {
            spike.SetActive(false);
        }
        
    }
    
    private IEnumerator SpikeActivation()
    {
        while (true)
        {
            // Randomly choose a random number of spikes to activate
            var numSpikes = UnityEngine.Random.Range(0, spikes.Count);
            for (int i = 0; i < numSpikes; i++)
            {
                var spike = spikes[UnityEngine.Random.Range(0, spikes.Count)];
                spike.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == GameManager.GAME_STATE.play && _hasStarted == false)
        {
            StartCoroutine(SpikeActivation());
            _hasStarted = true;
        }

        if (gameManager.gameState == GameManager.GAME_STATE.gameOver)
        {
            foreach (var spike in spikes)
            {
                spike.SetActive(false);
            }
        }
    }
}
