/// <summary>
/// Hold all the Independent Variables of the experiment.
/// Teleportation Method, Cognitive Interference, and Time And Environmental Stressor.
/// </summary>

/*
[1] E. Bozgeyikli, A. Raij, S. Katkoori, and R. Dubey, “Point & Teleport Locomotion Technique for Virtual Reality,” in Proceedings of the 2016 Annual Symposium on Computer-Human Interaction in Play, Austin Texas USA: ACM, Oct. 2016, pp. 205–216. doi: 10.1145/2967934.2968105.
[2] M. Funk et al., “Assessing the Accuracy of Point & Teleport Locomotion with Orientation Indication for Virtual Reality using Curved Trajectories,” in Proceedings of the 2019 CHI Conference on Human Factors in Computing Systems, Glasgow Scotland Uk: ACM, May 2019, pp. 1–12. doi: 10.1145/3290605.3300377.
*/
public static class IndependentVariables
{
  public enum Teleportation
  {
    PT_TRADITIONAL, // [1]
    PT_ORIENTATION_INDICATION // [2]
  }

  // NEW
  public enum GameStressor
  {
    NONE,
    /*
    Time Pressure: The time to meet a task gradually decreases to induce stress,
    following guidelines by Adams (Fundamentals of Game Design).
    */
    // Biased Instruction (Cognitively Interfered Instruction): The instruction to teleport to a platform
    // is distorted by presenting a color word in mismatched ink color. Users must teleport based on font color
    // rather than word content, creating cognitive interference.
    TIME_PRESSURE_AND_BIASED_INSTRUCTION,
    
    // Attention Demand: The teleportation platform shrinks toward a centered pivot point on its top surface.
    // As the platform size reduces, the available teleportation area diminishes while the platform's center remains fixed.
    TIME_PRESSURE_AND_ATTENTION_DEMAND,
  }

  // OLD
  public enum CognitiveInterference
  {
    DISABLED,
    ENABLE
  }

  public enum TimeAndEnvironmentalStressor
  {
    DISABLED,
    ENABLE
  }
}