using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client.Extensions.Msal;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLC
{
    internal class FirebaseOperations
    {
        public string downloadUrl;
        IFormFile file;
        public FirebaseOperations(IFormFile file)
        {
            this.file = file;   
        }

        async public void UpdownImage()
        {
            var serviceAccountKeyPath = "C:\\Users\\ali\\Documents\\e-newsapi\\BLC\\e-news-fbebf-firebase-adminsdk-taszh-25f7f16486.json"; // Path to your service account key file
            var bucketName = "e-news-fbebf.appspot.com";
            var credential = GoogleCredential.FromFile(serviceAccountKeyPath);
            var storage = StorageClient.Create(credential);

            //var bucket = storage.GetBucket(bucketName);

            var uniqueFileName = Path.GetRandomFileName(); // Generate a unique filename
            var storageObject = await storage.UploadObjectAsync(bucketName, uniqueFileName, null, file.OpenReadStream());

            // Set metadata for the uploaded image file
            var metadata = new Dictionary<string, string>()
        {
            { "content-type", file.ContentType },
            { "firebaseStorageDownloadTokens", Guid.NewGuid().ToString() } // Add a unique token for image download access
            // Add any additional metadata properties as needed
        };

            storageObject.Metadata = metadata;
            await storage.UpdateObjectAsync(storageObject);

            // Get the file download URL
            var downloadUrl = $"https://firebasestorage.googleapis.com/v0/b/{bucketName}/o/{storageObject.Name}?alt=media&token={metadata["firebaseStorageDownloadTokens"]}";
            this.downloadUrl = downloadUrl;
        }
        
    }
}
