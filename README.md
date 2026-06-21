# 3D Doğal Afet Bilinci ve Hayatta Kalma Simülasyonu

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

[![Oyun Tanıtım Videosu](https://img.youtube.com/vi/VİDEO_ID_BURAYA/maxresdefault.jpg)](https://www.youtube.com/watch?v=VİDEO_ID_BURAYA)
> **Not:** `VİDEO_ID_BURAYA` yazan kısımlara YouTube videonuzun linkindeki v= kısmından sonraki kodunu yapıştırmalısınız (Örn: dQw4w9WgXcQ).

---
## 📸 Oyundan Kareler

Aşağıda oyunumuzun farklı aşamalarından ve afet senaryolarından görselleri inceleyebilirsiniz:

<p align="center">
  <img src="giriş.png" width="48%" alt="Yatak Odası - Deprem Anı">
  <img src="GÖRSEL_YOLU_BURAYA/resim2.jpg" width="48%" alt="Banyo - Sel ve Şalter Kontrolü">
</p>
<p align="center">
  <img src="GÖRSEL_YOLU_BURAYA/resim3.jpg" width="48%" alt="Mutfak - Yangın Söndürme">
  <img src="GÖRSEL_YOLU_BURAYA/resim4.jpg" width="48%" alt="Bitiş Ekranı ve Skor Tablosu">
</p>
> **Not:** GitHub reponuzda `Images` veya `Gorseller` adında bir klasör açıp fotoğraflarınızı oraya yükledikten sonra, yukarıdaki `GÖRSEL_YOLU_BURAYA/resim.jpg` kısımlarını kendi dosya isimlerinizle değiştirmelisiniz.

---

## 🚀 Kurulum ve Çalıştırma
Projeyi kendi yerel ortamınızda çalıştırmak veya incelemek için:

1. Bu depoyu klonlayın:
   ```bash
   git clone [https://github.com/Ferhat-01/Unity_proje.git](https://github.com/Ferhat-01/Unity_proje.git)


   
