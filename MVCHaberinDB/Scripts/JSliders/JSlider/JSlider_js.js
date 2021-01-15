$(function () {
    var sure = 5000; //Slider Dönüş süresi
    var sliderHizi = 500;
    var toplamLi = $(".JSlider ul li").length; //Toplam kaç resim var?
    var liWidth = 990; //Tek bir li'nin (Resmin) boyutunu (Slider'da kullanılacak)
    var toplamWidth = liWidth * toplamLi; //Bir resmin boyutunu vererek tüm resimler yan yana geldiğinde kaç boyutunda olacak
    var liDeger = 0; //Li nin değeri

    $(".JSlider ul").css("width", toplamWidth + "px"); //JSlider classının içindeki ul nin genişliği tüm li lerin toplam genişliğine eşit olacak

    //Sonraki butonuna basıldığında işleyecek kodlar
    $("a#sonraki").click(function () { //sonraki id'sine sahip a etiketine tıklandığında ne yapacağı
        clearInterval(don); //Timer sıfırlama
        don = setInterval($.Slider, sure); //Timer'a yeni değer ata
        if (liDeger < toplamLi - 1) { //liDeger küçükse ToplamLi sayısından
            liDeger++; //liDeger'i bir bir arttırıyoruz.
            yeniWidth = liWidth * liDeger; //li nin boyutunu * lideger ile çarparak yeni genişliği belirliyoruz.
            $(".JSlider ul").animate({ marginLeft: "-" + yeniWidth + "px" }, sliderHizi); //Daha sonra bu değeri animate fonksiyonu ile marginLeft olarak veriyoruz.
        }
        else {//Eğer Son resimdeyse ilk resme dönsün diye,
            liDeger = 0; //liDegeri Sıfırlıyoruz.
            $(".JSlider ul").animate({ marginLeft: "0" }, sliderHizi); //İlk resime dönmesi için MarginLefti sıfırlıyoruz.
        }
        return false; //Link kısmına # işareti çıkartmasın diye return false ediyoruz. (a da ki #)
    })

    $("a#onceki").click(function () {//Önceki butonuna basıldığında işleyecek kodlar
        clearInterval(don); //Timer sıfırlama
        don = setInterval($.Slider, sure); //Timer'a yeni değer ata
        if (liDeger > 0) { //liDeger 0 dan büyükse, yani ilk resimde değilse,
            liDeger--; //liDeger'i bir azalt
            yeniWidth = liWidth * liDeger; //Yeni genişliği hesapla
            $(".JSlider ul").animate({ marginLeft: "-" + yeniWidth + "px" }, sliderHizi); //Ve slider'ın konumunu ayarla
        }
        else { //Eğer 1. resimdeysek bastığımızda son resme dönsün
            liDeger = toplamLi; //liDeger'e, toplamLi sayısını at..
            $(".JSlider ul").animate({ marginRight: "0" }, sliderHizi);//Animasyon ile slider değiştir.
        }
        return false; // # İşareti olmasın diye return et
    })

    /* Timer */
    $.Slider = function(){
        if (liDeger < toplamLi - 1) {
            liDeger++;
            yeniWidth = liWidth * liDeger;
            $(".JSlider ul").animate({ marginLeft: "-" + yeniWidth + "px" }, sliderHizi);
        } else {
            liDeger = 0;
            $(".JSlider ul").animate({ marginLeft: "0" }, sliderHizi);
        }
    }

    var don = setInterval("$.Slider()", sure);
}); 