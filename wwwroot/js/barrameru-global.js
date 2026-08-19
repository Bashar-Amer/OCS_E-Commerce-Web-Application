/**
 * Barrameru Global Layout & UI Interactions
 */

document.addEventListener('DOMContentLoaded', () => {
    // Newsletter Submit Event
    document.querySelectorAll('.newsletter-form').forEach(form => {
        form.addEventListener('submit', (e) => {
            e.preventDefault();
            const emailInput = form.querySelector('input[type="email"]');
            if (emailInput && emailInput.value) {
                if (typeof Swal !== 'undefined') {
                    Swal.fire({
                        icon: 'success',
                        title: 'Thank you for subscribing!',
                        text: `We've sent a confirmation to ${emailInput.value}`,
                        confirmButtonColor: '#B67961'
                    });
                }
                emailInput.value = '';
            }
        });
    });

    // Sticky Header Scroll Shadow
    const header = document.querySelector('.header-barrameru');
    window.addEventListener('scroll', () => {
        if (window.scrollY > 40) {
            header.classList.add('shadow-sm');
        } else {
            header.classList.remove('shadow-sm');
        }
    });
});
