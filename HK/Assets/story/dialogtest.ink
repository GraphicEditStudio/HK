Karagöz Hacivat Dialog Sistem
VAR total=1

Ne tarafa gitmeliyiz
->Tercihler
== Tercihler==
* Yemek Odası Tercih ->Yemek
* Koridor Tercih->Koridor
* Salon Tercih->Salon

== Yemek==
Yemek Odasına Giidelim
~ total = total +1

~ gorev1 = true
->TercihSonrasi


== Koridor ==
Koridora gidelim
~ gorev2 = true
->TercihSonrasi


== Salon==
Salona Gidelim
~ gorev3 = true
->TercihSonrasi


VAR gorev1 = false
VAR gorev2 = false
VAR gorev3 = false
VAR gameOver = false




== TercihSonrasi ==
Genel Konusma Dialogu
* Burdan kaçabilirmiyiz ->Hikaye2
* Oyunu bitir ->Görevler

== Hikaye2 ==
part2 devam edecektir
->END

== GameFinish ==
~gameOver=true
->END

== Görevler ==
{gorev1: Yemek Odasına gittin|Yemek odasına git}
{gorev2: Koridora Odasına gitin|Koridora odasına git}
{gorev3: Salon Odasına gittin|Salon odasına git}

->GameFinish