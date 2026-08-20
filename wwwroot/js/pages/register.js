/**
 * PAGE SCRIPT: REGISTER (Register.cshtml)
 */

document.addEventListener('DOMContentLoaded', () => {
    // Password toggle
    const togglePassBtn = document.getElementById('toggleRegisterPassBtn');
    const passInput = document.getElementById('registerPassInput');

    if (togglePassBtn && passInput) {
        togglePassBtn.addEventListener('click', () => {
            const isPassword = passInput.getAttribute('type') === 'password';
            passInput.setAttribute('type', isPassword ? 'text' : 'password');
            togglePassBtn.innerHTML = isPassword ? '<i class="bi bi-eye-slash-fill"></i>' : '<i class="bi bi-eye-fill"></i>';
        });
    }

    // Confirm password toggle
    const toggleConfirmBtn = document.getElementById('toggleRegisterConfirmBtn');
    const confirmInput = document.getElementById('registerConfirmInput');

    if (toggleConfirmBtn && confirmInput) {
        toggleConfirmBtn.addEventListener('click', () => {
            const isPassword = confirmInput.getAttribute('type') === 'password';
            confirmInput.setAttribute('type', isPassword ? 'text' : 'password');
            toggleConfirmBtn.innerHTML = isPassword ? '<i class="bi bi-eye-slash-fill"></i>' : '<i class="bi bi-eye-fill"></i>';
        });
    }
});
