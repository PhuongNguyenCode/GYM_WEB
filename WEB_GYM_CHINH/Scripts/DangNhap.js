const loginButtonRegister = document.getElementById('loginButtonRegister'),
    loginButtonAccess = document.getElementById('loginButtonAccess'),
    loginAccessRegister = document.getElementById('loginAccessRegister');

loginButtonRegister.addEventListener('click', () => {
    loginAccessRegister.classList.add('active');
});

loginButtonAccess.addEventListener('click', () => {
    loginAccessRegister.classList.remove('active');
});

const loginPassword = document.getElementById('loginPassword'),
    loginPasswordCreate = document.getElementById('loginPasswordCreate');

loginPassword.addEventListener('click', () => {
    loginPassword.classList.toggle('ri-eye-off-fill');
    loginPassword.classList.toggle('ri-eye-fill');

    const input = document.getElementById('password');
    if (input.getAttribute('type') === 'password') {
        input.setAttribute('type', 'text');
    } else {
        input.setAttribute('type', 'password');
    }
});

loginPasswordCreate.addEventListener('click', () => {
    loginPasswordCreate.classList.toggle('ri-eye-off-fill');
    loginPasswordCreate.classList.toggle('ri-eye-fill');

    const input = document.getElementById('passwordCreate');
    if (input.getAttribute('type') === 'password') {
        input.setAttribute('type', 'text');
    } else {
        input.setAttribute('type', 'password');
    }
});