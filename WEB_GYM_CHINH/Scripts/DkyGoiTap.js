document.querySelectorAll('.package-btn').forEach(button => {
    button.addEventListener('click', () => {
        const target = document.querySelector(button.getAttribute('data-target'));
        target.scrollIntoView({ behavior: 'smooth' });
    });
});

console.log("DkyGoiTap.js loaded");