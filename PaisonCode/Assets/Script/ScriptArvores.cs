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
        // Ajusta a ordem de renderiza��o com base na posi��o Y
        spriteRenderer.sortingOrder = Mathf.RoundToInt(( +100 -transform.position.y) +1 );
    }

    void Update()
    {
        // Atualiza a ordem de renderiza��o em tempo real, se necess�rio
        AdjustSortingOrder();
    }
}
