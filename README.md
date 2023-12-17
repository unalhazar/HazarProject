# HazarProject

*Kullanım
Uygulamanın kullanımı için HazarDB.db dosyasının yolunu appsettings.json içinde ConnectionString'i güncelleyerek kullanmaya başlayabilirsiz.

*Kullanılan Teknolojiler, Tasarımlar ve Mimariler
-Bu projede aşağıdaki teknolojiler kullanılmıştır:

.NET CORE 6: Uygulamanın temelini oluşturan framework.
Dapper: Veritabanı işlemleri için ORM aracı.
N-Tier Architecture Mimarisi kullanılmıştır.
CRUD işlemleri için Generic Repository ve BusinessLayer için Generic Service'de duruma bağlı kullanıma açıktır.
ASP.NET Core MVC: Web uygulaması geliştirme için kullanılan model-view-controller framework.
AutoMapper: Nesne-nesne eşlemeleri için kullanılır.
Serilog: Uygulama içi loglama için kullanılır.
FluentValidation: Giriş verilerinin doğrulanması için kullanılır.
SQLite: Veritabanı yönetim sistemi.
DTO kullanımı = Sadece her tablo için bir DTO tanımladım aslında normal olanı 
Request,Response şeklinde kullanmak veya DTO,ViewModel olarakda kullanım sergileyebilirdik ancak benim 2 tane küçük tablomun olması 
ve çok genişletmeyeceğim için sadece DTO kullandım ve AutoMapper'dan faydalandım.
!! Servislerimi Program.cs içinde tanımladım ileriki aşamalarda proje büyüme gösterir ise tek bir 
ServiceRegistiration adı altında servislerimizi tanımlayabilir ve program.cs içinde dahil edebilirsiniz.


