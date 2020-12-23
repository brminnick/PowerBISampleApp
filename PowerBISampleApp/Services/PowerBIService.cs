using System;
using System.Threading.Tasks;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;

namespace PowerBISampleApp
{
    abstract class PowerBIService : BasePowerBIService
    {
        public static async Task<Reports> GetReports()
        {
            await UpdateActivityIndicatorStatus(true).ConfigureAwait(false);

            try
            {
                var client = await GetPowerBIClient().ConfigureAwait(false);
                return client.Reports.GetReports();
            }
            catch(Exception e)
            {
                DebugHelpers.PrintException(e);
                return new Reports();
            }
            finally
            {
                await UpdateActivityIndicatorStatus(false).ConfigureAwait(false);
            }
        }
    }
}
