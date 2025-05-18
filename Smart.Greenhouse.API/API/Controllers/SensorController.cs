using Microsoft.AspNetCore.Mvc;
using Smart.Greenhouse.API.Core.DTOs;
using Smart.Greenhouse.API.Core.Interfaces;

namespace Smart.Greenhouse.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;
        private readonly ILogger<SensorController> _logger;

        public SensorController(ISensorService sensorService, ILogger<SensorController> logger)
        {
            _sensorService = sensorService ?? throw new ArgumentNullException(nameof(sensorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Adds new sensor data
        /// </summary>
        /// <param name="sensorData">Sensor data to add</param>
        /// <returns>Created sensor data</returns>
        /// <response code="201">Returns the newly created sensor data</response>
        /// <response code="400">If the sensor data is invalid</response>
        [HttpPost("data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSensorData([FromBody] SensorDataDto sensorData)
        {
            _logger.LogInformation("Adding new sensor data");
            
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid sensor data model state");
                return BadRequest(ModelState);
            }

            var result = await _sensorService.AddSensorDataAsync(sensorData);
            _logger.LogInformation("Sensor data added successfully with ID: {Id}", result.Id);
            
            return CreatedAtAction(nameof(GetCurrentData), new { id = result.Id }, result);
        }

        /// <summary>
        /// Gets the most recent sensor data
        /// </summary>
        /// <returns>The most recent sensor data</returns>
        /// <response code="200">Returns the most recent sensor data</response>
        /// <response code="404">If no sensor data is available</response>
        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SensorDataDto>> GetCurrentData()
        {
            _logger.LogInformation("Getting current sensor data");
            
            var data = await _sensorService.GetCurrentSensorDataAsync();
            if (data == null)
            {
                _logger.LogWarning("No sensor data available");
                return NotFound("No sensor data available");
            }

            _logger.LogInformation("Current sensor data retrieved successfully");
            return Ok(data);
        }

        /// <summary>
        /// Gets sensor data for a specific timeframe
        /// </summary>
        /// <param name="timeframe">Timeframe (daily, weekly, monthly)</param>
        /// <returns>Sensor data for the specified timeframe</returns>
        /// <response code="200">Returns the sensor data for the specified timeframe</response>
        /// <response code="400">If the timeframe is invalid</response>
        [HttpGet("report")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SensorDataDto>>> GetReportData([FromQuery] string timeframe)
        {
            _logger.LogInformation("Getting report data for timeframe: {Timeframe}", timeframe);
            
            if (string.IsNullOrEmpty(timeframe) || !new[] { "daily", "weekly", "monthly" }.Contains(timeframe.ToLower()))
            {
                _logger.LogWarning("Invalid timeframe: {Timeframe}", timeframe);
                return BadRequest("Invalid timeframe. Must be 'daily', 'weekly', or 'monthly'");
            }

            var data = await _sensorService.GetReportDataAsync(timeframe);
            _logger.LogInformation("Report data retrieved successfully for timeframe: {Timeframe}", timeframe);
            
            return Ok(data);
        }

        /// <summary>
        /// Gets historical sensor data between specified dates
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Historical sensor data</returns>
        /// <response code="200">Returns the historical sensor data</response>
        /// <response code="400">If the date range is invalid</response>
        [HttpGet("history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SensorDataDto>>> GetHistoricalData(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            _logger.LogInformation("Getting historical data from {StartDate} to {EndDate}", startDate, endDate);
            
            if (startDate > endDate)
            {
                _logger.LogWarning("Invalid date range: start date {StartDate} is later than end date {EndDate}", startDate, endDate);
                return BadRequest("Start date cannot be later than end date");
            }

            var data = await _sensorService.GetHistoricalDataAsync(startDate, endDate);
            _logger.LogInformation("Historical data retrieved successfully from {StartDate} to {EndDate}", startDate, endDate);
            
            return Ok(data);
        }
    }
} 