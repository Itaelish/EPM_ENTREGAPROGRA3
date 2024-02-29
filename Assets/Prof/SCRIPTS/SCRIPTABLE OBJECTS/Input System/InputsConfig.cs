using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este Script servira para manipular la configuracion de los inputs
/// Por ejemplo
/// 
/// 
/// Disparar: Clic Derecho
/// Saltar: Espacio
/// 
/// </summary>
[CreateAssetMenu(fileName = "New Input Config", menuName = "Input/New Input Config", order = 0)]
public class InputsConfig : ScriptableObject
{

    public KeyCode walkForward = KeyCode.W;
    public KeyCode walkBackward = KeyCode.S;

    public KeyCode rotateLeft = KeyCode.A;
    public KeyCode rotateRight = KeyCode.D;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;

    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode reloadKey = KeyCode.R;
}
