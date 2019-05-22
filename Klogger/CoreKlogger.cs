using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Serilog;
using Serilog.Events;
using System;

namespace Klogger
{
    public static class CoreKlogger
    {
        private static readonly CloudStorageAccount accountBlob = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=functodo91f1;AccountKey=g5lus//Xg8DTmJE7BG2qUiD08+mVjZACKImsQkFEMBGlEnAqEkFGCemI5pJZqlAtpp0HnrulbOsLnVWhVE0aEg==;BlobEndpoint=https://functodo91f1.blob.core.windows.net/;QueueEndpoint=https://functodo91f1.queue.core.windows.net/;TableEndpoint=https://functodo91f1.table.core.windows.net/;FileEndpoint=https://functodo91f1.file.core.windows.net/;");

        static CoreKlogger()
        {
            DateTime today = DateTime.Today;
            var year = today.Year;
            var month = today.Month;
            var day = today.Day;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.AzureBlobStorage(accountBlob, Serilog.Events.LogEventLevel.Information, "app-logs", string.Format("{0}/{1}/{2}/log.csv", year, month, day))
                .CreateLogger();
        }

        public static void WriteUsage(KloggerDetail infoToLog)
        {
            Log.Logger.Write(LogEventLevel.Information, "{@KloggerDetail}", infoToLog);
            CloudBlobClient cloudBlobClient = accountBlob.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("app-logs");
            cloudBlobContainer.CreateIfNotExists();

            DateTime today = DateTime.Today;
            var year = today.Year;
            var month = today.Month;
            var day = today.Day;

            CloudAppendBlob cloudAppendBlob = cloudBlobContainer.GetAppendBlobReference(string.Format("{0}/{1}/{2}/log.csv", year, month, day));
            cloudAppendBlob.AppendText("Content added");


        }
    }
}
