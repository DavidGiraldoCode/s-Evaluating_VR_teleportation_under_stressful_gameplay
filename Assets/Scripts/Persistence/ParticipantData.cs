using UnityEngine;

/// <summary>
/// ScriptableObject to store participant data and experimental condition variables.
/// It gets saved every time any of it fields changes
/// </summary>
[CreateAssetMenu(fileName = "ParticipantData", menuName = "Experiment/ParticipantData", order = 1)]
public class ParticipantData : ScriptableObject
{
    // Unique identifier for each participant.
    public string id;

    // Current experimental condition being tested.
    public string condition;

    // Teleportation method type: `0` for traditional teleport, `1` for teleport with orientation adjustment.
    public int independent_variable_1;

    // Stressor type applied: `0` for no stress, `1` for cognitive interference, `2` for environmental stress.
    public int independent_variable_2;

    // Total elapsed time, in seconds, since the start of the current condition.
    public float time;

    // Countdown timer, in seconds, indicating remaining time to complete the task (range: 0 to 300 seconds).
    public float trial_count_down;

    // The prompted distance the player must move to reach the target platform:
    // `1` for shortest, `2` for medium, and `3` for longest distance.
    // This measure excludes any central platform displacement.
    public int prompted_distance;

    // Reaction time, in milliseconds, between receiving the prompt and initiating the first teleportation.
    // Measured only on initial teleportation.
    public float reaction_time;

    // Color prompt for the target platform based on distance from the player. Colors represented as enums:
    // `0` = red, `1` = blue, `2` = green, `3` = yellow, `4` = orange, `5` = purple.
    public int prompted_color;

    // Count of teleportations taken from receiving a new destination prompt until the target platform is activated.
    // Linked with `last_orientation_delta.`
    public int cumulative_teleportations;

    // Absolute change in player orientation (in degrees) from the starting view (pre-teleport) 
    // to the final view (post-teleport) after indicating turn direction.
    public float last_orientation_delta;

    // Instances of landing on the incorrect platform after receiving a color prompt.
    // Resets with each new destination.
    public int wrong_platform_given_a_color;

    // Count the number of times the user touched the buzz wire when attempting platform activation;
    // reset it after each successful activation.
    public int buzzwire_touches_at_platform;

    // Time, in seconds, spent at the target platform for activation; calculated by referencing the last recorded log.
    public float buzzwire_time_at_platform;

    // Indicates whether the player’s required platform (for activation via buzz-wire) 
    // is currently in an activated state.
    public bool current_platform_activation_state;

    // Cumulative count of platforms successfully activated by the participant.
    public int activated_platforms_counter;
}

