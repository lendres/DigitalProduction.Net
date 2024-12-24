namespace DigitalProduction.Projects;

/// <summary>
/// Delegate used for saving Projects.
/// </summary>
/// <param name="projectCompressor">ProjectCompressor used to zip project files.</param>
public delegate void ProjectSavingEventHandler(ProjectCompressor projectCompressor);

/// <summary>
/// Delegate used for openning Projects.
/// </summary>
/// <param name="projectExtractor">ProjectExtractor used to unzip project files.</param>
public delegate void ProjectOpenedEventHandler(ProjectExtractor projectExtractor);