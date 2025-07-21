# 📡 PulseChat – Real-Time Messaging API

PulseChat, ASP.NET Core ile geliştirilmiş, gerçek zamanlı mesajlaşma destekli bir API servisidir. Proje, JWT tabanlı kimlik doğrulama, SignalR ile anlık iletişim, PostgreSQL + Redis desteği ve Docker ile konteynerleştirilmiş altyapıya sahiptir.

---

## 🛠️ Teknolojiler

- ASP.NET Core Web API
- SignalR (gerçek zamanlı iletişim)
- PostgreSQL (veritabanı)
- Redis (önbellekleme)
- JWT + Refresh Token Authentication
- Entity Framework Core
- xUnit + Moq (birim testleri)
- Docker & Docker Compose
- Onion Architecture
- CI/CD (hazırlık tamamlandı)

---

## 📂 Proje Yapısı (Onion Architecture)

PulseChat/
│
├── PulseChat.API # Web API
├── PulseChat.Application # Servis katmanı (DTOs, Interfaces, Services)
├── PulseChat.Domain # Domain modelleri (Entities)
├── PulseChat.Infrastructure # Veritabanı ve dış servis implementasyonları
├── PulseChat.Tests # Unit test projeleri (xUnit)

yaml
Kopyala
Düzenle

---

## 🚀 Nasıl Çalıştırılır?

### 1️⃣ Docker ile Başlat

```bash
docker-compose up --build
Docker Compose, PostgreSQL veritabanını çalıştırır. API ise launchSettings.json veya varsayılan https://localhost:5001 adresinden çalışır.

2️⃣ Veritabanı Migration (İlk Kurulum)
bash
Kopyala
Düzenle
dotnet ef database update --project PulseChat.Infrastructure
🔐 Kimlik Doğrulama
Uygulama JWT + Refresh Token tabanlı kimlik doğrulama sistemi kullanır.

Kayıt ve Giriş:

POST /api/auth/register

POST /api/auth/login

POST /api/auth/refresh

💬 Mesajlaşma Özellikleri
Özellik	Endpoint
Mesaj Gönderme	POST /api/message
Dosyalı Mesaj	POST /api/message/send-with-file
Mesajları Getir	GET /api/message/group/{groupId}
Mesaj Sil	DELETE /api/message/{messageId}?senderId={senderId}
Mesaj Güncelle	PUT /api/message/{messageId}
Mesaj Arama	GET /api/message/search (pagination destekli)

👥 Gruplar
Grup oluşturma: POST /api/group

Tüm grupları listeleme: GET /api/group

✅ Testler
Testler PulseChat.Tests projesi içinde xUnit ve Moq ile yazılmıştır.

Testleri çalıştırmak için:

bash
Kopyala
Düzenle
dotnet test
📦 CI/CD
CI/CD işlemleri için GitHub Actions veya GitLab CI kullanılabilir. Docker build + test aşamaları başarıyla çalışmaktadır.

📄 Lisans
Bu proje MIT lisansı ile lisanslanmıştır.
