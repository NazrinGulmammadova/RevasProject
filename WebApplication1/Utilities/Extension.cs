namespace WebApplication1.Utilities;

public static class Extension
{
    public static bool CheckFileSize(this IFormFile file, int kb)
    {
        return file.Length / 1024 <= kb;
    }
    public static bool CheckFileFormat(this IFormFile file, string format)
    {
        return file.ContentType.Contains(format);
    }
    public static async Task<string> CopyToFileAsync(this IFormFile file, string wwwroot, params string[] folders)
    {
        try
        {
            var filename = Guid.NewGuid().ToString() + file.FileName;
            var resultPath = wwwroot;
            foreach (var folder in folders)
            {
                resultPath = Path.Combine(folder, resultPath);
            }
            resultPath = Path.Combine(resultPath, filename);
            using (FileStream stream = new(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                return filename;
            }
            return filename;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
