public static class Paths
{
    public static FilePath SolutionFile => "NETCore.sln";
    public static FilePath ProjectFile => "webapi/webapi.csproj";
    public static FilePath TestProjectFile => "test/test.csproj";
}

public static FilePath Combine(DirectoryPath directory, FilePath file)
{
    return directory.CombineWithFilePath(file);
}