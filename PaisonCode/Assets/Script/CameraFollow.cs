using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string targetTag = "Player"; // Tag do objeto que a c�mera deve seguir.
    public Vector3 offset = new Vector3(0, 0, -10f); // Deslocamento da c�mera em rela��o ao alvo.
    public float smoothSpeed = 0.125f; // Velocidade de suaviza��o do movimento.

    private Transform target; // Refer�ncia ao Transform do alvo.

    private void Start()
    {
        // Encontra o objeto com a tag especificada.
        UpdateTarget();
    }

    private void LateUpdate()
    {
        // Se o alvo foi encontrado, a c�mera segue o alvo.
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    // M�todo para atualizar o alvo com base na tag.
    public void UpdateTarget()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogWarning("Objeto com a tag '" + targetTag + "' n�o encontrado.");
        }
    }
}
