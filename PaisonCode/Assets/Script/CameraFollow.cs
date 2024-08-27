using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string targetTag = "Player"; // Tag do objeto que a câmera deve seguir.
    public Vector3 offset = new Vector3(0, 0, -10f); // Deslocamento da câmera em relação ao alvo.
    public float smoothSpeed = 0.125f; // Velocidade de suavização do movimento.

    private Transform target; // Referência ao Transform do alvo.

    private void Start()
    {
        // Encontra o objeto com a tag especificada.
        UpdateTarget();
    }

    private void LateUpdate()
    {
        // Se o alvo foi encontrado, a câmera segue o alvo.
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    // Método para atualizar o alvo com base na tag.
    public void UpdateTarget()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogWarning("Objeto com a tag '" + targetTag + "' não encontrado.");
        }
    }
}
