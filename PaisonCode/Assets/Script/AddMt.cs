using UnityEngine;

public class AddMt : MonoBehaviour
{
    void Start()
    {
        // Criar um novo PhysicsMaterial2D
        PhysicsMaterial2D novoMaterial = new PhysicsMaterial2D();
        novoMaterial.friction = 0.5f; // Configurar propriedades conforme necessário

        // Obtém o Collider2D do objeto
        Collider2D collider = GetComponent<Collider2D>();

        // Verifica se o Collider2D existe
        if (collider != null)
        {
            // Atribui o novo material ao Collider2D
            collider.sharedMaterial = novoMaterial;
        }
        else
        {
            Debug.LogWarning("Collider2D não encontrado neste objeto.");
        }
    }
}