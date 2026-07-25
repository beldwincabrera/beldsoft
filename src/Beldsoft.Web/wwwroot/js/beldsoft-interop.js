(function () {
    "use strict";

    const initializedBackToTop = new WeakSet();

    function directChild(element, selector) {
        return Array.from(element.children).find(child => child.matches(selector));
    }

    function prepareMobileMenus() {
        document.querySelectorAll(".main-header").forEach(header => {
            const source = header.querySelector(".main-menu .navigation");
            const target = header.querySelector(".mobile-menu .menu-outer");
            if (!source || !target) return;

            target.replaceChildren(source.cloneNode(true));
            target.querySelectorAll("li.dropdown").forEach(item => {
                const button = document.createElement("button");
                button.type = "button";
                button.className = "dropdown-btn";
                button.setAttribute("aria-label", "Toggle submenu");
                button.setAttribute("aria-expanded", "false");
                button.innerHTML = '<span class="fa fa-angle-down" aria-hidden="true"></span>';
                item.appendChild(button);
            });
        });
    }

    function updateActiveNavigation() {
        const currentPath = window.location.pathname.replace(/\/+$/, "") || "/";
        document.querySelectorAll(".main-header .navigation a[href]").forEach(link => {
            const linkPath = new URL(link.href, window.location.origin).pathname.replace(/\/+$/, "") || "/";
            const isActive = linkPath === "/"
                ? currentPath === "/"
                : currentPath === linkPath || currentPath.startsWith(`${linkPath}/`);
            link.classList.toggle("active", isActive);
            if (isActive) {
                link.setAttribute("aria-current", "page");
            } else {
                link.removeAttribute("aria-current");
            }
        });
    }

    function updateHeader() {
        document.querySelectorAll(".main-header").forEach(header => {
            header.classList.toggle("fixed-header", window.scrollY >= header.offsetHeight);
        });
    }

    function initializeBackToTop() {
        const control = document.querySelector(".progress-wrap");
        const path = control?.querySelector("path");
        if (!control || !path || initializedBackToTop.has(control)) return;

        initializedBackToTop.add(control);
        const pathLength = path.getTotalLength();
        path.style.strokeDasharray = `${pathLength} ${pathLength}`;

        const update = function () {
            const scrollable = document.documentElement.scrollHeight - window.innerHeight;
            const progress = scrollable > 0 ? window.scrollY / scrollable : 0;
            path.style.strokeDashoffset = String(pathLength * (1 - progress));
            control.classList.toggle("active-progress", window.scrollY > 50);
        };

        control.addEventListener("click", event => {
            event.preventDefault();
            window.scrollTo({ top: 0, behavior: "smooth" });
        });
        window.addEventListener("scroll", update, { passive: true });
        update();
    }

    function closeMobileMenu() {
        document.body.classList.remove("mobile-menu-visible");
        document.querySelectorAll(".mobile-nav-toggler").forEach(button =>
            button.setAttribute("aria-expanded", "false"));
        document.querySelectorAll(".mobile-menu li.open").forEach(item => {
            item.classList.remove("open");
            const submenu = directChild(item, "ul, .mega-menu");
            if (submenu) submenu.style.display = "none";
        });
        document.querySelectorAll(".mobile-menu .dropdown-btn[aria-expanded='true']")
            .forEach(button => button.setAttribute("aria-expanded", "false"));
    }

    function handleDocumentClick(event) {
        const mobileToggle = event.target.closest(".mobile-nav-toggler");
        if (mobileToggle) {
            document.body.classList.add("mobile-menu-visible");
            mobileToggle.setAttribute("aria-expanded", "true");
            return;
        }

        if (event.target.closest(".mobile-menu .menu-backdrop, .mobile-menu .close-btn")) {
            closeMobileMenu();
            return;
        }

        const dropdownButton = event.target.closest(".mobile-menu .dropdown-btn");
        if (dropdownButton) {
            event.preventDefault();
            const item = dropdownButton.closest("li.dropdown");
            const willOpen = !item.classList.contains("open");
            item.parentElement.querySelectorAll(":scope > li.open").forEach(sibling => {
                if (sibling === item) return;
                sibling.classList.remove("open");
                const siblingMenu = directChild(sibling, "ul, .mega-menu");
                if (siblingMenu) siblingMenu.style.display = "none";
            });
            item.classList.toggle("open", willOpen);
            const submenu = directChild(item, "ul, .mega-menu");
            if (submenu) submenu.style.display = willOpen ? "block" : "none";
            dropdownButton.setAttribute("aria-expanded", String(willOpen));
            return;
        }

    }

    function initializePage() {
        prepareMobileMenus();
        updateActiveNavigation();
        initializeBackToTop();
        updateHeader();
    }

    document.addEventListener("click", handleDocumentClick);
    document.addEventListener("keydown", event => {
        if (event.key !== "Escape") return;
        closeMobileMenu();
    });
    window.addEventListener("scroll", updateHeader, { passive: true });
    document.addEventListener("DOMContentLoaded", initializePage);
    document.addEventListener("enhancedload", initializePage);

    window.beldsoftInterop = {
        toggleClass: function (selector, className) {
            document.querySelector(selector)?.classList.toggle(className);
        },
        addClass: function (selector, className) {
            document.querySelector(selector)?.classList.add(className);
        },
        removeClass: function (selector, className) {
            document.querySelector(selector)?.classList.remove(className);
        },
        scrollToTop: function () {
            window.scrollTo({ top: 0, behavior: "smooth" });
        },
        scrollToElement: function (id) {
            document.getElementById(id)?.scrollIntoView({ behavior: "smooth" });
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
})();
