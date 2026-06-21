# 3D Doğal Afet Bilinci ve Hayatta Kalma Simülasyonu
Bursa Uludağ Üniversitesi Yönetim Bilişim Sistemleri (YBS) Dönem Sonu Projesi kapsamında geliştirilmiş, 3 boyutlu birinci kişilerin (FPS) afet yönetimi  simülasyonudur.

Bu proje, oyuncuların acil durum ve doğal afet anlarında doğru refleksleri göstermesini, stres altında hızlı ve doğru karar verme yeteneklerini geliştirmesini amaçlayan **Unity** ile geliştirilmiş 3 boyutlu bir eğitim ve farkındalık oyunudur. 

Klasik eğitici oyun kalıplarından uzaklaşarak, oyunlaştırma dinamikleri ve fantastik ögeleri acil durum prosedürleriyle birleştirmeyi hedefler.

---

## 🎮 Oyun Kurgusu ve Hikayesi
Sıradan bir ev ortamında başlayan macera, her odanın farklı bir doğal afet senaryosuna ev sahipliği yapmasıyla bir hayatta kalma mücadelesine dönüşür. 

Oyunu benzersiz kılan en büyük özelliklerden biri, her odada o afetin doğasını ve elementini temsil eden benzersiz bir **Pokémon**'un bulunmasıdır.

### ⚠️ Sert Kurallar (3 Hata Sistemi)
Gerçek hayatta doğal afetlerin hata payı olmadığı gibi, bu simülasyonda da kurallar oldukça katıdır:
* Oyuncunun test sorularında veya fiziksel görevlerde toplam **3 hata yapma hakkı** vardır.
* Üçüncü hata yapıldığı an zaman kırılır, oyun sıfırlanır ve oyuncu doğrudan **Başlangıç Ekranına (Ana Menü)** geri gönderilir.

---

## 🚪 Odalar ve Mekanikler

Oyun, her biri farklı bir afeti ve alınması gereken önlemleri temsil eden 3 ana bölümden oluşmaktadır:

1. **Yatak Odası (Deprem Senaryosu)**
   * **İçerik:** Şiddetli bir sarsıntı ile başlar.
   * **Görev:** Oyuncu saniyeler içinde doğru noktayı tespit edip **Çök-Kapan-Tutun** (Drop-Cover-Hold on) hareketini başarıyla uygulamalıdır.

2. **Banyo (Sel Senaryosu)**
   * **İçerik:** Odada sular hızla yükselir ve su baskını başlar.
   * **Görev:** Elektrik çarpması riskine karşı oyuncunun en hızlı şekilde **ana şalteri indirmesi** gerekir.

3. **Mutfak (Yangın Senaryosu)**
   * **İçerik:** Ocaktaki tavanın alev almasıyla duman ve ateş etrafı sarar.
   * **Görev:** Doğru adımlarla ilerleyerek **yangın tüpünü kullanıp ateşi söndürmek** ve vakit kaybetmeden acil yardım için **112'yi aramak**.

---

## 🏆 Oyunlaştırma ve Skor Sistemi
Tüm odaları başarıyla tamamlayan oyuncular, finalde detaylı bir istatistik paneliyle karşılaşırlar:
* **Güncel Skor & Rekor Skor:** Doğru kararlar puan kazandırırken, hatalar can kaybettirir.
* **Süre & En İyi Süre:** Bölümlerin ne kadar sürede tamamlandığını takip ederek oyuncuyu tekrar oynamaya teşvik eder.
* Başarı durumuna göre kupa ve rozet ödüllendirmeleri içerir.

---

## 🛠️ Kullanılan Teknolojiler
* **Oyun Motoru:** Unity (3D)
* **Programlama Dili:** C#
* **Ses Tasarımı:** Mekana duyarlı 3D Ses Sistemi (Spatial Blend - Audio Source)
* **Arayüz Kontrolleri:** Responsive Canvas UI (Scale With Screen Size)

---
Bu proje, oyuncuların acil durum ve doğal afet anlarında doğru refleksleri göstermesini, stres altında hızlı ve doğru karar verme yeteneklerini geliştirmesini amaçlayan **Unity** ile geliştirilmiş 3 boyutlu bir eğitim ve farkındalık oyunudur. 

Klasik eğitici oyun kalıplarından uzaklaşarak, oyunlaştırma dinamikleri ve fantastik ögeleri acil durum prosedürleriyle birleştirmeyi hedefler.

---
## 🎥 Tanıtım Videosu
Oyunumuzun atmosferini, mekaniklerini ve genel oynanışını görmek için aşağıdaki görsele tıklayarak YouTube üzerinden tanıtım videomuzu izleyebilirsiniz:

[Oyun Tanıtım Videosu]https://youtu.be/zEIQbh5sVQs
>
---
## 📸 Oyundan Kareler

Aşağıda oyunumuzun farklı aşamalarından ve afet senaryolarından görselleri inceleyebilirsiniz:

<p align="center">
  <img width="48%" height="835" alt="giriş" src="https://github.com/user-attachments/assets/68f4a82b-4304-4af1-992e-7e595bcc6167" />
  <img width="48%" height="838" alt="Ekran görüntüsü 2026-06-21 165055" src="https://github.com/user-attachments/assets/1045e30f-d684-4b16-9a22-a4359fe777c2" />

</p>
<p align="center">
  <img width="48%" height="939" alt="mutfak" src="https://github.com/user-attachments/assets/d34c7acc-98e9-4b36-b31c-58260d69a76f" />
  <img width="48%" height="842" alt="mutfak1" src="https://github.com/user-attachments/assets/50d7a330-1914-4672-80d1-fa372e349665" />
  <img width="48%" height="844" alt="Ekran görüntüsü 2026-06-21 164949" src="https://github.com/user-attachments/assets/35d41344-773f-4f7e-85e1-134ab529683e" />
  <img width="48%" height="839" alt="AraHol" src="https://github.com/user-attachments/assets/746bbbea-d126-4cdf-8d6d-4f1f277d7388" />
  <img width="48%" height="846" alt="telefon" src="https://github.com/user-attachments/assets/6394df67-2450-452b-b824-1c4578b486f6" />
  <img width="48%" height="841" alt="telefon2" src="https://github.com/user-attachments/assets/24eb381a-c538-4b91-a79a-89e1c77c27eb" />
  <img width="48%" height="842" alt="telefon1" src="https://github.com/user-attachments/assets/5f8b9644-4ff4-4bad-8f53-8abc82e0c5d2" />
  <img width="48%" height="843" alt="bitiş" src="https://github.com/user-attachments/assets/783b4745-6b53-4b25-8fc8-cb13d1c085f3" />
</p>


---

## 🚀 Kurulum ve Çalıştırma
Projeyi kendi yerel ortamınızda çalıştırmak veya incelemek için:

1. Bu depoyu klonlayın:
   ```bash
   git clone [https://github.com/Ferhat-01/Unity_proje.git](https://github.com/Ferhat-01/Unity_proje.git)

## İletişim Bilgileri
Ad:Ferhat  </p>
Soyad:Gönülveren </p>
Gmail:132430027@ogr.uludag.edu.tr </p>
Bursa Uludağ Üniversitesi 2.sınf öğrencisi
   
