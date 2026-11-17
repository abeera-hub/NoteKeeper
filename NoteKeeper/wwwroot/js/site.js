// Small client-side helper: prevent accidental double submission of forms.
document.querySelectorAll('form').forEach(form => {
    form.addEventListener('submit', () => {
        const button = form.querySelector('button[type="submit"]');
        if (button && form.checkValidity()) {
            button.dataset.originalText = button.innerText;
            button.disabled = true;
            button.innerText = 'Please wait...';
        }
    });
});
