﻿using System;
using System.Collections.Generic;
using System.Text;
using LibGit2Sharp;

namespace SharpGLTF
{
    /// <summary>
    /// Utility class to download the gltf2 schema from github
    /// </summary>
    static class SchemaDownload
    {
        public static void Syncronize(string remoteUrl, string remoteBranch, string localDirectory)
        {
            if (LibGit2Sharp.Repository.Discover(localDirectory) == null)
            {
                Console.WriteLine($"Cloning {remoteUrl} can take several minutes; Please wait...");

                LibGit2Sharp.Repository.Clone(remoteUrl, localDirectory, new CloneOptions() { BranchName = remoteBranch });

                Console.WriteLine($"... Clone Completed");
                
                return;
            }

            using (var repo = new LibGit2Sharp.Repository(localDirectory))
            {
                var options = new LibGit2Sharp.PullOptions
                {
                    FetchOptions = new LibGit2Sharp.FetchOptions()
                };

                var r = LibGit2Sharp.Commands.Pull(repo, new LibGit2Sharp.Signature("Anonymous", "anon@anon.com", new DateTimeOffset(DateTime.Now)), options);

                Console.WriteLine($"{remoteUrl} is {r.Status}");
            }

        }
    }
}
