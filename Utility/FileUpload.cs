namespace MadhurAPI.Utility
{
    public static class FileUpload
    {
        public static void upload(IFormFile file,string fileName)
        {
            var folderName = Path.Combine("Resource", "Kyc");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            var fullPath = Path.Combine(pathToSave, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
    }
}
