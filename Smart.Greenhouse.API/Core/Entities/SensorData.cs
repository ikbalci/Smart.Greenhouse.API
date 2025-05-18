using System;
using System.ComponentModel.DataAnnotations;

namespace Smart.Greenhouse.API.Core.Entities
{
    public class SensorData
    {
        [Key]
        public int Id { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public double Moisture { get; set; }
        public double Temperature { get; set; }
        public double AirQuality { get; set; }
        public bool PumperOn { get; set; }
        public bool HeaterOn { get; set; }
        public bool CoolerOn { get; set; }
        public bool AirPurifierOn { get; set; }
        public string? TemperatureCategory { get; set; }
        public string? MoistureCategory { get; set; }
        public double HeatingDemand { get; set; }
        public double CoolingDemand { get; set; }
        public double MoistureTemperatureRatio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 