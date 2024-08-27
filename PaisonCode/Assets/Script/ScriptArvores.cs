using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptArvores : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AdjustSortingOrder();
    }

    void AdjustSortingOrder()
    {
        // Ajusta a ordem de renderização com base na posição Y
        spriteRenderer.sortingOrder = Mathf.RoundToInt(( +100 -transform.position.y) +1 );
    }

    void Update()
    {
        // Atualiza a ordem de renderização em tempo real, se necessário
        AdjustSortingOrder();
    }
}
