using Microsoft.AspNetCore.Mvc;
using Smart.Greenhouse.API.Core.Entities;
using Smart.Greenhouse.API.Core.Interfaces.Repositories;
using System.Globalization;

namespace Smart.Greenhouse.API.API.Controllers
{
    /// <summary>
    /// Test amaçlı dummy verileri yönetmek için kullanılır.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DataSeedController : ControllerBase
    {
        private readonly ISensorRepository _sensorRepository;
        private readonly Random _random;
        private readonly ILogger<DataSeedController> _logger;

        public DataSeedController(ISensorRepository sensorRepository, ILogger<DataSeedController> logger)
        {
            _sensorRepository = sensorRepository ?? throw new ArgumentNullException(nameof(sensorRepository));
            _random = new Random();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Belirtilen gün sayısı ve günlük kayıt adedine göre dummy sensör verisi oluşturur.
        /// </summary>
        /// <param name="days">Kaç gün geriye gidileceği (Örn: 90)</param>
        /// <param name="recordsPerDay">Her gün için oluşturulacak kayıt sayısı (Örn: 10)</param>
        /// <returns>Oluşturulan toplam kayıt sayısı hakkında bilgi.</returns>
        [HttpPost("generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateDummyData([FromQuery] int days = 90, [FromQuery] int recordsPerDay = 10)
        {
            if (days <= 0 || recordsPerDay <= 0)
            {
                return BadRequest("Gün sayısı ve günlük kayıt adedi pozitif olmalıdır.");
            }

            try
            {
                var createdRecords = 0;
                var startDate = DateTime.Now.AddDays(-days);

                for (int day = 0; day < days; day++)
                {
                    var currentDate = startDate.AddDays(day);
                    double interval = 24.0 / recordsPerDay;

                    for (int record = 0; record < recordsPerDay; record++)
                    {
                        var recordTime = currentDate.AddHours(record * interval);
                        var sensorData = GenerateDummySensorEntity(recordTime);
                        await _sensorRepository.AddAsync(sensorData);
                        createdRecords++;
                    }
                }

                return Ok(new
                {
                    Message = $"{createdRecords} adet dummy veri başarıyla oluşturuldu.",
                    DaysGenerated = days,
                    RecordsPerDay = recordsPerDay,
                    TotalRecords = createdRecords,
                    StartDate = startDate.ToString("dd MMM yyyy", CultureInfo.InvariantCulture),
                    EndDate = DateTime.Now.ToString("dd MMM yyyy", CultureInfo.InvariantCulture)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dummy veri oluşturulurken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Dummy veri oluşturulurken bir hata oluştu.", Error = ex.Message });
            }
        }

        /// <summary>
        /// TABLODAKİ HER ŞEYİ SİLER. DİKKAT!!!!!!
        /// </summary>
        /// <returns>İşlemin başarı durumunu bildiren bir mesaj.</returns>
        [HttpDelete("clear-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ClearAllData()
        {
            try
            {
                await _sensorRepository.ClearAllDataAsync();
                return Ok(new { Message = "Tüm sensör verileri başarıyla silindi ve ID sayacı sıfırlandı." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Veriler silinirken bir hata oluştu.", Error = ex.Message });
            }
        }

        /// <summary>
        /// Veritabanındaki toplam veri sayısını döndürür.
        /// </summary>
        /// <returns>Toplam kayıt sayısı.</returns>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecordCount()
        {
            try
            {
                var count = await _sensorRepository.CountAsync();
                return Ok(new { TotalRecords = count });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Kayıt sayısı alınırken bir hata oluştu.", Error = ex.Message });
            }
        }

        private SensorData GenerateDummySensorEntity(DateTime dateTime)
        {
            var hour = dateTime.Hour;
            var baseTemperature = 22 + Math.Sin((hour - 6) * Math.PI / 12) * 10;
            var temperature = baseTemperature + _random.NextDouble() * 6 - 3;
            var moisture = 30 + _random.NextDouble() * 60;
            var airQuality = 40 + _random.NextDouble() * 60;

            string tempCategory = temperature < 18 ? "LOW" : temperature > 28 ? "HIGH" : "OPTIMAL";
            string moistureCategory = moisture < 40 ? "LOW" : moisture > 70 ? "HIGH" : "OPTIMAL";

            return new SensorData
            {
                Date = dateTime.ToString("dd MMM yyyy", CultureInfo.InvariantCulture),
                Time = dateTime.ToString("HH:mm"),
                Temperature = Math.Round(temperature, 1),
                Moisture = Math.Round(moisture, 1),
                AirQuality = Math.Round(airQuality, 0),
                TemperatureCategory = tempCategory,
                MoistureCategory = moistureCategory,
                PumperOn = moisture < 40,
                HeaterOn = temperature < 18,
                CoolerOn = temperature > 28,
                AirPurifierOn = airQuality < 60,
                HeatingDemand = Math.Max(0, 20 - temperature) * 0.5,
                CoolingDemand = Math.Max(0, temperature - 25) * 0.5,
                MoistureTemperatureRatio = temperature != 0 ? Math.Round(moisture / temperature, 2) : 0,
                CreatedAt = dateTime
            };
        }
    }
} 