// راه‌انداز ادیتور HTML5 (Quill.js) با پشتیبانی از هایلایت کد از طریق highlight.js
// هر دو کتابخانه رایگان و متن‌باز هستند و از طریق CDN در App.razor بارگذاری می‌شوند.
window.htmlEditorInterop = (function () {
    const instances = {};

    // دکمه‌های تولباری که هر مطلب وبلاگ معمولاً به آن نیاز دارد.
    const toolbarOptions = [
        [{ header: [2, 3, 4, false] }],
        ['bold', 'italic', 'underline', 'strike'],
        [{ list: 'ordered' }, { list: 'bullet' }],
        [{ align: [] }],
        [{ direction: 'rtl' }],
        ['blockquote', 'link', 'image'],
        ['code-block'],
        ['clean']
    ];

    function init(elementId, dotNetRef, initialContent) {
        if (typeof Quill === 'undefined') {
            console.error('Quill بارگذاری نشده است؛ لینک CDN را در App.razor بررسی کنید.');
            return;
        }

        const quill = new Quill('#' + elementId, {
            theme: 'snow',
            placeholder: 'محتوای مطلب را اینجا بنویسید... (برای کد از دکمه </> استفاده کنید)',
            modules: {
                toolbar: toolbarOptions,
                // ماژول syntax داخلی Quill به‌صورت خودکار از highlight.js برای
                // رنگی‌کردن بلاک‌های کد استفاده می‌کند (سی‌شارپ، جاوااسکریپت، پایتون و ...
                // به‌صورت خودکار تشخیص داده می‌شوند، چون از باندل استاندارد hljs استفاده شده).
                syntax: typeof hljs !== 'undefined'
            }
        });

        if (initialContent) {
            quill.clipboard.dangerouslyPasteHTML(initialContent);
        }

        let debounceTimer = null;
        quill.on('text-change', function () {
            clearTimeout(debounceTimer);
            debounceTimer = setTimeout(function () {
                const html = quill.root.innerHTML;
                dotNetRef.invokeMethodAsync('OnContentChanged', html);
            }, 300); // debounce تا با هر ضربه‌کلید درخواست SignalR جدا نرود
        });

        instances[elementId] = quill;
    }

    function getContent(elementId) {
        const quill = instances[elementId];
        return quill ? quill.root.innerHTML : '';
    }

    function setContent(elementId, html) {
        const quill = instances[elementId];
        if (quill) {
            quill.setText('');
            quill.clipboard.dangerouslyPasteHTML(html || '');
        }
    }

    function destroy(elementId) {
        delete instances[elementId];
    }

    return { init, getContent, setContent, destroy };
})();
