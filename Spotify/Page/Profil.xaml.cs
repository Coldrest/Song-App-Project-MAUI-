using Spotify.Model;
using Spotify.Service;

namespace Spotify.Page;

public partial class Profil : ContentPage
{
    private readonly ISarkiService sarkıService;
    public Profil()
    {
        InitializeComponent();
        sarkıService = new SarkiService();
        string ad = Login.ad;
        string sifre = Login.parola;
        kullanıcıAdi.Text = ad;
        Sifre.Text = sifre;
        LoadBilgi();


    }
    private async void LoadBilgi()
    {


        List<KullanıcıM> kullanıcılar = await sarkıService.GetKullanıcıMs();

        
        KullanıcıM kullanici = kullanıcılar.FirstOrDefault(k => k.KullanıcıAdi == kullanıcıAdi.Text && k.Sifre == Sifre.Text);

        if (kullanici != null)
        {
            
            string dogum = kullanici.DogumTarihi.ToString("dd/MM/yyyy");
            string cins = kullanici.Cinsiyet;
            
            dogumTarihi.Text = dogum;
            cinsiyet.Text = cins;
        }

    }
    private async void ParolaDegistir(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Page.Parola());

    }
    private async void HesapSil(object sender, EventArgs e)
    {
        List<KullanıcıM> kullanıcılar = await sarkıService.GetKullanıcıMs();

        KullanıcıM kullanici = kullanıcılar.FirstOrDefault(k => k.KullanıcıAdi == kullanıcıAdi.Text && k.Sifre == Sifre.Text);

        if (kullanici != null)
        {
            bool confirmed = await DisplayAlert("Onay", "Hesabınız Silinsin Mi?", "Evet", "Hay?r");

            if (confirmed)
            {
                await sarkıService.Sil(kullanici.Id);
                Login mainPage = new Login();
                await Navigation.PushModalAsync(new NavigationPage(mainPage) { BarBackgroundColor = Colors.Transparent, BarTextColor = Colors.White });
            }
        }
    }

    private async void geriButton(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}