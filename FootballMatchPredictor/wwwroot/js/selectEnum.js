function initSelect2ForEnum(parameters) {
    $('#' + parameters.selectId).select2({
        dropdownParent: parameters.isModal ? ('#modalWindow') : $(document.body),
        placeholder: parameters.placeholder,
        allowClear: true,
        ajax: {
            type: "POST",
            url: parameters.url,
            dataType: "json",
            processResults: function (result) {
                return {
                    results: $.map(result, function (val, index) {
                        return {
                            id: index,
                            text: val
                        };
                    }),
                };
            }
        }
    });
}