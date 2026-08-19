/**
 * PAGE SCRIPT: CONTACT US (Contact.cshtml)
 */

document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('contactDropLineForm');
    if (form) {
        form.addEventListener('submit', (e) => {
            e.preventDefault();
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    icon: 'success',
                    title: 'Message Sent!',
                    text: 'Thank you for reaching out. We will get back to you shortly!',
                    confirmButtonColor: '#B67961'
                });
            }
            form.reset();
        });
    }
});
