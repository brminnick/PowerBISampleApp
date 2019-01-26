using System;
using System.Threading.Tasks;

using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;

namespace PowerBISampleApp
{
    abstract class PowerBIService : BasePowerBIService
    {
        #region Methods
        public static async Task<ODataResponseListReport> GetReports()
        {
            UpdateActivityIndicatorStatus(true);

            try
            {
                var client = await GetPowerBIClient().ConfigureAwait(false);

                return await client.Reports.GetReportsAsync();
            }
            catch(Exception e)
            {
                DebugHelpers.PrintException(e);
                return null;
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }
        #endregion
    }
}
