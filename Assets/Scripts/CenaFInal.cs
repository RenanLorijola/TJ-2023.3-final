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
            Debug.LogError("TargetLocation não encontrado! Certifique-se de que o objeto está na cena e tem o nome correto.");
        }
    }
    void Update()
    {
        // Verifica a distância entre o jogador e o alvo
        float distanceToTarget = Vector3.Distance(transform.position, targetLocation.position);
        // Se o jogador estiver dentro do raio carregue a cena
        if (distanceToTarget < 10)
        {
            SceneManager.LoadScene("CenaFinal");
        }
    }
}
