const form = document.querySelector('form');
const [password, confirm] = form.querySelectorAll('input[type="password"]');
const message = form.querySelector('.form-message');

form.addEventListener('submit', (e) => {
  e.preventDefault();
  if (confirm && password.value !== confirm.value) {
    message.textContent = 'The passwords do not match. Please check them.';
    return;
  }
  location.href = 'index.html';
});
