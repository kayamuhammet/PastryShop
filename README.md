# PastryShop - Pastane Yönetim Sistemi

Bu proje, bir pastanenin yönetimini sağlayan ASP.NET Core MVC tabanlı bir web uygulamasıdır. Müşterilerin ürünleri görüntüleyebileceği, sipariş verebileceği ve yorum yapabileceği bir platform sunarken, yöneticilerin de tüm süreçleri yönetebileceği kapsamlı bir yönetim paneli içermektedir.

## Özellikler

### Kullanıcı Tarafı
- Ürün kataloğu görüntüleme
- Kategori bazlı ürün listeleme
- Ürün yorumları ve değerlendirmeleri
- Sipariş oluşturma (Online ödeme yok)
- SSS (Sıkça Sorulan Sorular) sayfası
- Hakkımızda sayfası
- İletişim bilgisi

### Admin Paneli
- Dashboard (Son 7 günlük sipariş istatistikleri / grafiği)
- Ürün yönetimi
- Kategori yönetimi
- Slider yönetimi
- Günün tatlısı yönetimi
- Yorum yönetimi (Onaylama, silme)
- Sipariş yönetimi (Beklemede / Onaylandı / Hazırlanıyor / Tamamlandı / İptal edildi)
- Hakkımızda sayfası yönetimi
- Footer yönetimi

## Teknolojiler

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap
- JavaScript/jQuery
- HTML5/CSS3
- Chart.js (İstatistikler için)
- DataTables (Tablo yönetimi için)
- SweetAlert2 (Bildirimler için)

## Gereksinimler

- .NET 7.0 SDK veya üzeri
- SQL Server
- Visual Studio 2022 veya Visual Studio Code

## Kurulum

1. Projeyi klonlayın:
```bash
git clone https://github.com/kayamuhammet/PastryShop.git
```

2. Proje dizinine gidin:
```bash
cd PastryShop
```

3. Veritabanı bağlantı ayarlarını yapılandırın:
   - `appsettings.json` dosyasında ConnectionString'i düzenleyin

```C#
"ConnectionStrings": {
    "DefaultConnection": "Server=SERVER_NAME\\SQLEXPRESS;Database=DATABASE_NAME;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

4. Bağımlılıkları yükleyin:
```bash
dotnet restore
```

5. Veritabanı migration'larını uygulayın:
```bash
dotnet ef database update
```

6. Projeyi çalıştırın:
```bash
dotnet run
```

## Proje Yapısı

```
PastryShop/
├── Areas/
│   └── Admin/           # Admin paneli
│       ├── Controllers/    # Admin controller'ları
│       └── Views/          # Admin view'ları
├── Controllers/         # Normal kullanıcı (müşteri) işlemlerini yöneten controller
├── Data/                # Veritabanı context ve konfigürasyonları
├── Models/              # Veri modelleri
│       ├── Validators/     # Kullanıcı giriş/veri doğrulama kuralları (FluentValidation vs.)
│       └── ViewModels/     # View'lara özel veri taşıma sınıfları (UI için özel yapı)
├── Views/               # Normal kullanıcılar için view dosyaları
├── ViewComponents/      # Sayfalarda yeniden kullanılabilir UI bileşenleri
└── wwwroot/             # Statik dosyalar (CSS, JS, resimler)
```

## Katkıda Bulunma

1. Bu depoyu fork edin
2. Yeni bir branch oluşturun (`git checkout -b feature/yeniOzellik`)
3. Değişikliklerinizi commit edin (`git commit -m 'Yeni özellik: Açıklama'`)
4. Branch'inizi push edin (`git push origin feature/yeniOzellik`)
5. Pull Request oluşturun

## Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.

## İletişim

Proje Sahibi - [Muhammet KAYA](https://github.com/kayamuhammet)

Proje Linki - [PastryShop](https://github.com/kayamuhammet/PastryShop) 