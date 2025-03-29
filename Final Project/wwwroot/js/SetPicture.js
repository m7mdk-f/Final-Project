    let cropper;
    const imageModal = new bootstrap.Modal(document.getElementById('imageModal'));
    const imageInput = document.getElementById('imageInput');
    const cropImage = document.getElementById('cropImage');

    function previewImage(event) {
            const file = event.target.files[0];
    if (file) {
                const reader = new FileReader();
    reader.onload = function(e) {
        cropImage.src = ''; // تصفير الصورة السابقة
    cropImage.src = e.target.result;
    imageModal.show();
    setTimeout(initCropper, 500);
                };
    reader.readAsDataURL(file);

    // تصفير إدخال الملف ليسمح باختيار نفس الصورة مرة أخرى
    imageInput.value = '';
            }
        }

    function initCropper() {
    if (cropper) cropper.destroy();
    cropper = new Cropper(cropImage, {
        aspectRatio: 1,
    viewMode: 1,
    background: false,
    guides: false,
    dragMode: 'move', // السماح بتحريك الصورة بحرية داخل الدائرة
    cropBoxResizable: false,
    cropBoxMovable: false,
    autoCropArea: 1, // جعل الصورة تمتد لتغطية الدائرة بالكامل
    ready() {
        // إخفاء مربع الاقتصاص الافتراضي
        document.querySelector('.cropper-container .cropper-crop-box').style.display = 'none';
    document.querySelector('.cropper-container .cropper-view-box').style.borderRadius = '50%'; // جعل المعاينة دائرية
        }
    });
}
function saveCroppedImage() {
    const croppedCanvas = cropper.getCroppedCanvas({
        width: 150,
        height: 150
    });

    const finalImage = document.getElementById('finalImage');
    const svgIcon = document.getElementById('svgIcon');

    
    // Update the final cropped image preview
    finalImage.src = croppedCanvas.toDataURL();
    svgIcon.style.display="none"
    finalImage.style.display = "block";
    imageModal.hide();
}
