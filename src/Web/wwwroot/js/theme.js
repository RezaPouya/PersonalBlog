// این فایل قالب روشن/تیره سایت را مدیریت می‌کند.
//
// نکته‌ی مهم (چرا این فایل جدا شد): وقتی بین صفحات سایت حرکت می‌کنید، Blazor Web App
// به‌جای رفرش کامل مرورگر از «Enhanced Navigation» استفاده می‌کند (سریع‌تر است، ولی
// اسکریپت‌های داخل <head> فقط در بارگذاری اول صفحه اجرا می‌شوند، نه در پیمایش‌های بعدی).
// قبلاً کلاس dark فقط در همان اسکریپت اولیه‌ی <head> اضافه می‌شد، پس بعد از اولین
// پیمایش، دیگر اعمال نمی‌شد و سایت به‌اشتباه روشن نشان داده می‌شد.
// راه‌حل: تابع اعمال قالب را اینجا expose می‌کنیم و در پایین App.razor، بعد از لود
// blazor.web.js، با Blazor.addEventListener('enhancedload', ...) بعد از هر پیمایش
// دوباره صدا می‌زنیم.
(function () {
    function applyTheme() {
        var saved = localStorage.getItem('theme');
        if (saved === 'light') {
            document.documentElement.classList.remove('dark');
        } else {
            // پیش‌فرض سایت قالب تیره است.
            document.documentElement.classList.add('dark');
        }
    }

    function toggleTheme() {
        var isDark = !document.documentElement.classList.contains('dark');
        localStorage.setItem('theme', isDark ? 'dark' : 'light');
        applyTheme();
        return isDark;
    }

    function isDarkMode() {
        return document.documentElement.classList.contains('dark');
    }

    // اعمال فوری برای جلوگیری از چشمک قالب روشن در بارگذاری اول
    applyTheme();

    window.siteTheme = {
        toggle: toggleTheme,
        isDark: isDarkMode,
        apply: applyTheme
    };
})();
