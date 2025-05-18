using AutoMapper;
using Smart.Greenhouse.API.Core.DTOs;
using Smart.Greenhouse.API.Core.Entities;
using Smart.Greenhouse.API.Core.Interfaces;
using Smart.Greenhouse.API.Core.Interfaces.Repositories;

namespace Smart.Greenhouse.API.Core.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;
        private readonly IMapper _mapper;

        public SensorService(ISensorRepository sensorRepository, IMapper mapper)
        {
            _sensorRepository = sensorRepository ?? throw new ArgumentNullException(nameof(sensorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SensorData> AddSensorDataAsync(SensorDataDto sensorDataDto)
        {
            var sensorData = _mapper.Map<SensorData>(sensorDataDto);
            return await _sensorRepository.AddAsync(sensorData);
        }

        public async Task<SensorDataDto> GetCurrentSensorDataAsync()
        {
            var latestData = await _sensorRepository.GetLatestAsync();
            return latestData != null ? _mapper.Map<SensorDataDto>(latestData) : null;
        }

        public async Task<IEnumerable<SensorDataDto>> GetReportDataAsync(string timeframe)
        {
            var data = await _sensorRepository.GetByTimeframeAsync(timeframe);
            return _mapper.Map<IEnumerable<SensorDataDto>>(data);
        }

        public async Task<IEnumerable<SensorDataDto>> GetHistoricalDataAsync(DateTime startDate, DateTime endDate)
        {
            var data = await _sensorRepository.GetByDateRangeAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<SensorDataDto>>(data);
        }
    }
} 