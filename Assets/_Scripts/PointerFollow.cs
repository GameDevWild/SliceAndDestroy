using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerFollow : MonoBehaviour
{
    public Camera mainCamera;
    void Start()
    {
        
    }

    
    void Update()
    {
        GetMouseWorldPos();
    }

    /// <summary>
    /// Convierte la posición del ratón en la posición que representa la cámara, obteniendo así las coordenadas del mundo. Z=0 
    /// </summary>
    private void GetMouseWorldPos()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y);
        transform.position = mousePos;
    }
}
