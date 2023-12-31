using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Veri.Models;
using WebApplication1.EF;
using WebApplication1.Models;

namespace Veri
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.MapGet("/şarkılar", () =>
            {
                MusicContext context = new MusicContext();
                return context.Sarkı.ToList();
            });
            app.MapGet("/kullanıcılar", () =>
            {
                MusicContext context1 = new MusicContext();
                return context1.Person.ToList();
            });
            app.MapDelete("/kullanıcılar/{id}", (string id) =>
            {
                MusicContext context = new MusicContext();
                var silinecek = context.Person.FirstOrDefault(x => x.Id == Guid.Parse(id));
                if (silinecek != null)
                {
                    context.Person.Remove(silinecek);
                    context.SaveChanges();
                }
            });
            app.MapPost("/kullanıcılar", (Kullanıcılar kullanıcılar) =>
            {
                MusicContext context = new MusicContext();
                context.Person.Add(kullanıcılar);
                context.SaveChanges();
            });

            app.MapPost("/kullanıcılar/guncelle", (Kullanıcılar kullanıcılar) =>
            {
                MusicContext context = new MusicContext();
                var guncellenecek = context.Person.Find(kullanıcılar.Id);
                guncellenecek.Sifre = kullanıcılar.Sifre;
                context.SaveChanges();
            });
            app.Run();
        }
    }
}