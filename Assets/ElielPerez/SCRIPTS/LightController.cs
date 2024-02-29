using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    

    [SerializeField] private Light directionalLight;
    public VolumeProfile vol;
    public void UpdateLight(float temp, int timezone)
    {
        // Color Luz
        if (temp < 15)
        {
            directionalLight.color = new Color(0f, 0f, 1f);
            // Ajusta el color de rojo
        }
        else 
        {
            directionalLight.color = new Color(1f, 0f, 0f);
            // Ajusta el color de azul
        }

       
        DateTimeOffset gmtTime = DateTimeOffset.UtcNow;  // Obtener hora actual en UTC

       
        TimeSpan offset = TimeSpan.FromSeconds(timezone);  // Aplicar el offset de la zona horaria
        DateTimeOffset localTime = gmtTime.ToOffset(offset);

        
        if (localTime.Hour < 6) // Verificar la hora local y ajustar la intensidad de la luz
        {
            if (vol.TryGet(out Bloom bloom))
            {
                bloom.intensity.value = 2f;
            }
           
            directionalLight.intensity = 0.5f; //Antes de las 6 AM, establecer baja intensidad
        }
        else if (localTime.Hour >= 12)
        {
          
           
            directionalLight.intensity = 1.5f;  // Después de las 12 PM establece alta intensidad
            if (vol.TryGet(out Bloom bloom))
            {
                bloom.intensity.value = 5f;
            }
        }
        else
        {
            // En otros casos, mantener intensidad normal
            directionalLight.intensity = 1.0f;
        }

        
    }
}
