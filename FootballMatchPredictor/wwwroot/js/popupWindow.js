function doActionWithPopupWindow(event, parameters) {
    event.preventDefault();
    formId = parameters.formId;
    const data = $('#' + formId).serialize()
    $.ajax({
        type: 'POST',
        url: parameters.url,
        data: data,
        success: function (response) {
            Swal.fire({
                title: 'Уведомление!',
                text: response,
                icon: 'success',
                confirmButtonColor: "#0d6efd",
                cancelButtonColor: "#0d6efd",
                confirmButtonText: 'Окей',
                background: '#212529',
                color: 'white'
            }).then(() => {
                location.reload()
            });
        },
        error: function (response) {
            Swal.fire({
                title: 'Уведомление!',
                text: response.responseJSON.errorMessage,
                icon: 'error',
                confirmButtonColor: "#0d6efd",
                confirmButtonText: 'Окей',
                background: '#212529',
                color: 'white'
            }).then(() => {
                location.reload()
            });
        }
    })
}

