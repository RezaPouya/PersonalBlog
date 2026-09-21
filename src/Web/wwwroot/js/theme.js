// این فایل قالب روشن/تیره سایت را مدیریت می‌کند — کاملاً با جاوااسکریپت خالص،
// بدون هیچ وابستگی به Blazor یا اتصال SignalR.
//
// چرا این‌طور طراحی شد: دکمه‌ی تغییر قالب قبلاً از @onclick بلیزور استفاده می‌کرد که
// یعنی هر کلیک باید یک round-trip به سرور (روی همون کانکشن SignalR) می‌زد. اگر آن
// اتصال به هر دلیلی برقرار نشده باشد یا کند باشد، دکمه از دید کاربر «کار نمی‌کند».
// چون این یک تعامل کاملاً بصری/کلاینتی است (نیازی به منطق سمت سرور ندارد)، الان
// کاملاً با onclick ساده‌ی HTML + جاوااسکریپت انجام می‌شود؛ یعنی حتی اگر اتصال
// تعاملی بلیزور اصلاً برقرار نشود، باز هم کار می‌کند.
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

    // اعمال فوری برای جلوگیری از چشمک قالب روشن در بارگذاری اول
    applyTheme();

    window.siteTheme = {
        toggle: toggleTheme,
        isDark: function () { return document.documentElement.classList.contains('dark'); },
        apply: applyTheme
    };
})();
