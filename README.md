[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/denizduman1/BlogProject">
    <img src="https://www.chip.de/ii/1/2/6/3/5/8/2/8/3/Bild16.gif-4ff4769bc2e78c72.jpg" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">ASP.NET 5.0 ile Blog Projesi</h3>

  <p align="center">
    ASP.NET 5.0 Katmanlı Mimari ile Blog Projesi yapımı
    <br />
    <a href="https://github.com/denizduman1/BlogProject"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/denizduman1/BlogProject">View Demo</a>
    ·
    <a href="https://github.com/denizduman1/BlogProject/issues">Report Bug</a>
    ·
    <a href="https://github.com/denizduman1/BlogProject/issues">Request Feature</a>
  </p>
</div>

<!-- Projenin İçerikleri -->
## Projede kullanılan yapılar 
 * Entity Framework Core 
 * Generic Repository Pattern
 * Fluent API
 * Unit Of Work Design Pattern
 
 
<!-- Katmanlar -->
## Katmanlar
* BlogProject.Data
* BlogProject.Entities
* BlogProject.Mvc
* BlogProject.Services
* BlogProject.Shared

<!-- Commitler ve Yapılan İşler -->
## Commitler ve Yapılan İşler
### Commit: Veritabanı Nesneleri Oluşturuldu
 <ol>
  <li>BlogProject.Shared katmanı bizim tüm .net 5 projelerinde kullanabileceğimiz ortak bir class library'dir.</li>
  <li>İlgili katmana Entites adında klasör oluşturup ortak bir entity yapısını entegre ediyoruz.</li>
  <li>IEntity adında veritabanı sınıflarımızı işaret edecek olan (imzası) bir interface oluşturuyoruz</li>
  <li>EntityBase adında tüm veritabanı sınıflarına base görevi edinecek abstract base class'ımızı oluşturuyoruz.</li>
  <li>EntityBase sınıfında methodlar ezilebilir olması adına virtual keywordünü alırken tarih kısmına default şimdiki tarihi             alıyor.Abstract keywordünü koymamamızın nedeni başlangıç değeri alamaması ve mutlaka ezilmesi gerekmesidir. Virtual'da
      ise başlangıç değeri almalı ve daha sonradan ezilebilir olduğu için bize uygun olandır.</li>
  <li>BlogProject.Entites katmanına geliyoruz ve ilgili veri tabanı sınıflarını EntityBase sınıfından inheritiance alarak ve de
      IEntity interface imzasını implemente ederek hazırlıyoruz.</li>
  <li>Burada önemli bir kısım bir e çok ilişkili tabloları hazırlarken bir olacak sınıfa çok olan sınıfın ICollection
      yapısı tanımlanmasıdır. Çok durumda olan sınıf ta bir olan sınıftan ilgili sınıf adıyla property üretmesidir.
      Örnek durum: Bir olan User, public ICollection<Article> Articles { get; set; } iken çok olan Article ise
      public User User { get; set; } olarak tanımlanır.
  </li>
 </ol>
 
### Commit: Data (Veri Erişim) Katmanımızı Oluşturalım - 1.Kısım
  <ol>
    <li>Shared katmanımızda IEntityRepository interfacesini generic olarak oluşturuyoruz.</li> 
    <li>Generic olarak gelen T parametresi new'lene bilen IEntity interfacesini implemente eden bir class olmalıdır şartını ekliyoruz.</li> 
    <li>Asenkron methodları oluştururken Task ile tanımlıyoruz. Void methodlar void yerine task yazılarak; değer döndüren methodlar ise Task<T> (gibi) şeklinde yazılır.</li> 
    <li>Methodları abstract class'da tanımlarken de access modifier'dan sonra async keywordünü ekliyoruz.</li>  
    <li>Methoda girerken şartı olabilecek durumlar için parametre de delegate tanımlıyoruz. Func delegate'i T alırken bool döndürüyor.Şartı isteğe bağlı olabilecek methodlara
        null değeri ile başlangıç değeri atanıyor.</li>
    <li>Methodlarımıza ek olarak ilişkili tablolarda ilişkideki diğer tabloya erişmek için include params ekliyoruz. Bunu ekleyerek Lazy Loading(ilişkili tablolarda her başka       tablodan veri çağrıldığında sorgu yazan tek seferde getirmeyen mantık) yerine Eager Loading(Bağlı diğer tablolara daha çağrılmadan başlangıçta tek seferde getiren mantık)       mantığını getirmiş oluyoruz.</li>
    <li>Delegateleri Expression içerisine Func alarak tanımlıyoruz. Func olarak tanımladığımızda EF bunu anlamıyor ve tüm tablolara bakmak zorunda kalıyor. Expression
        veri ağacı oluşturup EF 'nin anlamasını sağlıyor ve de bu bize performans kazancı sağlatıyor.</li>
   <li>Data Katmanımızda veritabanımızdaki sınıfların interfacelerini oluşturup IEntityRepository<T> T içerisine ilgili sınıfı ekliyoruz.</li>
  </ol>
  
### Commit: Data (Veri Erişim) Katmanımızı Oluşturalım - 2.Kısım
  
  <ol>
    <li>Data katmanında veritabanı nesnelerine ait repository sınıflarını oluşturuyoruz.(Entity Framework klasörünün altında topluyoruz çünkü buradaki sınıfları            EFEntityRepositoryBase<T> abstract sınıfından kalıtacağız.)</li>
    <li>İlgili sınıftan abstract ettikten sonra interface Repolarımızı imza olarak ekliyoruz.(interfaceyi ekleme sebebimiz IoC için)</li>
    <li>Projemizin adı ile Context sınıfı oluşturuyoruz. BlogProjectContext adında olacak sınıfımız EF'den gelen DBContext sınıfından inheritiance alacak.Bu sınıfta ilgili veritabanı sınıflarının mssql tablolarını oluşturuyoruz. OnConfiguring methodunda sql serverimizin adresini ve ayarlarınızı yapıyoruz.</li>
    <li>OnConfiguring methodunda tanımladığımız Connect Timeout değeri varsayılan olarak 30'dur. 0 dan yüksek değer örnek 5, 5 saniye bağlanmak için bekle anlamına geliyor.
        0 verirsek bu bağlanana kadar bekle anlamına gelmektedir.
    </li>
  </ol>
  
### Commit: Fluent API ile Veritabanı için Mapping İşlemlerimizi Oluşturalım
  
  <ol>
      <li>Data Katmanımızda Concrete alanı içerisindeki EntityFramework alanındaki Mappings klasörüne Fluent API'yi entegre ediyoruz.</li>
      <li>Fluent API EntityFramework'den gelen bir sınıftır.Görevi, mssql tablolarını oluştururken veritabanı sınıf alanlarının         karşılıklarını belirlememize yardımcı olur(Map eder). Data Annotation yerine kullanıllır.</li>  
      <li>IEntityTypeConfiguration<T> generic interface'yi implemente ederiz.</li>  
      <li>Constructor oluşturup builder ile property lerimize varsa ilişkili diğer sınıfların ilişkisini ayarlarız.</li>  
      <li>Son olarak ToTable methodu ile ilgili entity'nin mssql deki oluşturacağı tablonun adını vermiş oluruz.</li>    
  </ol>
  
 ### Commit: Fluent API ile Veritabanı için Mapping İşlemlerimizi Oluşturalım - 2.Kısım
  <ol>
      <li>Mapping işlemlerini yaptıktan sonra Context sınıfımızda OnModelCreating methodunu ezerek(override) mapping sınıflarını configuration'larımıza ekliyoruz.</li>
  </ol>
 
  ### Commit: Unit Of Work Tasarım Deseni Projemize Uygulayalım
  <ol>
      <li>Data katmanımıza tüm repolarımızı kullanabileceğimiz IUnitOfWork interfacesi ve onu implemente eden UnitOfWork sınıfımızı oluşturuyoruz.</li>
      <li>UnitOfWork sınıfımızda ilgili repoları return'liyoruz ve en son save changes i gerçekleştirmesi için Asenkron SaveAsync methodunu oluşturuyoruz.</li>
      <li>Garbage collector normalde kullanılmayan sınıfları kendisi temizliyor ama ram'de yer var ise bu işlemi geciktirebiliyor. Bu yüzden IAsyncDisposable interfacesini IUnitOfWorke imza olarak ekliyoruz. DisposeAsync ile de ilgili sınıflarla işimiz bittikten sonra bellekten temizliyoruz.</li>
  </ol>
  
  ### Commit: Veritabanının Oluşturulması, İlk Verilerin Eklenmesi ve Migration İşlemleri
  <ol>
      <li>Veritabanına oluşturacağımız tablolara oluştururken ilk değerlerini de vermek için mappings klasöründe ilgili sınıflara HasData methodu ile verilerini ekliyoruz.
          Eğer veri yoksa otomatik olarak girdiğimiz değerleri veritabanına aktaracak.</li>
      <li>Artık migrationa hazırız. Powershell'den dotnet ef 5.0 kuruyoruz. Daha sonrasında migration yapmak için dotnet ef migrations add InitialCreate kodu yazıyoruz.</li>
      <li>İlgili migration sınıfı oluşturulduktan sonra veritabanına bunu set etmek için dotnet ef database update kodumuzu yazıyoruz ve artık yazdığımız sınıfların veritabanında ilgili database'de tablolarını ve initial value'larını görmüş oluyoruz.</li>
  </ol>
  
  ### Commit: Result (Sonuç) Yapımızı Oluşturalım
  
 <ol>
      <li>Shared katmanımıza service katmanında verileri çekerken sonuç değerini de görebilmek için IResult interfacesini  öncelikle ekliyoruz. IResult içerisinde ResultStatus adında enum(Success,Error,Warning,Information değerlerini içeren) bir property barındırıyor. Bunun dışında Mesaj ve Hata fırlatmasını içersin diye Exception türünden Exception propunu içeriyor.</li>
      <li>Shared katmanında Result adında IResult interfacesini implemente eden bir sınıf oluşturuyoruz. Propertylerinde sadece get methodu olduğu için ilgili değerleri constructordan alacak şekilde ayarlıyoruz</li>
      <li>IResult interfacesinden ayrı olarak geri döenecek bir data değeri olursa diye IDataResult tipinde IResult imzasınıda taşıyan ayri bir interface oluşturuyoruz. Interface data döneceği için <out T> tipinde generic yapıyoruz. Out keywordü koymamızın sebebi ise datanın hem T hem de IList<T> tipinde dönebilmesini sağlamaktır.</li>
  </ol>
  
<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/denizduman1/BlogProject.svg?style=for-the-badge
[contributors-url]: https://github.com/denizduman1/BlogProject/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/denizduman1/BlogProject.svg?style=for-the-badge
[forks-url]: https://github.com/denizduman1/BlogProject/network/members
[stars-shield]: https://img.shields.io/github/stars/denizduman1/BlogProject.svg?style=for-the-badge
[stars-url]: https://github.com/denizduman1/BlogProject/stargazers
[issues-shield]: https://img.shields.io/github/issues/denizduman1/BlogProject.svg?style=for-the-badge
[issues-url]: https://github.com/denizduman1/BlogProject/issues
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/deniz-duman-166a91218
