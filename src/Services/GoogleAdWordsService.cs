using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util.Reports;
using Google.Api.Ads.AdWords.v201809;
using Google.Api.Ads.Common.Util.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWordsApiExample.Services
{
    public class GoogleAdWordsService
    {
        private readonly AdWordsAppConfig _config;

        public GoogleAdWordsService(AdWordsAppConfig config)
        {
            _config = config;
        }

        public async Task<object> GetReport(string customerId)
        {
            var reportDefinition = new ReportDefinition()
            {
                reportName = "MyReportName",
                selector = new Selector()
                {
                    fields = new string[]
                    {
                        "",
                        "",
                        ""
                    }
                },
                downloadFormat = DownloadFormat.XML,
                reportType = ReportDefinitionReportType.AD_PERFORMANCE_REPORT,
            };

            _config.ClientCustomerId = customerId;
            var user = new AdWordsUser(_config);
            user.OAuthProvider.RefreshAccessTokenIfExpiring();
            var response = await GetResponse(user, reportDefinition);
            return await DownloadContent(response);
        }

        private async Task<ReportResponse> GetResponse(AdWordsUser user, IReportDefinition reportDefinition)
        {
            var utils = new ReportUtilities(user, reportDefinition);
            var response = default(ReportResponse);
            utils.OnReady = new AdsReportUtilities.OnReadyCallback((r) => response = r);
            utils.OnFailed = new AdsReportUtilities.OnFailedCallback((ex) => throw ex);

            var task = Task.Run(async () =>
            {
                while (response == default(ReportResponse))
                    await Task.Delay(300);

                return response;
            });
            utils.GetResponseAsync();

            return await task;
        }

        private async Task<object> DownloadContent(ReportResponse response)
        {
            var result = default(object);
            response.OnDownloadSuccess = new ReportResponse.OnDownloadSuccessCallback((bytes) => result = Encoding.UTF8.GetString(bytes));
            response.OnFailed = new ReportResponse.OnFailedCallback((ex) => result = ex.Message);
            var task = Task.Run(async () =>
            {
                while (result == null)
                    await Task.Delay(1000);

                return result;
            });

            response.DownloadAsync();
            return await task;
        }
    }
}
