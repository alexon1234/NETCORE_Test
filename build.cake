#load cake/paths.cake 
#load cake/projectInfo.cake 
#tool nuget:?package=Codecov


#addin "Cake.Docker"
#addin nuget:?package=Cake.Coverlet
#addin nuget:?package=Cake.Codecov


var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");
var buildId = "";
var branch = "";

if(BuildSystem.TFBuild.IsRunningOnVSTS)
{
   branch= $"VSTS{BuildSystem.TFBuild.Environment.Repository.Branch}";
   buildId= (BuildSystem.TFBuild.Environment.Build.Id).ToString();
}
if(BuildSystem.AppVeyor.IsRunningOnAppVeyor)
{     
    branch=$"AppVeyor{BuildSystem.AppVeyor.Environment.Repository.Branch}";
    buildId= BuildSystem.AppVeyor.Environment.Build.Id; 
}

if (BuildSystem.TravisCI.IsRunningOnTravisCI)
{          
    branch= $"TravisCI{BuildSystem.TravisCI.Environment.Build.Branch}";            
    buildId= BuildSystem.TravisCI.Environment.Build.BuildId;
}
if(string.IsNullOrEmpty(buildId))
{
    buildId="github-action";
    branch="master";
}

Task("DockerCompose")
    .Does(() => {
        DockerComposeUp(new DockerComposeUpSettings{ForceRecreate=true,DetachedMode=true,Build=true});   
    });


Task("DockerLogin")
    .IsDependentOn("Test")
    .Does(() => {   
        var dockerPassword = EnvironmentVariable("DOCKER_PASSWORD");
        if(string.IsNullOrEmpty(dockerPassword))
        {
            throw new Exception("Could not get dockerPassword environment variable");
        }
        DockerLogin(new DockerRegistryLoginSettings{Password=dockerPassword,Username=Docker.Username});   
    });


Task("DockerBuild")
    .IsDependentOn("DockerLogin")
    .Does(() => {
        string [] tags = new string[]  { $"{Docker.Username}/{Docker.Repository}:{buildId}"};
        DockerBuild(new DockerImageBuildSettings{Tag=tags},".");   
    });


Task("DockerTag")
   .IsDependentOn("DockerBuild")
    .Does(() => {      
        bool IsVSTSMasterBrach = StringComparer.OrdinalIgnoreCase.Equals("VSTSmaster", branch);
        string tag="";
        if(IsVSTSMasterBrach && BuildSystem.TFBuild.IsRunningOnVSTS)
        {
            tag="latest";
        }
        else
        {    
            tag=$"{branch}-{buildId}";
        }
        DockerTag($"{Docker.Username}/{Docker.Repository}:{buildId}",$"{Docker.Username}/{Docker.Repository}:{tag}");   
    });

Task("DockerPush")
    .IsDependentOn("DockerTag")
    .Does(() => {     
        DockerPush($"{Docker.Username}/{Docker.Repository}");   
    });


Task("Restore")
    .Does(() =>
    {
        DotNetCoreRestore(Paths.SolutionFile.FullPath);
    }); 
	
Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetCoreBuild(
            Paths.ProjectFile.FullPath,
            new DotNetCoreBuildSettings
            {
                Configuration = configuration
            });
    });

Task("Test")
    .IsDependentOn("DockerCompose")
    .IsDependentOn("Restore")    
    .Does(() =>
    {
        var testSettings = new DotNetCoreTestSettings {
        };

        var coverletSettings = new CoverletSettings {
            CollectCoverage = true,
            CoverletOutputFormat = CoverletOutputFormat.opencover,
            CoverletOutputDirectory = Directory(@".\coverage-results\"),
            CoverletOutputName = $"results-{DateTime.UtcNow:dd-MM-yyyy-HH-mm-ss-FFF}"
        };

        DotNetCoreTest(Paths.TestProjectFile.FullPath, testSettings, coverletSettings);

        Invoke-WebRequest -Uri 'https://codecov.io/bash' -OutFile codecov.sh
        codecov.sh -f $"{coverletSettings.CoverletOutputDirectory}{coverletSettings.CoverletOutputName}.xml" -t "3aded4e7-5777-4d20-90de-6b1ea408ac27"

        // Codecov(
        //     coverletSettings.CoverletOutputDirectory + coverletSettings.CoverletOutputName, 
        //     "3aded4e7-5777-4d20-90de-6b1ea408ac27");     

        // var buildVersion = string.Format("{0}.build.{1}",
        //     variableThatStores_GitVersion_FullSemVer,
        //     BuildSystem.AppVeyor.Environment.Build.Version
        // );
        // var settings = new CodecovSettings {
        //     Files = new[] { $"{coverletSettings.CoverletOutputDirectory}{coverletSettings.CoverletOutputName}.xml" },
        //     EnvironmentVariables = new Dictionary<string,string> { { "APPVEYOR_BUILD_VERSION", buildVersion } }
        // };
        // Codecov(settings);   
    });



RunTarget(target);