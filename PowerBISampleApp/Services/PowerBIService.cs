using System;
using System.Threading.Tasks;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;

namespace PowerBISampleApp
{
    abstract class PowerBIService : BasePowerBIService
    {
        public static async Task<ODataResponseListReport> GetReports()
        {
            await UpdateActivityIndicatorStatus(true).ConfigureAwait(false);

            try
            {
                var client = await GetPowerBIClient().ConfigureAwait(false);

                return await client.Reports.GetReportsAsync();
            }
            catch(Exception e)
            {
                DebugHelpers.PrintException(e);
                return new ODataResponseListReport();
            }
            finally
            {
                await UpdateActivityIndicatorStatus(false).ConfigureAwait(false);
            }
        }
    }
}
