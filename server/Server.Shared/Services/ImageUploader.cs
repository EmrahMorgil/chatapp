﻿namespace Server.Shared.Services;

public class ImageUploader
{
    public static string UploadImage(string image)
    {
        string[] allowedExtensions = { "jpg", "jpeg", "png", "gif", "bmp", "tif", "tiff", "svg", "webp", "ico", "psd", "heic", "heif" };
        string[] parts = image.Split(',');
        bool containsAllowedExtension = allowedExtensions.Any(extension => parts[0].Contains(extension));
        string imageName = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() + parts[0];
        imageName = imageName.Replace(" ", "");
        string directory = Path.Combine("wwwroot", "images", "users", imageName);

        if (containsAllowedExtension)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(parts[2]);
                File.WriteAllBytes(directory, imageBytes);
                return imageName;
            }
            catch (Exception)
            {
                return null!;
            }
        }
        else
        {
            return null!;
        }

    }
}
