using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CenaFInal : MonoBehaviour
{
    public Transform targetLocation; // Posição que aciona o carregamento da cena
    

    void Start()
    {
        targetLocation = GameObject.Find("TargetLocationA").transform;

        if (targetLocation == null)
        {
            Debug.LogError("TargetLocation não encontrado!");
        }
    }
    void Update()
    {
        // Verifica a distância entre o jogador e o alvo
        float distanceToTarget = Vector3.Distance(transform.position, targetLocation.position);
        bool matou_o_boss = GameManager.Singleton.GetFlag("boss_fase") == 3;
        // Se o jogador estiver dentro do raio carregue a cena
        if (distanceToTarget < 10 && matou_o_boss)
        {
            SceneManager.LoadScene("CenaFinal");
        }
    }
}
