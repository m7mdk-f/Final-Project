function toggleNavBar() {
    let ul = document.getElementById('UlNavBar')
    ul.classList.toggle("activat");
    if (ul.classList.contains("activat")) {
        ul.style.maxHeight = "1000px";
    }
    else {
        ul.style.maxHeight = "0px";
    }
}

function toggleNotifications() {
    const dropdownMenu = document.getElementById('notificationDropdownMenu');

    if (dropdownMenu.classList.contains('showdiv')) {
        dropdownMenu.classList.add('hide');
        setTimeout(() => {
            dropdownMenu.classList.remove('hide');
            dropdownMenu.classList.remove('showdiv');
        }, 300);
    } else {
        dropdownMenu.classList.remove('hide');
        dropdownMenu.classList.add('showdiv');
    }
}

document.addEventListener('click', function (event) {
    const dropdownMenu = document.getElementById('notificationDropdownMenu');
    const notificationIcon = document.querySelector('[data-bs-toggle="dropdown"]');
    const ul = document.getElementById('UlNavBar')
    const buttontoogle = document.getElementById('buttontoogle');

    if (!ul.contains(event.target) && !buttontoogle.contains(event.target)) {
        ul.style.maxHeight = "0px";
        ul.classList.toggle("activat");

    }

    if (!dropdownMenu.contains(event.target) && !notificationIcon.contains(event.target)) {
        if (dropdownMenu.classList.contains('showdiv')) {
            dropdownMenu.classList.add('hide');
            setTimeout(() => {
                dropdownMenu.classList.remove('hide');
                dropdownMenu.classList.remove('showdiv');
            }, 300);
        }
    }
});



document.addEventListener('DOMContentLoaded', function () {
    const dropdownButton = document.getElementById('dropdownMenuButton');
    const customDropdown = document.querySelector('.custom-dropdown');

    dropdownButton.addEventListener('click', function (e) {
        e.preventDefault();
        customDropdown.classList.toggle('show');
    });

    document.addEventListener('click', function (event) {
        if (!customDropdown.contains(event.target)) {
            customDropdown.classList.remove('show');
        }
    });
});