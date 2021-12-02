### Web veya mobil uygulamalar tarafından kullanılabilecek web servis yapısında bir telefon rehber uygulamasıdır.

# Kullanılan Teknolojiler
- .NET 5
- MSSQL v18.10
- REDIS 3.2.100
- Ocelot 17.0.0

# Projenin İşlevleri
- Rehberde kişi oluşturma
- Rehberde kişi güncelleme
- Rehberde kişi silme
- Rehberdeki kişilerin listelenmesi
- Rehberdeki kişiye iletişim bilgisi ekleme
- Rehberdeki kişiden iletişim bilgisi kaldırma
- Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin 
getirilmesi
- Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor 
talebi
  - Konum Bilgisi
    - En çok -> En az olacak şekilde konumlarının sayılarıyla listelenmesi
  - O konumda yer alan rehbere kayıtlı kişi sayısı
  - O konumda yer alan rehbere kayıtlı telefon numarası sayısı

# Apilerin Çalıştıkları Port Numaraları
- **Api Gateway:** 13347
- **Report Api:** 4000
- **Communication Information Api:** 7000
- **Directory Users Api:** 8000
- **Company Api:** 9000

# ApiGateway Endpoints
- Report Api
  - **[GET]** /service/v{version}/Report/GetSortByLocation
  - **[GET]** /service/v{version}/Report/GetUserCountByLocation/{location}
  - **[GET]** /service/v{version}/Report/GetPhoneNumberCountByLocation/{location}
- Communication Information Api
  - **[GET]** /service/v{version}/CommunicationInformation
  - **[POST]** /service/v{version}/CommunicationInformation
  - **[PUT]** /service/v{version}/CommunicationInformation
  - **[GET]** /service/v{version}/CommunicationInformation/{id}
  - **[DELETE]** /service/v{version}/CommunicationInformation/{id}
- Directory Users Api- 
  - **[GET]** /service/v{version}/DirectoryUsers
  - **[POST]** /service/v{version}/DirectoryUsers
  - **[PUT]** /service/v{version}/DirectoryUsers
  - **[GET]** /service/v{version}/DirectoryUsers/{id}
  - **[DELETE]** /service/v{version}/DirectoryUsers/{id}
  - **[GET]** /service/v{version}/DirectoryUsers/{userId}/information/{directoryUserId}
  - **[POST]** /service/v{version}/DirectoryUsers/{userId}/information/{directoryUserId}
  - **[DELETE]** /service/v{version}/DirectoryUsers/{userId}/directory/{directoryUserId}/information/{informationId}
- Company Api
  - **[GET]** /service/v{version}/Company
  - **[POST]** /service/v{version}/Company
  - **[PUT]** /service/v{version}/Company
  - **[DELETE]** /service/v{version}/Company
  - **[GET]** /service/v{version}/Company/{id}

### Notlar
- Tüm API endpointleri saniye başına 1 istek cevaplayabilecek şekilde oluşturulmuştur.
