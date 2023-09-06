﻿namespace ValeECOS.ExternalInterface.GarbageCollection.TestHelpers.AppSettingsConfigHelper
{
    public static class SolutionPathHelper
    {
        public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(
            currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }
    }
}