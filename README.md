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

## 🚀 Kurulum ve Çalıştırma
Projeyi kendi yerel ortamınızda çalıştırmak veya incelemek için:

1. Bu depoyu klonlayın:
   ```bash
   git clone [https://github.com/kullanici-adi/proje-adi.git](https://github.com/kullanici-adi/proje-adi.git)
