/**
 * PAGE SCRIPT: LOGIN (Login.cshtml)
 */

document.addEventListener('DOMContentLoaded', () => {
    // Password visibility toggle
    const toggleBtn = document.getElementById('togglePasswordBtn');
    const passwordInput = document.getElementById('passwordInput');

    if (toggleBtn && passwordInput) {
        toggleBtn.addEventListener('click', () => {
            const isPassword = passwordInput.getAttribute('type') === 'password';
            passwordInput.setAttribute('type', isPassword ? 'text' : 'password');
            toggleBtn.innerHTML = isPassword ? '<i class="bi bi-eye-slash-fill"></i>' : '<i class="bi bi-eye-fill"></i>';
        });
    }

    // Demo account autofill buttons
    const demoButtons = document.querySelectorAll('.demo-account-pill');
    const emailInput = document.getElementById('emailInput');

    demoButtons.forEach(btn => {
        btn.addEventListener('click', () => {
            const email = btn.getAttribute('data-email');
            const pass = btn.getAttribute('data-pass');
            if (emailInput && passwordInput) {
                emailInput.value = email;
                passwordInput.value = pass;
                if (typeof Swal !== 'undefined') {
                    Swal.fire({
                        toast: true,
                        position: 'top-end',
                        icon: 'info',
                        title: 'Filled demo credentials!',
                        showConfirmButton: false,
                        timer: 1500
                    });
                }
            }
        });
    });
});
