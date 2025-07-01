using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Smart.Greenhouse.API.Core.Entities;

namespace Smart.Greenhouse.API.Core.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for sensor data operations
    /// </summary>
    public interface ISensorRepository : IRepository<SensorData>
    {
        /// <summary>
        /// Get the most recent sensor data
        /// </summary>
        Task<SensorData> GetLatestAsync();
        
        /// <summary>
        /// Get sensor data between specified dates
        /// </summary>
        Task<IEnumerable<SensorData>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Get sensor data from a specific timeframe (daily, weekly, monthly)
        /// </summary>
        Task<IEnumerable<SensorData>> GetByTimeframeAsync(string timeframe);

        /// <summary>
        /// Deletes all sensor data from the table and resets the identity.
        /// </summary>
        Task ClearAllDataAsync();
    }
} 