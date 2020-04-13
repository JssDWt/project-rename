using CommandLine;
using CommandLine.Text;

public class Options
{
    [Option(
        'd', 
        "directory", 
        Required = false, 
        Default = ".",
        HelpText = "Directory to scan. By default the current directory is used.")]
    public string Directory { get; set; }

    [Option(
        'f',
        "from",
        Required = true,
        HelpText = "The text to replace."
    )]
    public string From { get; set; }

    [Option(
        't',
        "to",
        Required = true,
        HelpText = "The text to use as a replacement."
    )]
    public string To { get; set; }

    [Option(
        "rd",
        Default = true,
        HelpText = "Flag indicating whether directories should be renamed.")]
    public bool ShouldRenameDirectories { get; set; }          

    [Option(
        "rf",
        Default = true,
        HelpText = "Flag indicating whether files should be renamed.")]
    public bool ShouldRenameFiles { get; set; }  

    [Option(
        "rc",
        Default = true,
        HelpText = "Flag indicating whether file contents should be handled as well.")]
    public bool ShouldRenameFileContents { get; set; }  

    [Option(
        'e',
        "exclude",
        Default = "bin,obj,.git,.vs,.vscode",
        HelpText = "Folders to ignore, separated by comma ','."
    )]
    public string ExcludeFolders { get; set; }

    [Option(
        's',
        "allow-non-git",
        Default = false,
        HelpText = "Flag indicating whether to allow non-source controlled folders to rename."
    )]
    public bool AllowNonGit { get; set; }
}