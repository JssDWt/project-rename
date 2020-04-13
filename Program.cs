using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;

namespace ProjectRename
{
    class Program
    {
        static void Run(Options options)
        {
            VerifyGitFolder(options);
            ReplaceFolderContents(options.Directory, options);
        }

        private static void ReplaceFolderContents(string directory, Options options)
        {
            string directoryName = new DirectoryInfo(directory).Name;
            string[] exclude = options.ExcludeFolders.Split(',');
            if (exclude.Contains(directoryName))
            {
                return;
            }
            
            foreach (var subDirectory in Directory.GetDirectories(directory))
            {
                ReplaceFolderContents(subDirectory, options);
            }

            ReplaceFiles(directory, options);

            
            if (options.ShouldRenameDirectories && directoryName.Contains(options.From))
            {
                string newDirectoryName = directoryName.Replace(options.From, options.To);
                string newLocation = Path.Combine(Directory.GetParent(directory).FullName, newDirectoryName);
                Directory.Move(directory, newLocation);
            }
        }

        private static void ReplaceFiles(string directory, Options options)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                ReplaceFileContent(file, options);

                string fileName = Path.GetFileName(file);
                if (options.ShouldRenameFiles && fileName.Contains(options.From))
                {
                    string newFileName = fileName.Replace(options.From, options.To);
                    string newLocation = Path.Combine(directory, newFileName);
                    File.Move(file, newLocation);
                }
            }
        }

        private static void ReplaceFileContent(string file, Options options)
        {
            if (options.ShouldRenameFileContents)
            {
                string content = File.ReadAllText(file);
                string replaced = content.Replace(options.From, options.To);
                if (content != replaced)
                {
                    File.WriteAllText(file, replaced);
                }
            }
        }

        static void VerifyGitFolder(Options options)
        {
            if (options.AllowNonGit)
            {
                return;
            }

            var gitDirectories = Directory.GetDirectories(
                options.Directory, 
                ".git", 
                SearchOption.TopDirectoryOnly);
            
            if (gitDirectories.Length == 0)
            {
                System.Console.WriteLine("This is not a source-controlled directory (has no .git folder). Skipping execution.");
                Environment.Exit(1);
            }
        }

        static void HandleParserError(IEnumerable<Error> errors)
        {

        }

        static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run)
                .WithNotParsed(HandleParserError);
        }
    }
}
