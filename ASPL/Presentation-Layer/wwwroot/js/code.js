var code = "";
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip()
    $(".runCode .spinner").hide();
    loadCode()
    $(".runCode").on("click", function () {
        $(".runCode .text").hide();
        $(".runCode .spinner").show();

        alertMessage("Running Code!")

        setTimeout(function () {
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
        }, 2000);
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

                if (response["output"] == "") {
                    response["output"] = "Output"
                }

                editor.setValue(response["code"])
                console.log(response["code"])
                $("#output-content").text(response["output"])
                $("#error-explaination-content").text(response["errorExplanation"])


            },
            error: function (xhr, status, error) {
                alertMessage("Something Went Wrong!")
            }
        });
    }, 100);
}

function saveCode() {
    code = editor.getValue()
    if (code == "Loading...") {
        code = ""
    }

    if (code != "") {
        var chapter = $("#chapter-dropdown").find(".selected").attr("chapter")
        $.ajax({
            url: 'app/saveCode',
            method: 'POST',
            data: {
                chapter: chapter,
                code: code
            },
            success: function (response) {
                console.log("saved code")
            },
            error: function (xhr, status, error) {
                alertMessage("Something Went Wrong!")
            }
        });
    }
}