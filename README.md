# Akıllı Sera Takip Sistemi API

Bu proje, sera içerisindeki sensörlerden alınan verileri takip etmek ve yönetmek için geliştirilmiş bir RESTful API'dir.

## Teknolojiler

* ASP.NET Core 8.0
* Entity Framework Core 7.0
* SQL Server
* AutoMapper
* Serilog
* Swagger

## Kurulum

### Gereksinimler

* .NET 8.0 SDK
* SQL Server (ya da başka bir veritabanı, bağlantı ayarlarını düzenlemeniz gerekir)
* Visual Studio 2022 veya Visual Studio Code

### Adımlar

1. Projeyi klonlayın:
   

2. Veritabanı bağlantısını yapılandırın:
   
   `appsettings.json` dosyasını aşağıdaki gibi düzenleyin:
   
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=SUNUCU_ADRESI;Database=VERITABANI_ADI;User ID=KULLANICI_ADI;Password=SIFRE;TrustServerCertificate=True;MultipleActiveResultSets=true"
     },
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*"
   }
   ```

3. Projeyi derleyin:
   ```
   dotnet build
   ```

4. Projeyi çalıştırın:
   ```
   dotnet run --project Smart.Greenhouse.API
   ```


## API Endpointleri

API belgelendirmesi ve test için Swagger UI kullanılmıştır. API çalıştırıldığında ana sayfada Swagger UI'a erişebilirsiniz.

## Proje Yapısı

- **API**: Controller'lar, middleware ve API ile ilgili diğer bileşenler
- **Core**: İş mantığı, arayüzler ve varlık modelleri
- **Infrastructure**: Veritabanı bağlantısı, repository implementasyonları ve diğer altyapı bileşenleri

## Yeni Özellik Ekleme Rehberi

Akıllı Sera Takip Sistemi API'ye yeni özellikler eklemek için izlenmesi gereken adımlar:

### 1. Veri Modelini Güncelleme

Yeni bir sensör verisi veya özellik eklemek için öncelikle `Core/Entities/SensorData.cs` dosyasını güncelleyin:

```csharp
public class SensorData
{
    // Mevcut özellikler...
    
    // Yeni özellik ekleme örneği:
    public bool NewFeatureStatus { get; set; }
}
```

### 2. DTO Sınıfını Güncelleme

Veri aktarımı için kullanılan DTO sınıfını da güncelleyin (`Core/DTOs/SensorDataDto.cs`):

```csharp
public class SensorDataDto
{
    // Mevcut özellikler...
    
    // Yeni özellik ekleme örneği:
    public bool NewFeatureStatus { get; set; }
}
```

### 3. Migration Oluşturma ve Veritabanını Güncelleme

Entity Framework Core ile migration oluşturup veritabanını güncelleyin:

```bash
# Migration oluşturma
dotnet ef migrations add AddNewFeature

# Veritabanını güncelleme
dotnet ef database update
```
