using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Smart.Greenhouse.API.Core.Entities;
using Smart.Greenhouse.API.Core.Interfaces.Repositories;

namespace Smart.Greenhouse.API.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository implementation for sensor data operations
    /// </summary>
    public class SensorRepository : Repository<SensorData>, ISensorRepository
    {
        public SensorRepository(SensorDataContext context) : base(context)
        {
        }

        public async Task<SensorData> GetLatestAsync()
        {
            return await _dbSet
                .OrderByDescending(s => s.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SensorData>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(s => s.CreatedAt >= startDate && s.CreatedAt <= endDate)
                .OrderBy(s => s.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<SensorData>> GetByTimeframeAsync(string timeframe)
        {
            DateTime startDate;
            
            switch (timeframe.ToLower())
            {
                case "daily":
                    startDate = DateTime.Today;
                    break;
                case "weekly":
                    startDate = DateTime.Today.AddDays(-7);
                    break;
                case "monthly":
                    startDate = DateTime.Today.AddMonths(-1);
                    break;
                default:
                    startDate = DateTime.Today;
                    break;
            }

            return await _dbSet
                .Where(s => s.CreatedAt >= startDate)
                .OrderBy(s => s.CreatedAt)
                .ToListAsync();
        }

        public async Task ClearAllDataAsync()
        {
            var tableName = _context.Model.FindEntityType(typeof(SensorData)).GetTableName();
            await _context.Database.ExecuteSqlRawAsync($"DELETE FROM [{tableName}]");
            await _context.Database.ExecuteSqlRawAsync($"DBCC CHECKIDENT ('{tableName}', RESEED, 0)");
        }
    }
} 