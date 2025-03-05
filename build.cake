#nullable enable
#addin nuget:?package=Cake.Git&version=5.0.0
#addin nuget:?package=Cake.Npm&version=5.1.0

// To use this, navigate to the directory containing the .sln file and run:
// - On the first run, install the Cake tool: 
//   - dotnet new tool-manifest
//   - dotnet tool install cake.tool
// - To run the build script:
//   - dotnet dotnet-cake
// - To run a specific target, for example, to the Build step:
//   - dotnet dotnet-cake --target=Build
// See more at https://cakebuild.net/

using System.Text.RegularExpressions;

// `target` indicates where to run through
// - e.g. dotnet dotnet-cake --target=Clean
var target             = Argument("target", "Test");
var configuration      = Argument("configuration", "Release");
var slnPath            = "InfiniLore.Lucide.sln";
var workingDirectory   = "tests/Tests.InfiniLore.Lucide";
                  
var nugetApi           = "https://api.nuget.org/v3/index.json";
var nugetApiKey        = EnvironmentVariable("NUGET_API_KEY");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

record BuildData(
        string? Version
    );

Setup(ctx =>
    {
        var tip = GitLogTip(".");
        var tags = GitTags(".", true);
        var tipTag = tags
            .FirstOrDefault(tag => tag.Target.Sha == tip.Sha)
            ;
        string? version = null;

        if (tipTag is not null)
        {
            var tagName = tipTag.FriendlyName;
            var match   = Regex.Match(tagName, @"^v(?<version>\d+\.\d+\.\d+)$");
            if (match.Success)
            {
                version = match.Groups["version"].Value;
                Information($"Tip is tagged with version: {version}");
            }
            else
            {
                Warning($"Tip is tagged, but the tag doesn't match the version schema: {tagName}");
            }
        }
        else
        {
            Information("Tip is not tagged with version");
        }

        return new BuildData(version);
    });

// Use `--rebuild` to clean the solution
Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
    {
        DotNetClean(slnPath, new()
        {
            Configuration = configuration,
        });
    });

Task("SetupNode")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        NpmInstall("./");
    });

Task("Restore")
    .IsDependentOn("SetupNode")
    .Does(() =>
    {
        DotNetRestore(slnPath);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetBuild(slnPath, new()
        {
            Configuration = configuration,
            NoRestore     = true,
        });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetTest(slnPath, new()
        {
            Configuration = configuration,
            NoBuild       = true,
            NoRestore     = true,
        });
    });

Task("Pack")
    .IsDependentOn("Test")
    .Does<BuildData>((ctx, bd) =>
    {
        var bs = new DotNetMSBuildSettings()
            .SetVersion(bd.Version??"0.0.1")
            ;

        DotNetPack(slnPath, new()
        {
            Configuration   = configuration,
            NoBuild         = true,
            NoRestore       = true,
            MSBuildSettings = bs
        });
    });

//Task("PublishCoreLibraryToNuGet")
//    .WithCriteria<BuildData>((ctx, bd) => bd.Version is not null)
//    .IsDependentOn("Pack")
//    .Does<BuildData>((ctx, bd) =>
//    {
//        var packPath = $"./src/nupkg/DataImportUtility.{bd.Version}.nupkg";
//        Information($"Publishing package: {packPath}");
//        DotNetNuGetPush(packPath, new()
//        {
//                ApiKey = nugetApiKey,
//                Source = nugetApi
//        });
//    });

Task("GithubAction")
    .IsDependentOn("Pack") // Remove after adding the PublishCoreLibraryToNuGet task
    //.IsDependentOn("PublishCoreLibraryToNuGet")
    ;

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);