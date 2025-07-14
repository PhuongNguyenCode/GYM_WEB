const registerPassword = document.getElementById('registerPassword'),
    confirmPasswordToggle = document.getElementById('confirmPasswordToggle');

registerPassword.addEventListener('click', () => {
    registerPassword.classList.toggle('ri-eye-off-fill');
    registerPassword.classList.toggle('ri-eye-fill');

    const input = document.getElementById('passwordCreate');
    if (input.getAttribute('type') === 'password') {
        input.setAttribute('type', 'text');
    } else {
        input.setAttribute('type', 'password');
    }
});

confirmPasswordToggle.addEventListener('click', () => {
    confirmPasswordToggle.classList.toggle('ri-eye-off-fill');
    confirmPasswordToggle.classList.toggle('ri-eye-fill');

    const input = document.getElementById('confirmPassword');
    if (input.getAttribute('type') === 'password') {
        input.setAttribute('type', 'text');
    } else {
        input.setAttribute('type', 'password');
    }
});