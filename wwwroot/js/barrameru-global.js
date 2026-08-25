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

    // Expandable Header Search (Button -> Bar Transition)
    const searchContainer = document.getElementById('headerSearchContainer');
    const searchTrigger = document.getElementById('headerSearchTrigger');
    const searchInput = document.getElementById('headerSearchInput');
    const searchClose = document.getElementById('headerSearchClose');

    if (searchTrigger && searchContainer) {
        searchTrigger.addEventListener('click', (e) => {
            e.stopPropagation();
            searchContainer.classList.add('is-open');
            setTimeout(() => searchInput?.focus(), 150);
        });
    }

    if (searchClose && searchContainer) {
        searchClose.addEventListener('click', (e) => {
            e.stopPropagation();
            searchContainer.classList.remove('is-open');
            if (searchInput) searchInput.value = '';
        });
    }

    // Close when clicking outside
    document.addEventListener('click', (e) => {
        if (searchContainer && searchContainer.classList.contains('is-open')) {
            if (!searchContainer.contains(e.target)) {
                searchContainer.classList.remove('is-open');
            }
        }
    });

    // Close on Escape key
    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape' && searchContainer?.classList.contains('is-open')) {
            searchContainer.classList.remove('is-open');
        }
    });
});
