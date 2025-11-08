using UnityEngine;

public class EnemyAlert : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f; // quanto tempo o alerta fica ativo

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}

