window.beldsoftInterop = {

    initInstagramCarousel: function () {
        try {
            if (typeof Swiper === 'undefined') return;
            var el = document.querySelector('.instagram-one_carousel');
            if (!el) return;
            if (el.swiper) { el.swiper.destroy(true, true); }
            new Swiper('.instagram-one_carousel', {
                slidesPerView: 5,
                spaceBetween: 0,
                loop: true,
                autoplay: { enabled: true, delay: 6000 },
                navigation: {
                    nextEl: '.instagram-one_next-arrow',
                    prevEl: '.instagram-one_prev-arrow',
                    clickable: true
                },
                pagination: { el: '.instagram-one_carousel-pagination', clickable: true },
                speed: 500,
                breakpoints: {
                    1600: { slidesPerView: 5 },
                    1200: { slidesPerView: 5 },
                    992:  { slidesPerView: 4 },
                    768:  { slidesPerView: 4 },
                    576:  { slidesPerView: 3 },
                    0:    { slidesPerView: 2 }
                }
            });
        } catch (e) { console.warn('Instagram carousel init failed:', e); }
    },

    initFooterTypeIt: function () {
        try {
            if (typeof jQuery === 'undefined') return;
            var el = jQuery('.footer-type_title.variable-text');
            if (!el.length) return;
            // destroy previous instance if typeIt was already applied
            if (el.data('typeit')) { try { el.data('typeit').destroy(); } catch (_) {} }
            el.typeIt({
                strings: ['Beldsoft'],
                speed: 450,
                breakLines: true,
                loop: true,
                autoStart: true
            });
        } catch (e) { console.warn('Footer typeIt init failed:', e); }
    },

    initMainSlider: function () {
        try {
            if (typeof Swiper === 'undefined') return;
            var el = document.querySelector('.main-slider');
            if (!el) return;
            // Destroy existing instance if present
            if (el.swiper) { el.swiper.destroy(true, true); }
            new Swiper('.main-slider', {
                slidesPerView: 1,
                spaceBetween: 0,
                loop: true,
                autoplay: { enabled: true, delay: 6000 },
                navigation: { nextEl: '.main-slider-next', prevEl: '.main-slider-prev' },
                pagination: {
                    el: '.slider-one_pagination',
                    clickable: true,
                    renderBullet: function (index, className) {
                        return '<span class="' + className + '">' + String(index + 1).padStart(2, '0') + '</span>';
                    }
                },
                speed: 500
            });
        } catch (e) { console.warn('Main slider init failed:', e); }
    },

    initSwiper: function (containerSelector, options) {
        try {
            if (typeof Swiper !== 'undefined') {
                new Swiper(containerSelector, options);
            }
        } catch (e) { console.warn('Swiper init failed:', e); }
    },

    initTestimonialsSwiper: function () {
        try {
            if (typeof Swiper !== 'undefined') {
                new Swiper('#testimonials-slider', {
                    loop: true,
                    slidesPerView: 1,
                    spaceBetween: 30,
                    autoplay: { delay: 4000, disableOnInteraction: false },
                    pagination: { el: '.swiper-pagination', clickable: true },
                    breakpoints: {
                        768: { slidesPerView: 2 },
                        1200: { slidesPerView: 3 }
                    }
                });
            }
        } catch (e) { console.warn('Testimonials swiper init failed:', e); }
    },

    initCursor: function () {
        const cursor = document.querySelector('.cursor');
        const follower = document.querySelector('.cursor-follower');
        if (!cursor || !follower) return;
        document.addEventListener('mousemove', function (e) {
            cursor.style.left = e.clientX + 'px';
            cursor.style.top = e.clientY + 'px';
            setTimeout(function () {
                follower.style.left = e.clientX + 'px';
                follower.style.top = e.clientY + 'px';
            }, 100);
        });
    },

    dismissPreloader: function () {
        const el = document.querySelector('.preloader');
        if (el) {
            setTimeout(() => { el.style.opacity = '0'; setTimeout(() => el.remove(), 600); }, 800);
        }
    },

    observeCounters: function () {
        const counters = document.querySelectorAll('[data-count-target]');
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const el = entry.target;
                    const target = parseInt(el.getAttribute('data-count-target'), 10);
                    const suffix = el.getAttribute('data-count-suffix') || '';
                    let current = 0;
                    const step = Math.ceil(target / 80);
                    const timer = setInterval(() => {
                        current += step;
                        if (current >= target) { current = target; clearInterval(timer); }
                        el.textContent = current.toLocaleString() + suffix;
                    }, 20);
                    observer.unobserve(el);
                }
            });
        }, { threshold: 0.3 });
        counters.forEach(el => observer.observe(el));
    },

    initInViewAnimations: function () {
        const els = document.querySelectorAll('.wow');
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.classList.add('animated');
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1 });
        els.forEach(el => observer.observe(el));
    },

    toggleClass: function (selector, className) {
        const el = document.querySelector(selector);
        if (el) el.classList.toggle(className);
    },

    addClass: function (selector, className) {
        const el = document.querySelector(selector);
        if (el) el.classList.add(className);
    },

    removeClass: function (selector, className) {
        const el = document.querySelector(selector);
        if (el) el.classList.remove(className);
    },

    scrollToTop: function () {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    },

    scrollToElement: function (id) {
        const el = document.getElementById(id);
        if (el) el.scrollIntoView({ behavior: 'smooth' });
    },

    getScrollY: function () {
        return window.scrollY;
    },

    setLocalStorage: function (key, value) {
        localStorage.setItem(key, value);
    },

    getLocalStorage: function (key) {
        return localStorage.getItem(key);
    }
};
