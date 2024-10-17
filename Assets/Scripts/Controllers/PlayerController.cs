using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool m_canTeleport = true;
    public bool CanTeleport { get => m_canTeleport; set => m_canTeleport = value; }
}
