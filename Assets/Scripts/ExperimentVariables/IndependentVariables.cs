/// <summary>
/// Hold all the Independent Variables of the experiment.
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