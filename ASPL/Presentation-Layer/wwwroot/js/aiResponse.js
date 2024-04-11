function getErrorExplanation(course, error) {

    $.ajax({
        url: 'ai/getErrorExplanation',
        method: 'POST',
        data: {
            course: course,
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