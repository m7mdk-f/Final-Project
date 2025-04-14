var isValid = true;

function inputcheck(event) {
    var inputElement = event.target;
    var inputId = inputElement.id;

    var inputValue = inputElement.value.trim();

    var errorElement = document.getElementById(inputId + "Error");

    errorElement.textContent = "";

    if (inputId === "CurrentPassword") {
        if (!inputValue) {
            errorElement.textContent = "Current password is required.";
        }
    } else if (inputId === "NewPassword") {
        var minLength = 8;
        if (!inputValue) {
            errorElement.textContent = "New password is required.";
        } else if (inputValue.length < minLength) {
            errorElement.textContent = `New password must be at least ${minLength} characters long.`;
        }
    } else if (inputId === "ConFirmPassword") {
        var newPassword = document.getElementById('NewPassword').value.trim();
        if (!inputValue) {
            errorElement.textContent = "Confirm password is required.";
        } else if (inputValue !== newPassword) {
            errorElement.textContent = "New password and confirm password must match.";
        }
    }
}

function onchangeInput() {
    var currentPassword = document.getElementById('CurrentPassword').value.trim();
    var newPassword = document.getElementById('NewPassword').value.trim();
    var confirmPassword = document.getElementById('ConFirmPassword').value.trim();

    var currentPasswordError = document.getElementById('CurrentPasswordError');
    var newPasswordError = document.getElementById('NewPasswordError');
    var confirmPasswordError = document.getElementById('ConFirmPasswordError');

    currentPasswordError.textContent = "";
    newPasswordError.textContent = "";
    confirmPasswordError.textContent = "";


    if (!currentPassword) {
        currentPasswordError.textContent = "Current password is required.";
        isValid = false;
    }
    if (!newPassword) {
        newPasswordError.textContent = "New password is required.";
        isValid = false;
    }
    var minLength = 8;
    if (newPassword.length < minLength) {
        newPasswordError.textContent = `New password must be at least ${minLength} characters long.`;
        isValid = false;
    }
    if (!confirmPassword) {
        confirmPasswordError.textContent = "Confirm password is required.";
        isValid = false;
    }
    if (newPassword !== confirmPassword) {
        confirmPasswordError.textContent = "New password and confirm password must match.";
        isValid = false;
    }
}

function ChangePassword(event) {
    event.preventDefault();

    var currentPassword = document.getElementById('CurrentPassword').value.trim();
    var newPassword = document.getElementById('NewPassword').value.trim();
    var confirmPassword = document.getElementById('ConFirmPassword').value.trim();
    var currentPasswordError = document.getElementById('CurrentPasswordError');
    onchangeInput();
    if (isValid) {
        const xhr = new XMLHttpRequest();
        const formData = new FormData();
        formData.append("CurrentPassword", currentPassword);
        formData.append("NewPassword", newPassword);
        formData.append("ConFirmPassword", confirmPassword);

        xhr.open("POST", "/Admin/Home/ChangePassword", true);
        xhr.onload = function () {
            if (xhr.status === 200) {

                // location.reload();
            } else {
                newPassword = "";
                confirmPassword = ""
                currentPasswordError.textContent = "Current password is incorrect.";
            }
        };
        xhr.send(formData);
    }
}


function previewImage(input) {
    if (input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById('profileImagePreview').src = e.target.result;
        }
        reader.readAsDataURL(input.files[0]);
    }
}
document.addEventListener('DOMContentLoaded', function () {
    const editProfileModal = new bootstrap.Modal(document.getElementById('editProfileModal'));

    document.getElementById('editProfileBtn').addEventListener('click', function () {
        editProfileModal.show();

    });

    document.getElementById('editProfileModal').addEventListener('hidden.bs.modal', function () {
    });
});
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.toggle-password').forEach(function (button) {
        button.addEventListener('click', function () {
            const container = button.closest('.form-floating');
            const passwordInput = container.querySelector('.password-input');
            const passwordIcon = button.querySelector('.password-icon');

            if (passwordInput.type === 'password') {
                passwordInput.type = 'text';
                passwordIcon.classList.remove('bi-eye-slash');
                passwordIcon.classList.add('bi-eye');
            } else {
                passwordInput.type = 'password';
                passwordIcon.classList.remove('bi-eye');
                passwordIcon.classList.add('bi-eye-slash');
            }
        });
    });
});
// Function to validate form inputs before submitting
function validateProfileForm() {
    const firstName = document.querySelector('input[name="FName"]').value;
    const lastName = document.querySelector('input[name="LName"]').value;
    const phoneNumber = document.querySelector('input[name="PhoneNumber"]').value;
    const address = document.querySelector('select[name="Address"]').value;

    const errorFName = document.getElementById('errorFName');
    const errorLName = document.getElementById('errorLName');
    const errorPhoneNumber = document.getElementById('errorPhoneNumber');
    const errorAddress = document.getElementById('errorAddress');

    // Hide the error messages initially
    errorFName.style.display = 'none';
    errorLName.style.display = 'none';
    errorPhoneNumber.style.display = 'none';
    errorAddress.style.display = 'none';

    let isValid = true;
    if (!firstName) {
        errorFName.style.display = 'inline';
        isValid = false;
    }

    if (!lastName) {
        errorLName.style.display = 'inline';
        isValid = false;
    }

    // Validate Phone Number
    const phoneRegex = /^05\d{8}$/;
    if (phoneNumber && !phoneRegex.test(phoneNumber)) {
        errorPhoneNumber.style.display = 'inline';
        isValid = false;
    }


    return isValid;
}

// Attach event listener to the submit button of the form
document.getElementById('profileForm').addEventListener('submit', function (event) {
    if (!validateProfileForm()) {
        event.preventDefault();
    }
});

let cropper;

function initCropper() {
    const cropImage = document.getElementById('cropImage');
    if (cropper) cropper.destroy(); // Destroy any previous instances

    cropper = new Cropper(cropImage, {
        aspectRatio: 1,
        viewMode: 1,
        background: false,
        guides: false,
        dragMode: 'move',
        cropBoxResizable: false,
        cropBoxMovable: false,
        autoCropArea: 1,
        ready() {
            // Ensure the crop box is circular
            document.querySelector('.cropper-container .cropper-crop-box').style.borderRadius = '50%';
            document.querySelector('.cropper-container .cropper-view-box').style.borderRadius = '50%';
        }
    });
}

function previewImage(event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById('cropImage').src = e.target.result;
            const imageModal = new bootstrap.Modal(document.getElementById('imageModal'));
            imageModal.show();
            setTimeout(initCropper, 500);
        };
        reader.readAsDataURL(file);
        event.target.value = ""
    }
}

function saveCroppedImage(event) {
    event.preventDefault();

    const croppedCanvas = cropper.getCroppedCanvas({ width: 150, height: 150 });
    croppedCanvas.toBlob((blob) => {
        const file = new File([blob], 'profile.jpg', { type: 'image/jpeg' });

        const formData = new FormData();
        formData.append("ImageUrl", file);

        const xhr = new XMLHttpRequest();
        xhr.open("POST", "/Admin/Home/ChangeImage", true);

        xhr.onload = function () {
            if (xhr.status === 200) {
                location.reload();
            } else {
                alert("An error occurred while uploading the image.");
            }
        };

        xhr.send(formData);
    }, 'image/jpeg');
}