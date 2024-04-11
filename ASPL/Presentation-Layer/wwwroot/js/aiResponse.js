function getErrorExplanation(code, error) {

    $.ajax({
        url: 'ai/getErrorExplanation',
        method: 'POST',
        data: {
            code: code,
            error: error
        },
        success: function (response) {
            loadErrorExplaination2(response)
        },
        error: function (xhr, status, error) {
            alertMessage("Something Went Wrong!")
        }
    });
}