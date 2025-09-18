# Project: TD Prototype

Bu proje Unity kullanılarak geliştirilmiş bir **base defense / tower defense** tarzı oyundur. Oyuncu, dalgalar halinde gelen düşmanları karşılar. Düşmanlar önceden belirlenmiş bir yol boyunca ilerler ve oyuncunun üssüne ulaşmaya çalışır. Oyuncu üssünü korumak için otomatik saldırı yapan bir karaktere sahiptir.

## 🛠 Kullanılan Teknolojiler
- **Oyun Motoru**: Unity (2022.3.62f1 LTS)  
- **Programlama Dili**: C#
- **IDE**: Visual Studio
- **UI**: TextMeshPro, Unity UI (Slider, Panel vb.)  
- **Ses**: AudioSource, PlayClipAtPoint  

## 🎮 Özellikler
- Dalgalar halinde düşman spawn edilmesi
- Dalga sayısı arttıkça düşmanların **renk, hız ve can değerlerinin** değişmesi
- Düşmana atak yapılınca ses çıkması ve vfx, partikül efekti olması
- Düşman öldüğünde **“die”** sesi çalması
- Başlangıç menüsü olması ve müzik çalması **(kendim besteledim)**
- Oyun başladığında **arkaplan müziği** çalması
- Oyuncunun üssü zarar gördüğünde slider üzerinden can durumunun güncellenmesi
- `E` tuşuna basarak **bir sonraki dalgayı erken başlatabilme**
- Düşmanların **yol noktaları (waypoints)** üzerinden ilerlemesi

## ⚙️ Varsayımlar / Açıklamalar
- **Waypoints** nesnesi sahnede mevcut ve düşmanların ilerleyeceği yol noktalarını içeriyor.
- **Enemy prefab**’ı içinde:
  - `EnemyFollowPath` bileşeni (waypoint takibi)
  - `SimpleHealth` bileşeni (can sistemi)
  - `AudioSource` + “die” ve "attack" sesi atanmış durumda
- **Player / Base**:
  - `SimpleHealth` bileşeni üssün canını tutuyor.
  - UIManager’da bu can slider’a bağlanıyor.
- **WaveManager**:
  - Dalgaları yönetir, `enemyPrefab`, `spawnPoint` ve `pathWaypoints` atamaları Inspector’dan yapılmalıdır.
  - Dalga sayısına göre düşman özelliklerini (hız, can, renk) otomatik ayarlar.
- **Audio**:
  - Arkaplan müziği sahnede “AudioManager” veya boş bir GameObject’teki `AudioSource` üzerinden loop edilerek çalınır.
  - `E` tuşu bir sonraki dalgayı erken başlatır (WaveManager scripti üzerinden).
- **Scripts**
  - Tüm scriptler (UIManager, WaveManager, EnemyFollowPath, SimpleHealth, vb.) **Scripts** klasörü altında düzenlenmiştir.
