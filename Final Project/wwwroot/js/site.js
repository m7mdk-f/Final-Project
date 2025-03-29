const buttons = document.querySelectorAll('.flutter-button');
function createRipple(event) {
    const button = event.currentTarget;

    const ripple = document.createElement('span');
    ripple.classList.add('flutter-ripple');

    const diameter = Math.max(button.clientWidth, button.clientHeight);
    const radius = diameter / 2;

    ripple.style.width = ripple.style.height = `${diameter}px`;
    ripple.style.left = `${event.clientX - button.getBoundingClientRect().left - radius}px`;
    ripple.style.top = `${event.clientY - button.getBoundingClientRect().top - radius}px`;

    button.appendChild(ripple);

    setTimeout(() => {
        ripple.remove();
    }, 600);
}

buttons.forEach(button => {
    button.addEventListener('mousedown', createRipple);
    button.addEventListener('touchstart', createRipple);
});