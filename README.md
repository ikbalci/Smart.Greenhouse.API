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


## API Uç Noktaları

API belgelendirmesi ve test için Swagger UI kullanılmıştır. API çalıştırıldığında ana sayfada Swagger UI'a erişebilirsiniz.

## Proje Yapısı

- **API**: Controller'lar, middleware ve API ile ilgili diğer bileşenler
- **Core**: İş mantığı, arayüzler ve varlık modelleri
- **Infrastructure**: Veritabanı bağlantısı, repository implementasyonları ve diğer altyapı bileşenleri
