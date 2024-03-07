function openModal(parameters) {
    const { data: id, url, modalId, modalTitle } = parameters;
    const modal = $('#' + modalId);

    if (!url) {
        alert('Извиняемся.. Возникла ошибка!');
        return;
    }

    const ajaxParams = {
        type: 'GET',
        url,
        success: function (response) {
            modal.find(".modal-body").html(response);
            modal.find(".modal-title").html(modalTitle);
            modal.modal('show');
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.responseText);
        }
    };

    if (id) {
        ajaxParams.data = { "id": id };
    }

    $.ajax(ajaxParams);
}
