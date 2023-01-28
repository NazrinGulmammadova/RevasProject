namespace WebApplication1.Utilities;

public static class Helper
{
    public static bool DeleteFile(params string[] paths)
    {
        var resultPath=string.Empty;
        foreach (var path in paths)
        {
            resultPath=Path.Combine(resultPath,path);
        }
        if(File.Exists(resultPath))
        {
            File.Delete(resultPath);
            return true;
        }
        return false;
    }
}
