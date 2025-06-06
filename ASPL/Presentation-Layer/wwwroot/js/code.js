var code = "";
var output = "";
var errorExplanation = "";

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip()
    $(".runCode .spinner").hide();
    $("#error-explaination-placeholder").hide()
    $(".runCode").on("click", function () {
        $(".runCode .text").hide();
        $(".runCode .spinner").show();

        alertMessage("Running Code!")

        runCode()
    });

    $(".copyCode .bi-check2").hide();
    $(".copyCode").on("click", function () {
        $(".copyCode .bi-check2").show();
        $(".copyCode .bi-clipboard").hide();

        navigator.clipboard.writeText(editor.getValue());
        alertMessage("Copied Code!")

        setTimeout(function () {
            $(".copyCode .bi-check2").hide();
            $(".copyCode .bi-clipboard").show();
        }, 2000);
    });

    editor.on('change', (args) => {
        saveCode()
    })

});

function loadCode() {
    setTimeout(function () {
        var chapter = $("#chapter-dropdown").find(".selected").attr("chapter")
        $.ajax({
            url: 'app/getCode',
            data: { chapter },
            type: 'GET',
            success: function (response) {
                console.log("Load:")
                console.log(response)

                if (response["output"] == "") {
                    response["output"] = "Output"
                }

                if (response["errorExplanation"] == "") {
                    response["errorExplanation"] = "-"
                }

                editor.setValue(response["code"])
                $("#output-content").text(response["output"])
                $("#error-explaination-content").text(response["errorExplanation"])

                output = response["output"]
                errorExplanation = response["errorExplanation"]

            },
            error: function (xhr, status, error) {
                alertMessage("Something Went Wrong!")
            }
        });
    }, 0);
}

function saveCode() {
    setTimeout(function () {
        code = editor.getValue()

        if (code != "") {
            var chapter = $("#chapter-dropdown").find(".selected").attr("chapter")
            $.ajax({
                url: 'app/saveCode',
                method: 'POST',
                data: {
                    chapter: chapter,
                    code: code,
                    output: output,
                    errorExplanation: errorExplanation
                },
                success: function (response) {
                    console.log("Saved")
                    console.log({
                        chapter: chapter,
                        code: code,
                        output: output,
                        errorExplanation: errorExplanation
                    })
                },
                error: function (xhr, status, error) {
                    alertMessage("Something Went Wrong!")
                }
            });
        }
    }, 0);
}

function runCode() {
    if (code != "") {
        $.ajax({
            url: 'app/runCode',
            method: 'POST',
            data: {
                course: currentCourse,
                code: code
            },
            success: function (response) {
                output = response
                $("#output-content").text(response)
                saveCode()
                $(".runCode .text").show();
                $(".runCode .spinner").hide();
                if ($(window).width() < 768) {
                    $(".course-section").hide();
                    $(".code-section").hide();
                    $(".output-section").show();
                    $('.nav-pills .nav-item .nav-link').each(function (i, item) {
                        $(item).removeClass("active");
                    });
                    $('.nav-pills .nav-item .nav-link').last().addClass("active");
                }
                loadErrorExplaination()
                
            },
            error: function (xhr, status, error) {
                alertMessage("Something Went Wrong!")
            }
        });
    }
}

function loadErrorExplaination() { 
    if (!$("#explaination-section-content").hasClass("show")) {
        $(".explaination-nav").trigger('click');
    }
    $("#error-explaination-content").hide()
    $("#error-explaination-placeholder").show()
    getErrorExplanation(currentCourse, output)
}

function loadErrorExplaination2(response) {
    errorExplanation = response
    saveCode()
    $("#error-explaination-content").text(response)
    $("#error-explaination-placeholder").hide()
    $("#error-explaination-content").show()
    console.log(response)
}