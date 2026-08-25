/**
 * Barrameru Global Layout & UI Interactions
 */

document.addEventListener('DOMContentLoaded', () => {
    // Newsletter Submit Event
    // document.querySelectorAll('.newsletter-form').forEach(form => {
    //     form.addEventListener('submit', (e) => {
    //         e.preventDefault();
    //         const emailInput = form.querySelector('input[type="email"]');
    //         if (emailInput && emailInput.value) {
    //             if (typeof Swal !== 'undefined') {
    //                 Swal.fire({
    //                     icon: 'success',
    //                     title: 'Thank you for subscribing!',
    //                     text: `We've sent a confirmation to ${emailInput.value}`,
    //                     confirmButtonColor: '#B67961'
    //                 });
    //             }
    //             emailInput.value = '';
    //         }
    //     });
    // });

    // Sticky Header Scroll Shadow
    const header = document.querySelector('.header-barrameru');
    window.addEventListener('scroll', () => {
        if (window.scrollY > 40) {
            header.classList.add('shadow-sm');
        } else {
            header.classList.remove('shadow-sm');
        }
    });

    // Desktop & Mobile Search Controller (Desktop: Inline Expandable | Mobile: Under-Navbar Slide-Down)
    const searchTrigger = document.getElementById('headerSearchTrigger');
    const searchContainer = document.getElementById('headerSearchContainer');
    const searchUnderBar = document.getElementById('headerSearchUnderBar');
    
    // Desktop elements
    const searchInputDesktop = document.getElementById('headerSearchInputDesktop');
    const searchCloseDesktop = document.getElementById('headerSearchCloseDesktop');

    // Mobile elements
    const searchInputMobile = document.getElementById('headerSearchInputMobile');
    const searchClearMobile = document.getElementById('headerSearchClearMobile');
    const searchCloseMobile = document.getElementById('headerSearchCloseMobile');

    function isMobileView() {
        return window.innerWidth < 768;
    }

    function openSearch() {
        if (isMobileView()) {
            searchUnderBar?.classList.add('is-open');
            searchTrigger?.classList.add('active');
            setTimeout(() => searchInputMobile?.focus(), 120);
        } else {
            searchContainer?.classList.add('is-open');
            setTimeout(() => searchInputDesktop?.focus(), 150);
        }
    }

    function closeSearch() {
        searchContainer?.classList.remove('is-open');
        searchUnderBar?.classList.remove('is-open');
        searchTrigger?.classList.remove('active');
    }

    if (searchTrigger) {
        searchTrigger.addEventListener('click', (e) => {
            e.stopPropagation();
            if (isMobileView()) {
                if (searchUnderBar?.classList.contains('is-open')) {
                    closeSearch();
                } else {
                    openSearch();
                }
            } else {
                if (searchContainer?.classList.contains('is-open')) {
                    closeSearch();
                } else {
                    openSearch();
                }
            }
        });
    }

    // Close buttons
    searchCloseDesktop?.addEventListener('click', (e) => {
        e.stopPropagation();
        closeSearch();
    });

    searchCloseMobile?.addEventListener('click', (e) => {
        e.stopPropagation();
        closeSearch();
    });

    // Mobile clear input (X) button
    if (searchInputMobile && searchClearMobile) {
        searchInputMobile.addEventListener('input', () => {
            if (searchInputMobile.value.trim().length > 0) {
                searchClearMobile.style.display = 'inline-flex';
            } else {
                searchClearMobile.style.display = 'none';
            }
        });

        searchClearMobile.addEventListener('click', (e) => {
            e.stopPropagation();
            searchInputMobile.value = '';
            searchClearMobile.style.display = 'none';
            searchInputMobile.focus();
        });
    }

    // Close on click outside
    document.addEventListener('click', (e) => {
        const isSearchOpen = searchContainer?.classList.contains('is-open') || searchUnderBar?.classList.contains('is-open');
        if (isSearchOpen) {
            const clickedInsideDesktop = searchContainer && searchContainer.contains(e.target);
            const clickedInsideMobile = searchUnderBar && searchUnderBar.contains(e.target);
            const clickedTrigger = searchTrigger && searchTrigger.contains(e.target);

            if (!clickedInsideDesktop && !clickedInsideMobile && !clickedTrigger) {
                closeSearch();
            }
        }
    });

    // Close on Escape key
    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape') {
            closeSearch();
        }
    });
});
