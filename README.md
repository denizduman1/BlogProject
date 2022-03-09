
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

<!-- Katmanlar -->
## Katmanlar
* BlogProject.Data
* BlogProject.Entities
* BlogProject.Mvc
* BlogProject.Services
* BlogProject.Shared

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
  </ol>

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/othneildrew/Best-README-Template.svg?style=for-the-badge
[contributors-url]: https://github.com/denizduman1/BlogProject/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/othneildrew/Best-README-Template.svg?style=for-the-badge
[forks-url]: https://github.com/denizduman1/BlogProject/network/members
[stars-shield]: https://img.shields.io/github/stars/othneildrew/Best-README-Template.svg?style=for-the-badge
[stars-url]: https://github.com/denizduman1/BlogProject/stargazers
[issues-shield]: https://img.shields.io/github/issues/othneildrew/Best-README-Template.svg?style=for-the-badge
[issues-url]: https://github.com/denizduman1/BlogProject/issues
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/deniz-duman-166a91218
