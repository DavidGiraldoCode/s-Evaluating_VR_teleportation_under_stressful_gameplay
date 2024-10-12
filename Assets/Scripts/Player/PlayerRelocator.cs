using UnityEngine;
/// <summary>
/// Listens to the condictions and relocates the player in the middle of the environment
/// </summary>
public class PlayerRelocator : MonoBehaviour
{
    [SerializeField] private Transform m_player;

    private void Awake()
    {
        if (!GameInstanceManager.Instance)
            throw new System.NullReferenceException("The GameInstanceManager is missing in the scene");
        if (!m_player)
            throw new System.NullReferenceException("No Player has been asgined to be realocated");
    }
    private void OnEnable()
    {
        SubribeFromConditionEvents();
    }
    private void OnDisable()
    {
        UnsubribeFromConditionEvents();
    }

    private void PlayerRelocationOnConditionChange(Condition newCondition)
    {
        m_player.position = transform.position;
    }
    private void SubribeFromConditionEvents()
    {
        if (GameInstanceManager.Instance)
        {
            GameInstanceManager.Instance.OnConditionChanged += PlayerRelocationOnConditionChange;
            GameInstanceManager.Instance.OnConditionTerminated += PlayerRelocationOnConditionChange;
            GameInstanceManager.Instance.OnConditionFulfilled += PlayerRelocationOnConditionChange;
        }
    }

    private void UnsubribeFromConditionEvents()
    {
        if (GameInstanceManager.Instance)
        {
            GameInstanceManager.Instance.OnConditionChanged -= PlayerRelocationOnConditionChange;
            GameInstanceManager.Instance.OnConditionTerminated -= PlayerRelocationOnConditionChange;
            GameInstanceManager.Instance.OnConditionFulfilled -= PlayerRelocationOnConditionChange;
        }
    }
}