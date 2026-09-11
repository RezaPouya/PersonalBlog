# وبلاگ شخصی — Blazor Server (.NET 10) | Clean Architecture

این پروژه بر اساس معماری لایه‌ای **Domain → AppServices → Infrastructure → Web**
(هم‌ساختار با پروژه‌ی نمونه‌ی شما، `Iau.Bazaar`) ساخته شده، با این تفاوت که چون
فقط یک ادمین سایت وجود دارد، لایه‌ی کاربری بسیار ساده‌تر شده است.

> ⚠️ این کدها بدون build/run واقعی (بدون دسترسی به .NET SDK / NuGet در محیط تولید
> این پاسخ) نوشته شده‌اند. قبل از اجرا، حتماً یک بار `dotnet build` روی کل سولوشن
> بگیرید تا خطاهای احتمالی کامپایل (اسم پکیج/فضای‌نام) رفع شود.

## ساختار پروژه

```
PersonalBlog.sln
src/
├── PersonalBlog.Domain           موجودیت‌ها، اینترفیس‌های پایه (بدون هیچ وابستگی خارجی)
├── PersonalBlog.AppServices      DTOها، سرویس‌ها، Validatorها، ICurrentAppUser
├── PersonalBlog.Infrastructure   AppDbContext، RepositoryBase، LocalCacheManager، Seed
├── PersonalBlog.Web              Blazor Server (صفحات عمومی + پنل مدیریت)
└── PersonalBlog.DbMigrator       پروژه‌ی کنسول برای اجرای Migration + Seed
```

## پیش‌نیازها

- .NET 10 SDK
- SQL Server (LocalDB، Express یا نسخه‌ی کامل)

## راه‌اندازی

### ۱. تنظیم Connection String

در `src/PersonalBlog.Web/appsettings.json` و `src/PersonalBlog.DbMigrator/appsettings.json`
مقدار `ConnectionStrings:DatabaseConnection` را با SQL Server خودتان تطبیق دهید.

### ۲. ساخت Migration اولیه

چون در این پاسخ امکان اجرای `dotnet ef` وجود نداشت، خودتان یک بار این دستور را
از ریشه‌ی سولوشن اجرا کنید:

```bash
dotnet tool install --global dotnet-ef   # اگر نصب نیست
cd src/PersonalBlog.Infrastructure
dotnet ef migrations add Initial --startproject ../PersonalBlog.DbMigrator
```

### ۳. اجرای Migration + Seed

```bash
dotnet run --project src/PersonalBlog.DbMigrator
```

این کار جداول را می‌سازد، نقش و کاربر Admin را (از بخش `AdminSeed` در
`appsettings.json`، پیش‌فرض: `admin@example.com` / `Admin@123456`) seed می‌کند
و یک دسته‌بندی/دوره/پست نمونه اضافه می‌کند.

**حتماً بعد از اولین اجرا، ایمیل و پسورد ادمین را در `appsettings.json` عوض کنید
یا از User Secrets استفاده کنید.**

### ۴. اجرای وب‌سایت

```bash
dotnet run --project src/PersonalBlog.Web
```

سپس:
- سایت عمومی: `https://localhost:xxxx/`
- ورود ادمین: `https://localhost:xxxx/admin/login`
- پنل مدیریت: `https://localhost:xxxx/admin`

## تنظیمات مهم در appsettings.json

| بخش | توضیح |
|---|---|
| `RecaptchaSettings` | کلیدهای reCAPTCHA v2/v3 را از [google.com/recaptcha/admin](https://www.google.com/recaptcha/admin) بگیرید و `SiteKey`/`SecretKey` را پر کنید. برای نمایش واقعی ویجت کپچا، باید اسکریپت `https://www.google.com/recaptcha/api.js` را هم به `App.razor` اضافه کنید. |
| `EmailSettings` | برای ارسال ایمیل خبرنامه و اعلان تماس با ما (SMTP). |
| `SocialLinks` | لینک گیت‌هاب/لینکدین که در فوتر و صفحه‌ی "درباره‌ی من" نمایش داده می‌شود. |
| `AdminSeed` | ایمیل/پسورد کاربر ادمین اولیه. |

## تصمیمات معماری کلیدی (طبق درخواست شما)

- **کش**: `ILocalCacheManager` در Domain تعریف و با `LocalCacheManager`
  (بر پایه‌ی `IMemoryCache`) در Infrastructure پیاده‌سازی شده — دقیقاً همان
  قرارداد نمونه‌ی شما، بدون Redis/Hybrid Cache.
- **ثبت خطا**: بدون سرویس Monitoring جداگانه؛ یک میان‌افزار سراسری در
  `Program.cs` هر خطای مدیریت‌نشده را در جدول `AppExceptionLogs` همان
  دیتابیس اصلی ثبت می‌کند (`IExceptionLogService`).
- **احراز هویت**: ASP.NET Core Identity کامل، اما بدون ثبت‌نام عمومی — فقط
  یک نقش (`Admin`) که با Seed ساخته می‌شود. ورود/خروج با Minimal API endpoint
  (`POST /admin/login`, `POST /admin/logout`) پیاده‌سازی شده چون
  `SignInAsync`/`SignOutAsync` باید قبل از commit شدن پاسخ اجرا شوند و از
  داخل کامپوننت تعاملی Blazor Server مستقیماً قابل انجام نیستند.
- **کامنت‌گذاری**: بازدیدکننده‌ها بدون نیاز به اکانت، با نام/ایمیل/کپچا
  کامنت می‌گذارند؛ همه‌ی کامنت‌ها پیش از نمایش باید از پنل ادمین تأیید شوند.
- **DbMigrator**: پروژه‌ی کنسول مجزا با `DbMigratorHostedService`، دقیقاً
  هم‌الگو با پروژه‌ی نمونه‌ی شما.

## چیزهایی که باید خودتان تکمیل کنید

1. **اجرای واقعی Migration** (بخش ۲ بالا) — چون محیطی که این کد نوشته شد به
   .NET SDK/NuGet دسترسی نداشت.
2. **ویجت واقعی reCAPTCHA**: اسکریپت گوگل را در `App.razor` اضافه کنید و
   بعد از تعامل کاربر، مقدار توکن را (با کمی جاوااسکریپت interop) به
   `RecaptchaToken` در فرم‌های کامنت/تماس بفرستید. فعلاً فقط `div.g-recaptcha`
   رندر شده است.
3. **ویرایشگر Rich Text واقعی** برای پست‌ها (فعلاً `textarea` ساده برای HTML
   خام است) — می‌توانید TinyMCE/Quill را به‌صورت یک کامپوننت JS Interop اضافه
   کنید.
4. **آپلود تصویر**: فعلاً فیلدهای تصویر (کاور پست/دوره/پروژه) فقط URL می‌گیرند؛
   اگر آپلود مستقیم فایل نیاز است، یک `IFileUploadService` ساده اضافه کنید.
5. صفحات ویرایش برای `Categories`, `Tags`, `Courses`, `Projects` فعلاً فقط
   افزودن/حذف دارند (بدون Edit)؛ الگوی `Posts/Edit.razor` را برای اضافه کردن
   ویرایش به آن‌ها هم می‌توانید کپی کنید.

## دستور اجرای سریع (خلاصه)

```bash
dotnet run --project src/PersonalBlog.DbMigrator   # یک‌بار، برای ساخت DB
dotnet run --project src/PersonalBlog.Web           # اجرای سایت
```
