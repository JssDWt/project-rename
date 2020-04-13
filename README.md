# project-rename
C# script to rename a .Net solution (Directories, files and file contents)

Be careful though, It will simply scan the folder for the string you pass in in the directory you pass in and replace all occurrences of that string with the replacement.
That means you may screw up your filesystem. (By default, folders that are not under source control are blocked from modifications.)
