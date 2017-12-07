using System;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Microsoft.Rest;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;

using Plugin.Settings;

using Xamarin.Forms;

namespace PowerBISampleApp
{
    abstract class PowerBIService : BasePowerBIService
    {
        #region Methods
        public static async Task<ODataResponseListReport> GetReports()
        {
            var client = await GetPowerBIClient();

            return await client.Reports.GetReportsAsync();
        }
        #endregion
    }
}
