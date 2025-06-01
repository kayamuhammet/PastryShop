function showSuccessMessage(message) {
    if (message) {
        Swal.fire({
            title: 'Başarılı!',
            text: message,
            icon: 'success',
            confirmButtonText: 'Tamam',
            customClass: {
                popup: 'swal2-popup-custom',
                select: 'd-none'
            }
        });
    }
}

function showErrorMessage(message) {
    if (message) {
        Swal.fire({
            title: 'Hata!',
            text: message,
            icon: 'error',
            confirmButtonText: 'Tamam'
        });
    }
}

function showWarningMessage(message) {
    if (message) {
        Swal.fire({
            title: 'Uyarı!',
            text: message,
            icon: 'warning',
            confirmButtonText: 'Tamam'
        });
    }
}
