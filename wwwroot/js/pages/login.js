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

    // 1-Click Instant Demo Login (Autofill & Auto-Submit)
    const demoButtons = document.querySelectorAll('.demo-account-pill');
    const emailInput = document.getElementById('emailInput');
    const loginForm = document.getElementById('accountLoginForm');

    demoButtons.forEach(btn => {
        btn.addEventListener('click', () => {
            const email = btn.getAttribute('data-email');
            const pass = btn.getAttribute('data-pass');
            if (emailInput && passwordInput) {
                emailInput.value = email;
                passwordInput.value = pass;

                if (loginForm) {
                    btn.disabled = true;
                    btn.style.opacity = '0.75';
                    btn.innerHTML = `<span class="spinner-border spinner-border-sm me-1" role="status" aria-hidden="true"></span> Signing in...`;
                    loginForm.submit();
                }
            }
        });
    });
});
