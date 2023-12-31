using Spotify.Model;
using Spotify.Service;

namespace Spotify.Page;

public partial class Parola : ContentPage
{
    private readonly ISarkiService sarkıService;

    public Parola()
    {
        InitializeComponent();

        sarkıService = new SarkiService();
    }
    
    private async void Degistir(object sender, EventArgs e)
    {
        string ad = Login.ad;
        string eskisifre = Login.parola;
        string sifre = SifreEntry.Text;
        if(eskisifre == sifre)
        {
            await DisplayAlert("Hata", "Yeni Şifreniz Eskisiyle Aynı Olamaz.","Tamam");
            return;
        }

        if (sifre.Length < 4)
        {
            await DisplayAlert("Hata", "Şifre en az 4 karakter olmalıdır.", "Tamam");
            return;
        }
        List<KullanıcıM> kullanıcılar = await sarkıService.GetKullanıcıMs();
        KullanıcıM kullanici = kullanıcılar.FirstOrDefault(k => k.Sifre == eskisifre && k.KullanıcıAdi == ad);

        if (kullanici != null)
        {
            kullanici.Sifre = sifre; 

            await sarkıService.Guncelle(kullanici);
            await DisplayAlert("Başarılı", "Şifre değiştirme işlemi başarıyla tamamlandı.", "Tamam");
            Login mainPage = new Login();
            await Navigation.PushModalAsync(new NavigationPage(mainPage) { BarBackgroundColor = Colors.Transparent, BarTextColor = Colors.White });
        }
    }

    private async void geriButton(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}