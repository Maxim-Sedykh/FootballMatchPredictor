function doActionWithConfirmation(event, parameters) {
    event.preventDefault();
    formId = parameters.formId;
    Swal.fire({
        title: "Вы действительно хотите " + parameters.confirmationTitle,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#042b76",
        cancelButtonColor: "#800707",
        confirmButtonText: "Да!",
        cancelButtonText: "Отмена",
        background: '#212529',
        iconColor: 'red',
        color: 'white',
    }).then(async (result) => {
        if (result.isConfirmed) {
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
    })
}