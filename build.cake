#load cake/paths.cake 
#load cake/projectInfo.cake  
#addin "Cake.Docker"

var target = Argument("target", "DockerCompose");
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
        DotNetCoreTest(Paths.TestProjectFile.FullPath);
    });
RunTarget(target);