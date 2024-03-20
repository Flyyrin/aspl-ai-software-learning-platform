$(document).ready(function(){
    $('[data-toggle="tooltip"]').tooltip()
    $(".runCode .spinner").hide();
    $(".runCode").on( "click", function() {
        $(".runCode .text").hide();
        $(".runCode .spinner").show();

        alertMessage("Running Code!")

        setTimeout(function(){
            $(".runCode .text").show();
            $(".runCode .spinner").hide();
            if ($(window).width() < 768) {
                $(".course-section").hide();
                $(".code-section").hide();
                $(".output-section").show();
                $('.nav-pills .nav-item .nav-link').each(function(i, item){    
                    $(item).removeClass("active");
                });
                $('.nav-pills .nav-item .nav-link').last().addClass("active");
            }
        }, 2000);
    });

    $(".copyCode .bi-check2").hide();
    $(".copyCode").on( "click", function() {
        $(".copyCode .bi-check2").show();
        $(".copyCode .bi-clipboard").hide();

        navigator.clipboard.writeText(editor.getValue());
        alertMessage("Copied Code!")

        setTimeout(function(){
            $(".copyCode .bi-check2").hide();
            $(".copyCode .bi-clipboard").show();
        }, 2000);
    });
});