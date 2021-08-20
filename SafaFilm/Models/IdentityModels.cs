using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SafaFilm.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("FilmDB", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class MyContext : DbContext
    {
        public MyContext() : base("FilmDB") { }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer<MyContext>(null);
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<Blogs> blogs { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<Contactus> contactus { get; set; }
        public DbSet<Films> films { get; set; }
        public DbSet<Countrys> countrys { get; set; }
        public DbSet<Styles> styles { get; set; }
    }

    public class Contactus
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "نام و نام خانوادگی اجباری است")]
        [Display(Name = "نام و نام خانوادگی")]
        public string Names { get; set; }

        [Required(ErrorMessage = "ایمیل یا تلفن همراه اجباری است")]
        [Display(Name = "ایمیل یا تلفن همراه")]
        public string Emails { get; set; }

        [Required(ErrorMessage = "متن پیام اجباری است")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "پیام")]
        public string Messages { get; set; }
        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }
    }
    public class News
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "عنوان خبر اجباری است")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required(ErrorMessage = "اطلاعات خبر اجباری است")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیح خبر")]
        public string Body { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }
        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }
    }
    public class Blogs
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "عنوان بلاگ اجباری است")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required(ErrorMessage = "اطلاعات بلاگ اجباری است")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیح بلاگ")]
        public string Body { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }
        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }
    }
    public class Countrys
    {
        [Key]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "عنوان کشور اجباری است")]
        [Display(Name = "محصول کشور")]
        public string Title { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }

        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }

        public virtual List<Films> LFilms { get; set; }
    }
    public class Styles
    {
        [Key]
        public int StyleID { get; set; }

        [Required(ErrorMessage = "عنوان سبک اجباری است")]
        [Display(Name = "سبک")]
        public string Title { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }

        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }

        public virtual List<Films> LFilms { get; set; }
    }
    public class Films
    {
        [Key]
        public int FilmID { get; set; }

        [Display(Name = "عکس")]
        public string imgpic { get; set; }

        [Required(ErrorMessage = "عنوان فیلم اجباری است")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required(ErrorMessage = "متن فیلم اجباری است")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "متن فیلم")]
        public string Body { get; set; }

        [Required(ErrorMessage = "تگ فیلم اجباری است")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "تگ فیلم")]
        public string Tags { get; set; }

        [Required(ErrorMessage = "کارگردان فیلم اجباری است")]
        [Display(Name = "کارگردان فیلم")]
        public string Director { get; set; }

        [Required(ErrorMessage = "تهیه کننده فیلم اجباری است")]
        [Display(Name = "تهیه کننده فیلم")]
        public string Producer { get; set; }

        [Required(ErrorMessage = "بازیگران فیلم اجباری است")]
        [Display(Name = "بازیگران فیلم")]
        public string Actress { get; set; }

        [Display(Name = "لینک 480")]
        public string L480 { get; set; }

        [Display(Name = "قیمت 480")]
        public string P480 { get; set; }

        [Display(Name = "لینک 720")]
        public string L720 { get; set; }

        [Display(Name = "قیمت 720")]
        public string P720 { get; set; }

        [Display(Name = "لینک 1080")]
        public string L1080 { get; set; }

        [Display(Name = "قیمت 1080")]
        public string P1080 { get; set; }

        [Display(Name = "لینک Q1080")]
        public string LQ1080 { get; set; }

        [Display(Name = "قیمت Q1080")]
        public string PQ1080 { get; set; }

        [Display(Name = "لینک همه کیفیت ها")]
        public string LALL { get; set; }

        [Display(Name = "قیمت همه کیفیت ها")]
        public string PALL { get; set; }

        [Display(Name = "زیر نویس")]
        public string SubTitle { get; set; }

        [Display(Name = "دوبله فارسی")]
        public bool Translate { get; set; }

        [Display(Name = "سبک")]
        public int StyleID { get; set; }
        public virtual Styles styles { get; set; }

        [Display(Name = "کشور")]
        public int CountryID { get; set; }
        public virtual Countrys countrys { get; set; }

        [Display(Name = "فعال")]
        public bool Status { get; set; }

        public string dt { get; set; }
        public string tm { get; set; }
        public string users { get; set; }
    }

}

