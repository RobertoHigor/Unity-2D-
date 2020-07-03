﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs; // Lista de Waves
    int startingWave = 0;

    // Start is called before the first frame update
    void Start()
    {
       var currentWave = waveConfigs[startingWave]; 
       StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig wave)
    {
        for (int i = 0; i < wave.GetNumberOfEnemies(); i++)
        {
            var newenemy = Instantiate(// Guardando os inimigos
            wave.GetEnemyPrefab(), // Pegar o inimigo da wave
            wave.GetWaypoints()[0].transform.position, // Pegar a posição do primeiro waypoint
            Quaternion.identity
            );

            // Passando a WaveConfig do método para o inimigo instanciado.
            newenemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);

            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
        }       
    }
}