using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Smart.Greenhouse.API.Core.DTOs;
using Smart.Greenhouse.API.Core.Entities;

namespace Smart.Greenhouse.API.Core.Interfaces
{
    public interface ISensorService
    {
        Task<SensorData> AddSensorDataAsync(SensorDataDto sensorData);
        Task<SensorDataDto> GetCurrentSensorDataAsync();
        Task<IEnumerable<SensorDataDto>> GetReportDataAsync(string timeframe);
        Task<IEnumerable<SensorDataDto>> GetHistoricalDataAsync(DateTime startDate, DateTime endDate);
    }
} 