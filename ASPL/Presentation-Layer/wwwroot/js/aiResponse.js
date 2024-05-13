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

async function getResponse() {
    var data = chat
    var courseContent = $("#course-content").html()
    data.unshift({
        sender: "ai",
        content: `Okay, I will remember that.`,
        time: getCurrentTime()
    })
    data.unshift({
        sender: "student",
        content: `I am currently learning ${courseName}, and this is the course i am following: [[${courseContent}]] Remember this!`,
        time: getCurrentTime()
    })

    data.splice(data.length - 1, 0, {
        sender: "student",
        content: `This is my code [[${editor.getValue()}]], and this is my output/error: [[${output}]] only use this if helpfull, Remember this!`,
        time: getCurrentTime()
    }, {
        sender: "ai",
        content: `Okay, I will remember that.`,
        time: getCurrentTime()
    });

    return new Promise((resolve, reject) => {
        $.ajax({
            url: 'ai/getResponse',
            method: 'POST',
            data: {
                chat: data
            },
            success: function (response) {
                resolve(response);
            },
            error: function (xhr, status, error) {
                alertMessage("Something Went Wrong!")
                reject("Something went wrong!");
            }
        });
    });
}